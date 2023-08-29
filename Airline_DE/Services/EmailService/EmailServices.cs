using Airline_DE.Interfaces;
using Airline_DE.Models.Email.DTOs;
using Airline_DE.Settings;
using Airline_DE.Wrappers;
using MailKit.Net.Smtp;
using MimeKit;
using System.Reflection;

namespace Airline_DE.Services.EmailService
{
    public class EmailServices : IEmailServices
    {
        public async Task<ApiResponse<string>> SendEmailContactUserAsync(EmailServiceDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(EmailSettings.User));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = request.Subject;


            //string? templateResourceName = "Airline_DE.Services.Templates.Register.html";
            //string templateContent;
            //using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(templateResourceName))
            //using (var reader = new StreamReader(stream))
            //{
            //    templateContent = await reader.ReadToEndAsync();
            //}

            //string body = templateContent
            //    .Replace("{Nombre}", request.Name)
            //    .Replace("{Email}", request.Email);

            email.Body = new TextPart("html") { Text = "" };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(EmailSettings.Server, EmailSettings.Port, MailKit.Security.SecureSocketOptions.Auto);
                await smtp.AuthenticateAsync(EmailSettings.User, EmailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

            return new ApiResponse<string>("", "Email sended");
        }
    }
}
