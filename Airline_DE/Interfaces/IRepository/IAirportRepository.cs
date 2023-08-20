using Airline_DE.Models.Airplane;
using Airline_DE.Models.Airport;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> UpdateAsync(Airport entity);
    }
}
