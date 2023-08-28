using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.Office;

namespace Airline_DE.Repository
{
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        private readonly Context _context;

        public OfficeRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Office> UpdateAsync(Office entity)
        {
            _context.Offices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
