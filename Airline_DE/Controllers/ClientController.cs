using Airline_DE.Interfaces;
using Airline_DE.Models.Client.DTOs;
using Airline_DE.Models.Employee.DTOs;
using Airline_DE.Services.CRUDService;
using Microsoft.AspNetCore.Mvc;

namespace Airline_DE.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _clientService.GetAllAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var result = await _clientService.GetByIdAsync(Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateClientDTO request)
        {
            var result = await _clientService.CreateAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateClientDTO request, string id)
        {
            var result = await _clientService.UpdateAsync(request, Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var result = await _clientService.DeleteAsync(Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
