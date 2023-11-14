using Airline_DE.Interfaces;
using Airline_DE.Models.User;
using Airline_DE.Models.User.DTOs;
using Airline_DE.Settings;
using Airline_DE.Wrappers;
using Airline_DE.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Airline_DE.Exceptions;
using System.Data;
using Airline_DE.Enums;
using Airline_DE.Interfaces.IRepository;

namespace Airline_DE.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRecoverPasswordRepository _recoverPassword;
        public AccountServices(UserManager<User> userManager, SignInManager<User> signInManager, IRecoverPasswordRepository recoverPassword)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _recoverPassword = recoverPassword;
        }

        public async Task<ApiResponse<AuthenticationResponseDTO>> AuthenticateAsync(AuthenticationRequestDTO request, string ipAddress, string domain)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ApiResponse<AuthenticationResponseDTO>($"User not found");
            }

            if (!user.EmailConfirmed)
            {
                return new ApiResponse<AuthenticationResponseDTO>($"User not confirmed");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);


            if (!result.Succeeded)
            {
                return new ApiResponse<AuthenticationResponseDTO>($"Invalid credentials");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJwtToken(user, domain);

            if (jwtSecurityToken == null)
            {
                return new ApiResponse<AuthenticationResponseDTO>($"Unrecognized");
            }

            AuthenticationResponseDTO response = new()
            {
                JWTtoken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                IsVerified = user.EmailConfirmed,
            };

            var resultUpdate = await _signInManager.UserManager.UpdateAsync(user);

            if (!resultUpdate.Succeeded)
            {
                return new ApiResponse<AuthenticationResponseDTO>($"Internal Timer Error");
            }

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();

            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new ApiResponse<AuthenticationResponseDTO>(response, $"{user.UserName} authenticated");
        }

        public async Task<ApiResponse<RegisterResponseDTO>> RegisterAsync(RegisterRequestDto request, string origin)
        {
            var userWithUsername = await _userManager.FindByEmailAsync(request.Email);

            if (userWithUsername != null)
            {
                return new ApiResponse<RegisterResponseDTO>($"User {request.Email} already in use");
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new ApiException($"Password and confirm password missmatch.");
            }

            var user = new User
            {
                Email = request.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                return new ApiResponse<RegisterResponseDTO>($"Email {request.Email} already in use");
            }
            else
            {
                user.UserName = request.Email.Split("@")[0];
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleType.Admin.ToString());
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var responseDto = new RegisterResponseDTO()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        ConfirmToken = confirmationToken
                    };

                    return new ApiResponse<RegisterResponseDTO>(responseDto, message: $"user succesfully registered : {request.Email}");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }

        public async Task<ApiResponse<bool>> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ApiResponse<bool>($"User not found");
            }

            if (user.EmailConfirmed)
            {
                return new ApiResponse<bool>(true, $"User has already been confirmed");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return new ApiResponse<bool>($"User couln't be confirmed");
            }

            return new ApiResponse<bool>(true, $"User confirmed");
        }

        public async Task<ApiResponse<AuthenticationResponseDTO>> GetCurrentUser(string token, string ip, string domain)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = JWTSettings.Issuer,
                ValidAudience = JWTSettings.Audience,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            if (DateTime.Compare(validatedToken.ValidTo.Add(TimeSpan.FromMinutes(JWTSettings.DurationInMinutes)), DateTime.UtcNow) <= 0)
            {
                throw new ApiException($"Token expired");
            }

            var Token = (JwtSecurityToken)validatedToken;
            var email = Token.Claims.First(x => x.Type == "email").Value;
            var user = await _userManager.FindByEmailAsync(email);
            JwtSecurityToken jwtSecurityToken = await GenerateJwtToken(user, domain);
            await _signInManager.SignInAsync(user, false);
            AuthenticationResponseDTO response = new AuthenticationResponseDTO
            {
                JWTtoken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                IsVerified = user.EmailConfirmed,
            };

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ip);
            response.RefreshToken = refreshToken.Token;
            return new ApiResponse<AuthenticationResponseDTO>(response, $"{user.UserName} authenticated");
        }

        public async Task<ApiResponse<CodeDTO>> GetCodeResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ApiResponse<CodeDTO>($"User not found");
            }
            var userRecovers = await _recoverPassword.GetAllAsync(x => x.UserId == Guid.Parse(user.Id));

            foreach (var item in userRecovers)
            {
                await _recoverPassword.RemoveAsync(item);
            }

            string code = GenerateRandomNumericCode(6);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassword = new RecoverPassword
            {
                RecoveryCode = code,
                RecoveryToken = token,
                RecoveryCodeTimeStamp = DateTime.UtcNow,
                UserId = Guid.Parse(user.Id)
            };

            await _recoverPassword.CreateAsync(resetPassword);

            var response = new CodeDTO { Email = user.Email, RecoveryCode = code };

            return new ApiResponse<CodeDTO>(response, "Success created recovey code");
        }

        public async Task<ApiResponse<bool>> ResetPassword(ResetPasswordDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new ApiResponse<bool>(false, $"User not found");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new ApiResponse<bool>(false, $"Passwords do not match");
            }

            var recoverydata = await _recoverPassword.GetAsync(x => x.RecoveryCode == request.Code);

            if (recoverydata == null)
            {
                return new ApiResponse<bool>(false, $"Code not found");
            }

            var timeCode = DateTime.UtcNow - recoverydata.RecoveryCodeTimeStamp;

            if (timeCode.Minutes > 5)
            {
                await _recoverPassword.RemoveAsync(recoverydata);
                return new ApiResponse<bool>(false, $"Code time expired, plis get new code");
            }

            var result = await _userManager.ResetPasswordAsync(user, recoverydata.RecoveryToken, request.Password);
            await _recoverPassword.RemoveAsync(recoverydata);

            return new ApiResponse<bool>(true, $"Password changed successfully");
        }

        public async Task<ApiResponse<bool>> ValidateCodeResetPassword(ValidateCodeDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new ApiResponse<bool>(false, $"User not found");
            }

            var resetExists = await _recoverPassword.GetAsync(x => x.RecoveryCode == request.RecoveryCode && x.UserId == Guid.Parse(user.Id));

            if (resetExists == null)
            {
                return new ApiResponse<bool>(false, $"Wrong code");
            }

            return new ApiResponse<bool>(true, $"Code confirmed");
        }

        private async Task<JwtSecurityToken> GenerateJwtToken(User user, string domain)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAdress = IpHelper.GetIpAdress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAdress),

            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken
                (
                issuer: JWTSettings.Issuer,
                audience: JWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(JWTSettings.DurationInMinutes),
                signingCredentials: signinCredentials
                );

            return jwtSecurityToken;
        }

        private static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = System.DateTime.Now.AddDays(1),
                Created = System.DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        private static string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");

        }

        private string GenerateRandomNumericCode(int lenght)
        {
            Random random = new Random();
            int num = 0;
            string randomCode = "";

            for (int i = 0; i < lenght; i++)
            {
                num = random.Next(0, 10);
                randomCode += num.ToString();
            }

            return randomCode;
        }
    }
}
