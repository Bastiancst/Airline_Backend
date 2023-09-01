using Airline_DE.Interfaces;
using Airline_DE.Models.Employee.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Airline_DE.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeService.GetAllAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var result = await _employeeService.GetByIdAsync(Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO request)
        {
            var result = await _employeeService.CreateAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDTO request, string id)
        {
            var result = await _employeeService.UpdateAsync(request, Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var result = await _employeeService.DeleteAsync(Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
