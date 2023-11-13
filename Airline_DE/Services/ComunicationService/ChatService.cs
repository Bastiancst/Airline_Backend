using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.ChatMessage;

namespace Airline_DE.Services.ComunicationService
{
    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid clientId, Guid employeeId)
        {
            var messages = await _chatMessageRepository.GetAllAsync(x => x.ClientId == clientId && x.EmployeeId == employeeId);
            return messages.OrderByDescending(x => x.TimesTamp).ToList();
        }

        public async Task SendMessageAsync(Guid clientId, Guid employeeId, string message)
        {
            var newMessage = new ChatMessage
            {
                ClientId = clientId,
                EmployeeId = employeeId,
                Message = message,
                TimesTamp = DateTime.Now
            };

            await _chatMessageRepository.CreateAsync(newMessage);
        }
    }
}
