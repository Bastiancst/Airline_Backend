using Airline_DE.Models.Airplane;
using Airline_DE.Models.Assignment;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<Assignment> UpdateAsync(Assignment entity);
    }
}
