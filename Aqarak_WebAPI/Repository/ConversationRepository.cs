using Aqarak_WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aqarak_WebAPI.Repository
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly APIContext context;

        public ConversationRepository(APIContext context)
        {
            this.context = context;
        }

        public async Task<Conversation?> GetByIdAsync(int Id)
        {
            return await context.Conversations
                .Include(c =>c.Messages)
                .ThenInclude(m =>m.Sender)
                .Include(c =>c.Owner)
                .Include(c =>c.Customer)
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Conversation?> GetConversation(int propertyId, string customerId)
        {
            return await context.Conversations
               .Include(c => c.Messages)
               .ThenInclude(m => m.Sender)
               .Include(c => c.Owner)
               .Include(c => c.Customer)
               .FirstOrDefaultAsync(c => c.PropertyId == propertyId && c.CustomerId == customerId);
        }

        public async Task<IEnumerable<Conversation>> GetUserConversationsAsync(string userId)
        {
            return await context.Conversations
                .Include(c => c.Owner)
                .Include(c => c.Customer)
                .Include(c => c.Property)
                .Where(c => c.CustomerId == userId || c.OwnerId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<string> GetOwnerIdByPropertyAsync(int propertyId)
        {
            return await context.Properties
                .Where(p => p.Id == propertyId)
                .Select(p => p.UserId)
                .FirstAsync();
        }

        public async Task AddConversationAsync(Conversation conversation)
        {
            await context.Conversations.AddAsync(conversation);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
