using Airline_DE.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Airline_DE.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(Guid clientId, Guid employeeId, string message)
        {
            await _chatService.SendMessageAsync(clientId, employeeId, message);
            await Clients.All.SendAsync("ReceiveMessage", clientId, employeeId, message);
        }
    }
}
