using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.Airport;

namespace Airline_DE.Repository
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        private readonly Context _context;

        public AirportRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Airport> UpdateAsync(Airport entity)
        {
            _context.Airports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
