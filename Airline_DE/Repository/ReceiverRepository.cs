using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.Receiver;

namespace Airline_DE.Repository
{
    public class ReceiverRepository : Repository<Receiver>, IReceiverRepository
    {
        private readonly Context _context;

        public ReceiverRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Receiver> UpdateAsync(Receiver entity)
        {
            _context.Receivers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
