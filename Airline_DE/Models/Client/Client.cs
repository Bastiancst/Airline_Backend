namespace Airline_DE.Models.Client
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Addres { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid PaimentId { get; set; }
    }
}
