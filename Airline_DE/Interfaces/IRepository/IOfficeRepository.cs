using Airline_DE.Models.Office;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IOfficeRepository : IRepository<Office>
    {
        Task<Office> UpdateAsync(Office entity);
    }
}
