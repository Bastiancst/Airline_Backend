using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.PlanningCrew;

namespace Airline_DE.Repository
{
    public class PlanningCrewRepository : Repository<PlanningCrew>, IPlanningCrewRepository
    {
        private readonly Context _context;

        public PlanningCrewRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<PlanningCrew> UpdateAsync(PlanningCrew entity)
        {
            _context.PlanningCrews.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
