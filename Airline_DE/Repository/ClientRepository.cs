using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.Client;

namespace Airline_DE.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly Context _context;

        public ClientRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Client> UpdateAsync(Client entity)
        {
            _context.Clients.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
