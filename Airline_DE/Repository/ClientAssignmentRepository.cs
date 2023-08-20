using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.ClientAssingnment;

namespace Airline_DE.Repository
{
    public class ClientAssignmentRepository : Repository<ClientAssignment>, IClientAssignmentRepository
    {
        private readonly Context _context;

        public ClientAssignmentRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientAssignment> UpdateAsync(ClientAssignment entity)
        {
            _context.ClientAssignments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
