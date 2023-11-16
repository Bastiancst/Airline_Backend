using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Assignment.DTOs;
using Airline_DE.Models.Email.DTOs;
using Airline_DE.Models.Passenger.DTOs;
using Airline_DE.Services.CRUDService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Airline_DE.Controllers
{
    [ApiController]
    [Route("api/passenger")]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _passengerService;
        private readonly IEmailServices _emailService;
        private readonly IClientRepository _clientRepository;

        public PassengerController(IPassengerService passengerService, IEmailServices emailService)
        {
            _passengerService = passengerService;
            _emailService = emailService;
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
                var getClient = await _clientRepository.GetAsync(x => x.Id == result.Result);
                await _emailService.SendBasicEmailAsync(new BasicEmailRequestDTO
                {
                    Subject = "Su compra se ha realizado con exito!",
                    Message = $"Hello! {getClient.Name} Welcome to Tesseract Ecosystem! \n please confirm your account with the following URL: \n",
                    ReceiverEmail = $"{getClient.Email}",
                    ReceiverName = $"{getClient.Name}"
                });
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
