using Airline_DE.Enums;

namespace Airline_DE.Models.Payment
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string BuyOrder { get; set; }
        public ServiceType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
