using Microsoft.AspNetCore.Identity;

namespace Airline_DE.Models.User
{
    public class User : IdentityUser
    {
        public string? ImageProfile { get; set; }
    }
}
