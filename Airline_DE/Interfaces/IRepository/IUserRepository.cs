using Airline_DE.Models.User;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> UpdateAsync(User entity);  
    }
}
