namespace Airline_DE.Models.Passenger.DTOs
{
    public class CreatePassengerDTO
    {
        public Guid ClientId { get; set; }
        public Guid FlightPlanningId { get; set; }
        public string IdentityDocument { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int SeatNumber { get; set; }
        public bool isCopyDocumentEmail { get; set; }
    }
}
