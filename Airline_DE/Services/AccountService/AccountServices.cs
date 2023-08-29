using Airline_DE.Interfaces;
using Airline_DE.Models.User.DTOs;
using Airline_DE.Wrappers;

namespace Airline_DE.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        public Task<ApiResponse<AuthenticationResponseDTO>> AuthenticateAsync(AuthenticationRequestDTO request, string ipAddress, string domain)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CodeDTO>> GetCodeResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<AuthenticationResponseDTO>> GetCurrentUser(string token, string ip, string domain)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<RegisterResponseDTO>> RegisterAsync(RegisterRequestDto request, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> ResetPassword(ResetPasswordDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> ValidateCodeResetPassword(ValidateCodeDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
