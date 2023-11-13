using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.ChatMessage;

namespace Airline_DE.Repository
{
    public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
    {
        private readonly Context _context;

        public ChatMessageRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<ChatMessage> UpdateAsync(ChatMessage entity)
        {
            _context.ChatMessages.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
