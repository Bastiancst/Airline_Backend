using Airline_DE.Models.Passenger;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Task<Passenger> UpdateAsync(Passenger entity);
    }
}
