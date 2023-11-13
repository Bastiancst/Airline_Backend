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

        [HttpPost("employee")]
        public async Task<IActionResult> GetEmployeebyClient([FromQuery] Guid id)
        {
            var result = await _clientService.GetEmployeebyClient(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("invoices")]
        public async Task<IActionResult> GetInvoicesbyClient([FromQuery] Guid id)
        {
            var result = await _clientService.GetInvoicesClient(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetClient(string id)
        {
            var result = await _clientService.GetByIdAsync(Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO request)
        {
            var result = await _clientService.CreateAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientDTO request, string id)
        {
            var result = await _clientService.UpdateAsync(request, Guid.Parse(id));

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteClient(string id)
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
