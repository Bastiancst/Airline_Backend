using Airline_DE.Models.Email.DTOs;
using Airline_DE.Wrappers;

namespace Airline_DE.Interfaces
{
    public interface IEmailServices 
    {
        Task<ApiResponse<string>> SendEmailContactUserAsync(EmailServiceDTO request);
    }
}
