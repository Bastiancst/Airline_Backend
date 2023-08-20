using Airline_DE.Models.Airplane;
using Airline_DE.Models.Employee;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> UpdateAsync(Employee entity);
    }
}
