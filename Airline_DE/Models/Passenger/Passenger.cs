namespace Airline_DE.Models.Passenger
{
    public class Passenger
    {
        public Guid Id { get; set; }
        public string IdentityDocument { get; set; }
        public string Name { get; set; }    
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Addres { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }   
        public int SeatNumber { get; set; }
        public bool isCopyDocumentEmail { get; set; }
    }
}
