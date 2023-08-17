using Airline_DE.Enums;

namespace Airline_DE.Models.Payment
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public PaymentType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
