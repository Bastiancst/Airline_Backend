using Airline_DE.Models.ChatMessage;

namespace Airline_DE.Interfaces
{
    public interface IChatService
    {
        Task SendMessageAsync(Guid clientId, Guid employeeId, string message);
        Task<List<ChatMessage>> GetMessagesAsync(Guid clientId, Guid employeeId);
    }
}
