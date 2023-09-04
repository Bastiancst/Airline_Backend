namespace Airline_DE.Models.User.DTOs
{
    public class RegisterResponseDTO
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string ConfirmToken { get; set; }
    }
}
