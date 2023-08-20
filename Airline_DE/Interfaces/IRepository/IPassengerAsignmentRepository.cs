using Airline_DE.Models.PassengerAsignment;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IPassengerAsignmentRepository : IRepository<PassengerAsignment>
    {
        Task<PassengerAsignment> UpdateAsync(PassengerAsignment entity);
    }
}
