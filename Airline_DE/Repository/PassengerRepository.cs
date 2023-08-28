using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Passenger;

namespace Airline_DE.Repository
{
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        private readonly Context _context;

        public PassengerRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Passenger> UpdateAsync(Passenger entity)
        {
            _context.Passengers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
