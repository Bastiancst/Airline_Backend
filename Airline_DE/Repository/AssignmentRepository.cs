using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.Assignment;

namespace Airline_DE.Repository
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private readonly Context _context;

        public AssignmentRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Assignment> UpdateAsync(Assignment entity)
        {
            _context.Assignments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
