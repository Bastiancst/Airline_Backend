using Airline_DE.Models.Airplane;
using Airline_DE.Models.Client;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> UpdateAsync(Client entity);
    }
}
