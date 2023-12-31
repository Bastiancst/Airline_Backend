﻿using Airline_DE.Enums;

namespace Airline_DE.Models.FlightPlanning
{
    public class FlightPlanning
    {
        public Guid Id { get; set; }
        public Guid OfficeId { get; set; }
        public Guid OriginAirportId { get; set; }
        public Guid FinalAirportId { get; set; }
        public PlanningType Type { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
