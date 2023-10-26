using Airline_DE.Interfaces;
using Airline_DE.Models.Assignment;
using Airline_DE.Models.Assignment.DTOs;
using Airline_DE.Models.Employee.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Airline_DE.Controllers
{
    [ApiController]
    [Route("api/assignment")]
    public class AssignmentController: ControllerBase
    {

        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssignment([FromQuery]Guid clientId)
        {
            var result = await _assignmentService.GetAllAsync(clientId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAssignment([FromQuery] Guid id)
        {
            var result = await _assignmentService.GetByIdAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentDTO request, [FromQuery] Guid clienId)
        {
            var result = await _assignmentService.CreateAsync(request, clienId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
