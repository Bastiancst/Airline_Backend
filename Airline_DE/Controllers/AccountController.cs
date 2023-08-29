using Airline_DE.Interfaces;
using Airline_DE.Models.Email.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Airline_DE.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailServices _emailServices;

        public AccountController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost]
        public async Task<IActionResult> TestEmail([FromBody] EmailServiceDTO request)
        {
            return Ok(await _emailServices.SendEmailContactUserAsync(request));
        }
    }
}
