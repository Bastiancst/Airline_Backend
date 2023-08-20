using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.FlightPlanning;

namespace Airline_DE.Repository
{
    public class FlightPlanningRepository : Repository<FlightPlanning>, IFlightPlanningRepository
    {
        private readonly Context _context;

        public FlightPlanningRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<FlightPlanning> UpdateAsync(FlightPlanning entity)
        {
            _context.FlightPlannings.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
