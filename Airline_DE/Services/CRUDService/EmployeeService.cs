using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Employee;
using Airline_DE.Models.Employee.DTOs;
using Airline_DE.Repository;
using Airline_DE.Wrappers;

namespace Airline_DE.Services.CRUDService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ApiResponse<Employee>> CreateAsync(CreateEmployeeDTO request)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                OfficeId= Guid.NewGuid(),
                Rut= request.Rut,
                Name = request.Name,
                LastName= request.LastName,
                Age= request.Age,
                Email= request.Email,
                Role = request.Role,
                WorkPosition= request.WorkPosition,
                Country= request.Country,
                City= request.City,
                Bonus= request.Bonus,
            };

            await _employeeRepository.CreateAsync(employee);

            return new ApiResponse<Employee>(employee);
        }

        public async Task<ApiResponse<Employee>> GetByIdAsync(Guid employeeId)
        {
            var result = await _employeeRepository.GetAsync(u => u.Id == employeeId);

            return new ApiResponse<Employee>(result);
        }

        public async Task<ApiResponse<IEnumerable<Employee>>> GetAllAsync()
        {
            var result = await _employeeRepository.GetAllAsync();

            return new ApiResponse<IEnumerable<Employee>>(result);

        }

        public async Task<ApiResponse<Employee>> UpdateAsync(UpdateEmployeeDTO request, Guid id)
        {
            var result = await _employeeRepository.GetAsync(u => u.Id == id);

            if (request == null)
            {
                return new ApiResponse<Employee>("User not found");
            }

            result.Rut = request.Rut;
            result.Name= request.Name;
            result.LastName = request.LastName;
            result.Age = request.Age;
            result.Email = request.Email;
            result.WorkPosition = request.WorkPosition;
            result.Role= request.Role;
            result.Country= request.Country;
            result.City = request.City;
            result.Bonus = request.Bonus;

            await _employeeRepository.UpdateAsync(result);

            return new ApiResponse<Employee>(result);
        }

        public async Task<ApiResponse<Guid>> DeleteAsync(Guid id)
        {
            var result = await _employeeRepository.GetAsync(u => u.Id == id);

            await _employeeRepository.RemoveAsync(result);

            return new ApiResponse<Guid>(result.Id);
        }
    }
}
