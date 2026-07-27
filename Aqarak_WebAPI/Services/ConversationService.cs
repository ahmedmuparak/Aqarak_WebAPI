using Aqarak_WebAPI.Interfaces;
using Aqarak_WebAPI.Repository;

namespace Aqarak_WebAPI.Services
{
    public class ConversationService: IConversationService
    {
        private readonly IConversationRepository conversationRepository;

        public APIContext context { get; }

        public ConversationService(IConversationRepository conversationRepository, APIContext context)
        {
            this.conversationRepository = conversationRepository;
            this.context = context;
        }

        public async Task<int> CreateOrGetConversationAsync(int propertyId, string customerId)
        {
            var Conversation = await conversationRepository.GetConversation(propertyId, customerId);

            if (Conversation != null)
                return Conversation.Id;

            var OwnerId = await conversationRepository.GetOwnerIdByPropertyAsync(propertyId);

            var newConversation = new Conversation
            {
                PropertyId = propertyId,
                CustomerId = customerId,
                OwnerId = OwnerId,
                CreatedAt = DateTime.UtcNow
            };

            await conversationRepository.AddConversationAsync(newConversation);
            await conversationRepository.SaveAsync();

            return newConversation.Id;
        }

        public async Task<IEnumerable<ConversationDTO>> GetUserConversationsAsync(string userId)
        {
            var conversations = await conversationRepository.GetUserConversationsAsync(userId);

            return conversations.Select(c => new ConversationDTO
            {
                ConversationId = c.Id,
                ReceiverName = c.CustomerId == userId
                    ? c.Owner.FullName
                    : c.Customer.FullName
            });
        }

        public async Task<ConversationDTO?> GetConversationAsync(int conversationId, string currentUserId)
        {
            var conversation = await conversationRepository.GetByIdAsync(conversationId);

            if (conversation == null)
                return null;

            var receiverName = conversation.OwnerId == currentUserId
                ? conversation.Customer.FullName
                : conversation.Owner.FullName;

            return new ConversationDTO
            {
                ConversationId = conversation.Id,

                ReceiverName = receiverName,

                Messages = conversation.Messages
                    .OrderBy(m => m.SentAt)
                    .Select(m => new MessageDTO
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        Content = m.Content,
                        SentAt = m.SentAt,
                        IsMine = m.SenderId == currentUserId

                    }).ToList()
            };

        }

        public async Task<bool> DeleteConversationAsync(int conversationId, string userId)
        {
            var conversation = await context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c =>
                    c.Id == conversationId &&
                    (c.OwnerId == userId || c.CustomerId == userId));

            if (conversation == null)
                return false;

            context.Messages.RemoveRange(conversation.Messages);
            context.Conversations.Remove(conversation);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
