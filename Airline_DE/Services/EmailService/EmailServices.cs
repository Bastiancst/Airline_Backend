using Airline_DE.Interfaces;
using Airline_DE.Models.Email.DTOs;
using Airline_DE.Settings;
using Airline_DE.Wrappers;
using SendGrid.Helpers.Mail;
using SendGrid;


namespace Airline_DE.Services.EmailService
{
    public class EmailServices : IEmailServices
    {
        public async Task<ApiResponse<bool>> SendBasicEmailAsync(BasicEmailRequestDTO request)
        {
            var apiKey = EmailSettings.ApiKey;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(request.SenderEmail, request.SenderName),
                Subject = request.Subject,
                PlainTextContent = request.Message
            };

            msg.AddTo(new EmailAddress(request.ReceiverEmail, request.ReceiverName));
            var response = await client.SendEmailAsync(msg);

            return new ApiResponse<bool>(response.IsSuccessStatusCode);
        }
    }
}
