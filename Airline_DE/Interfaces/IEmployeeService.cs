using Airline_DE.Models.Employee;
using Airline_DE.Models.Employee.DTOs;
using Airline_DE.Wrappers;

namespace Airline_DE.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse<Employee>> CreateAsync(CreateEmployeeDTO employee);
        Task<ApiResponse<Employee>> GetByIdAsync(Guid employeeId);
        Task<ApiResponse<IEnumerable<Employee>>> GetAllAsync();
        Task<ApiResponse<Employee>> UpdateAsync(UpdateEmployeeDTO employee, Guid id);
        Task<ApiResponse<Guid>> DeleteAsync(Guid employeeId);
    }
}
