using Airline_DE.Interfaces;
using Airline_DE.Models.Email.DTOs;
using Airline_DE.Models.User.DTOs;
using Airline_DE.Services.AccountServices;
using Airline_DE.Settings;
using Airline_DE.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Airline_DE.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountService;
        private readonly IEmailServices _emailService;

        public AccountController(IAccountServices accountServices, IEmailServices emailService)
        {
            _accountService = accountServices;
            _emailService = emailService;
        }

        
        [HttpPost("user")]
        public async Task<IActionResult> GetCurrentUserAsync(string token)
        {
            try
            {
                //var rawToken = HttpContext.Request.Headers["Authorization"];

                //if (string.IsNullOrEmpty(rawToken))
                //{
                //    return Unauthorized(new { message = "Token is empty" });
                //}

                string domainKey = Request.Headers["domainKey"];
                //string tokena = token.ToString().Split(" ")[1];
                string ip = GenerateIPAddress();
                var result = await _accountService.GetCurrentUser(token, ip, domainKey);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 40615)
                {
                    return BadRequest(new ApiResponse<string>("Error in database"));
                }
                var result = new ApiResponse<AuthenticationResponseDTO>();
                result.Success = false;
                result.Errors = new List<string>() { ex.ToString() };
                return BadRequest(result);
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequestDTO request)
        {
            try
            {
                string domainKey = Request.Headers["domainKey"];
                string ip = GenerateIPAddress();
                var result = await _accountService.AuthenticateAsync(request, ip, domainKey);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 40615)
                {
                    return BadRequest(new ApiResponse<string>("Error in database"));
                }

                var result = new ApiResponse<AuthenticationResponseDTO>();
                result.Success = false;
                result.Errors = new List<string>() { ex.ToString() };
                return BadRequest(result);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                var origin = Request.Headers["origin"];
                var creationResponse = await _accountService.RegisterAsync(request, "");

                if (creationResponse.Success)
                {

                    await _emailService.SendBasicEmailAsync(new BasicEmailRequestDTO
                    {
                        Subject = "Welcome to Tesseract!",
                        Message = $"Hello! {creationResponse.Result.UserId} Welcome to Tesseract Ecosystem! \n please confirm your account with the following URL: \n",
                        ReceiverEmail = $"{creationResponse.Result.Email}",
                        ReceiverName = $"{creationResponse.Result.Email}"
                    });

                    return Ok(creationResponse);
                }

                return BadRequest(creationResponse);
            }
            catch (Exception ex)
            {
                var result = new ApiResponse<string>();
                result.Success = false;
                result.Errors = new List<string>() { ex.ToString() };
                return BadRequest(result);
            }
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {
            try
            {
                token = WebUtility.UrlDecode(token).Replace("%2B", "+");
                var result = await _accountService.ConfirmEmailAsync(id, token);
                if (result.Success)
                {
                    if (result.Message == $"User has already been confirmed")
                    {
                        return Redirect(DomainSettings.AfterConfirmEmailDomain);
                    }
                    
                    //await _emailService.SendBasicEmailAsync(new BasicEmailRequestDTO
                    //{
                    //    Subject = "Tesseract account confirmed!",
                    //    Message = $"hi {user.Name}, your account is now confirmed!",
                    //    ReceiverEmail = $"{user.Email}",
                    //    ReceiverName = $"{user.Name}"
                    //});

                    return Redirect(DomainSettings.AfterConfirmEmailDomain);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 40615)
                {
                    return BadRequest(new ApiResponse<string>("Error in database"));
                }
                var result = new ApiResponse<bool>();
                result.Success = false;
                result.Errors = new List<string>() { ex.ToString() };
                return BadRequest(result);
            }
        }

        [HttpPost("recoverycode")]
        public async Task<IActionResult> RecoverCodeAccount(string email)
        {
            var getCode = await _accountService.GetCodeResetPassword(email);

            if (getCode.Success)
            {
                await _emailService.SendBasicEmailAsync(new BasicEmailRequestDTO
                {
                    Subject = "Tesseract Account Recovery",
                    Message = $"Hello! \n This is your recovery code: \n {getCode.Result.RecoveryCode}",
                    ReceiverEmail = $"{getCode.Result.Email}",
                    ReceiverName = $"{getCode.Result.Email}"
                });

                return Ok(getCode);
            }

            return BadRequest(getCode);
        }

        [HttpPost("validatecode")]
        public async Task<IActionResult> ValidateCodeAccount(ValidateCodeDTO request)
        {
            var code = await _accountService.ValidateCodeResetPassword(request);

            if (code.Result)
            {
                return Ok(code);
            }

            code.Success = false;
            return BadRequest(code);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetCodePasswordAccount(ResetPasswordDTO request)
        {
            var code = await _accountService.ResetPassword(request);

            if (code.Result)
            {
                return Ok(code);
            }

            code.Success = false;
            return BadRequest(code);
        }

        #region private methods

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        #endregion
    }
}
