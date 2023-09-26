using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.User;

namespace Airline_DE.Repository
{
    public class RecoverPasswordRepository : Repository<RecoverPassword>, IRecoverPasswordRepository
    {
        private readonly Context _context;

        public RecoverPasswordRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<RecoverPassword> UpdateAsync(RecoverPassword entity)
        {
            _context.RecoverPasswords.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
