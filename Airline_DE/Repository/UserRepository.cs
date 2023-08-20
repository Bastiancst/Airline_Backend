using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.User;

namespace Airline_DE.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
