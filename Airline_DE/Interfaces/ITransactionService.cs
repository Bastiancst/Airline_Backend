using Airline_DE.Models.Payment.DTOs;

namespace Airline_DE.Interfaces
{
    public interface ITransactionService
    {
        Task<CreateTransactionResponseDTO> CreateTransaction(CreateTransactionRequestDTO request);
    }
}
