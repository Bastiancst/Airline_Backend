using Airline_DE.Enums;

namespace Airline_DE.Models.Employee
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid OfficeId { get; set; }
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
        public string WorkPosition { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Bonus { get; set; }
    }
}
