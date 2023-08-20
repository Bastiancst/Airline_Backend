using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.PassengerAsignment;

namespace Airline_DE.Repository
{
    public class PassengerAsignmentRepository : Repository<PassengerAsignment>, IPassengerAsignmentRepository
    {
        private readonly Context _context;

        public PassengerAsignmentRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<PassengerAsignment> UpdateAsync(PassengerAsignment entity)
        {
            _context.PassengerAsignments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
