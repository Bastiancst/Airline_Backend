using Airline_DE.Models.Receiver;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IReceiverRepository : IRepository<Receiver>
    {
        Task<Receiver> UpdateAsync(Receiver entity);
    }
}
