using Airline_DE.Models.Receiver.DTOs;

namespace Airline_DE.Models.Assignment.DTOs
{
    public class CreateAssignmentDTO
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Wide { get; set; }
        public decimal Lenght { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool IsCopyDocumentEmail { get; set; }
        public CreateReceiverDTO Receiver { get; set; }
    }
}
