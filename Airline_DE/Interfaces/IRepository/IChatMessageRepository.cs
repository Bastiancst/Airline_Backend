using Airline_DE.Models.Airplane;
using Airline_DE.Models.ChatMessage;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IChatMessageRepository : IRepository<ChatMessage>
    {
        Task<ChatMessage> UpdateAsync(ChatMessage entity);
    }
}
