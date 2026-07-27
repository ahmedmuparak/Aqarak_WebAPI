namespace Aqarak_WebAPI.Repository
{
    public class MessageRepository: IMessageRepository
    {
        private readonly APIContext context;

        public MessageRepository(APIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Message>> GetConversationMessagesAsync (int conversationId)
        {
            return await context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.SentAt)
                .ToArrayAsync();
        }

        public async Task AddAsync(Message message)
        {
            await context.Messages.AddAsync(message);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
