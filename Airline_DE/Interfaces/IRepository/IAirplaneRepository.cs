using Airline_DE.Models.Airplane;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IAirplaneRepository : IRepository<Airplane>
    {
        Task<Airplane> UpdateAsync(Airplane entity);
    }
}
