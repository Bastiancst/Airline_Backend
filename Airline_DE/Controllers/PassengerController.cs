using Airline_DE.Interfaces;
using Airline_DE.Models.Assignment.DTOs;
using Airline_DE.Models.Passenger.DTOs;
using Airline_DE.Services.CRUDService;
using Microsoft.AspNetCore.Mvc;

namespace Airline_DE.Controllers
{
    [ApiController]
    [Route("api/passenger")]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _passengerService;

        public PassengerController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllAssignment([FromQuery] Guid clientId, [FromQuery] Guid flightPlanningId)
        {
            var result = await _passengerService.GetAllAsync(clientId, flightPlanningId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAssignment([FromBody] List<CreatePassengerDTO> request)
        {
            var result = await _passengerService.CreateAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
