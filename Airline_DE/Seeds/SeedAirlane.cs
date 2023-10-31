using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airport;
using Airline_DE.Models.FlightPlanning;

namespace Airline_DE.Seeds
{
    public static class SeedAirlane
    {
        public static async Task SeedAirlaneAsync(IAirportRepository _airportRepository,IFlightPlanningRepository _flightPlanningRepository, bool isSeedAsync)
        {
            if (isSeedAsync)
            {
                List<Airport> airports = new List<Airport>
                {
                    // México
                    new Airport { Id = Guid.NewGuid(), Name = "Mexico City International Airport", Country = "Mexico", City = "Mexico City" },
                    new Airport { Id = Guid.NewGuid(), Name = "Cancun International Airport", Country = "Mexico", City = "Cancun" },

                    // Brasil
                    new Airport { Id = Guid.NewGuid(), Name = "São Paulo/Guarulhos International Airport", Country = "Brazil", City = "São Paulo" },
                    new Airport { Id = Guid.NewGuid(), Name = "Congonhas International Airport", Country = "Brazil", City = "São Paulo" },


                    // Colombia
                    new Airport { Id = Guid.NewGuid(), Name = "El Dorado International Airport", Country = "Colombia", City = "Bogota" },

                    // Perú
                    new Airport { Id = Guid.NewGuid(), Name = "Jorge Chavez International Airport", Country = "Peru", City = "Lima" },


                    // Chile
                    new Airport { Id = Guid.NewGuid(), Name = "Arturo Merino Benitez International Airport", Country = "Chile", City = "Santiago" },
                };

                foreach (var airport in airports)
                {
                    await _airportRepository.CreateAsync(airport);
                }


                Random rand = new Random();

                foreach (var originAirport in airports)
                {
                    foreach (var finalAirport in airports)
                    {
                        if (originAirport.Id == finalAirport.Id) continue;

                        DateTime startTime = DateTime.Now.AddHours(rand.Next(1, 1000));
                        DateTime endTime = startTime.AddHours(rand.Next(1, 10));

                        FlightPlanning flightPlan = new FlightPlanning
                        {
                            Id = Guid.NewGuid(),
                            OfficeId = Guid.NewGuid(),  
                            OriginAirportId = originAirport.Id,
                            FinalAirportId = finalAirport.Id,
                            StartTime = startTime,
                            EndTime = endTime
                        };

                        await _flightPlanningRepository.CreateAsync(flightPlan);
                    }
                }

            }

        }
    }
}
