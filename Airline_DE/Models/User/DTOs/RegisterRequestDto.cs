

namespace Airline_DE.Models.User.DTOs
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsSuscribedNewsletter { get; set; }
    }
}
