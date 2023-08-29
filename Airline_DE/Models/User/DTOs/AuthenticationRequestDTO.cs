namespace Airline_DE.Models.User.DTOs
{
    public class AuthenticationRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberUser { get; set; }
    }
}
