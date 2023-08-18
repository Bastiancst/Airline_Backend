using Airline_DE.Enums;

namespace Airline_DE.Models.Airplane
{
    public class Airplane
    {
        public Guid Id { get; set; }
        public Guid OfficeId { get; set; }
        public string FabricantName { get; set; }
        public DateTime FabricationYear { get; set; }
        public DateTime LastCheck { get; set; }
        public int SitNumber { get; set; }
        public decimal MaxRange { get; set; }
        public decimal MaxHeigth { get; set; }
        public decimal MaxWeigth { get; set; }
        public int SeatAmount { get; set; }
        public AirplaneType Type { get; set; }
    }
}
