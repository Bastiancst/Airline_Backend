﻿using System.Text.Json.Serialization;


namespace Airline_DE.Models.User.DTOs
{
    public class AuthenticationResponseDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWTtoken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
