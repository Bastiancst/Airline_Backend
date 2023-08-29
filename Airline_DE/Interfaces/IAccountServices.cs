using Airline_DE.Models.User.DTOs;
using Airline_DE.Wrappers;

namespace Airline_DE.Interfaces
{
    public interface IAccountServices
    {
        Task<ApiResponse<AuthenticationResponseDTO>> AuthenticateAsync(AuthenticationRequestDTO request, string ipAddress, string domain);
        Task<ApiResponse<RegisterResponseDTO>> RegisterAsync(RegisterRequestDto request, string origin);
        Task<ApiResponse<bool>> ConfirmEmailAsync(string userId, string token);
        Task<ApiResponse<AuthenticationResponseDTO>> GetCurrentUser(string token, string ip, string domain);
        Task<ApiResponse<CodeDTO>> GetCodeResetPassword(string email);
        Task<ApiResponse<bool>> ValidateCodeResetPassword(ValidateCodeDTO request);
        Task<ApiResponse<bool>> ResetPassword(ResetPasswordDTO request);
    }
}
