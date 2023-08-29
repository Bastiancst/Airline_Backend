namespace Airline_DE.Models.User.DTOs
{


    public class ValidateCodeDTO
    {
        public string Email { get; set; }
        public string RecoveryCode { get; set; }
    }
}
