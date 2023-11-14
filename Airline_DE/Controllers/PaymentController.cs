using Airline_DE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Airline_DE.Models.Payment.DTOs;
using Airline_DE.Wrappers;

namespace Airline_DE.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class PaymentController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public PaymentController (ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromQuery] int amount, [FromQuery] Guid clientId)
        {
            var result = await _transactionService.CreateTransaction(new CreateTransactionRequestDTO
            {
                BuyOrder = "ABC123",
                Amount = amount,
                SessionId = Guid.NewGuid().ToString(),
                ReturnUrl = ""

            });

            return Ok(new ApiResponse<CreateTransactionResponseDTO>(result));
        }
    }
}
