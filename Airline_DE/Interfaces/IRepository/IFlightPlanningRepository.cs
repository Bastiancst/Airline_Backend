using Airline_DE.Models.FlightPlanning;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IFlightPlanningRepository : IRepository<FlightPlanning>
    {
        Task<FlightPlanning> UpdateAsync(FlightPlanning entity);
    }
}
