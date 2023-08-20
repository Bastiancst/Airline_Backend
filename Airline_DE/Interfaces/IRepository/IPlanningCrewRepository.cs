using Airline_DE.Models.PlanningCrew;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IPlanningCrewRepository : IRepository<PlanningCrew>
    {
        Task<PlanningCrew> UpdateAsync(PlanningCrew entity);
    }
}
