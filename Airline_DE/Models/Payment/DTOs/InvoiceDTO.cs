using Airline_DE.Enums;

namespace Airline_DE.Models.Payment.DTOs
{
    public class InvoiceDTO
    {
        public Guid Id { get; set; }
        public string BuyOrder { get; set; }
        public ServiceType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
