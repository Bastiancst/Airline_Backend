using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;

namespace Airline_DE.Repository
{
    public class AirplaneRepository : Repository<Airplane>, IAirplaneRepository
    {
        private readonly Context _context;

        public AirplaneRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Airplane> UpdateAsync(Airplane entity)
        {
            _context.Airplanes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
