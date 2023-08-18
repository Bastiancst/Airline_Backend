namespace Airline_DE.Models.Office
{
    public class Office
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Addres { get; set; }
        public string Name { get; set; }
        public string ContactAirport { get; set; }
        public int AirplaneAvailable { get; set; }
    }
}
