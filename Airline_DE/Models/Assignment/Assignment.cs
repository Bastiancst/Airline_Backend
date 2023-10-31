namespace Airline_DE.Models.Assignment
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Wide { get; set; }
        public decimal Lenght { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool IsCopyDocumentEmail { get; set; }

    }
}
