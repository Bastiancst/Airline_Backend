namespace Airline_DE.Models.Email.DTOs
{
    public class BasicEmailRequestDTO
    {
        public string SenderEmail { get; set; } = "contact@tesseractsoftwares.com";
        public string SenderName { get; set; } = "Airline's contact service";
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
    }
}
