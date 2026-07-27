using Aqarak_WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Aqarak_WebAPI.Services
{
    public class MessageService: IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IHubContext<ChatHub> hubContext;

        public MessageService(IMessageRepository messageRepository ,IHubContext<ChatHub> hubContext)
        {
            this.messageRepository = messageRepository;
            this.hubContext = hubContext;
        }

        public async Task SendMessageAsync(int conversationId,string senderId,string content)
        {
            var message = new Message
            {
                ConversationId = conversationId,
                SenderId = senderId,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            await messageRepository.AddAsync(message);
            await messageRepository.SaveAsync();

            await hubContext.Clients
                     .Group($"Conversation-{conversationId}")
                     .SendAsync(
                         "ReceiveMessage",
                         new
                         {
                             SenderId = senderId,
                             Content = content,
                             Time = DateTime.Now.ToString("HH:mm")
                         }
                     );
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesAsync(int conversationId)
        {
            var messages = await messageRepository
                .GetConversationMessagesAsync(conversationId);

            return messages.Select(m => new MessageDTO
            {
                Id = m.Id,
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt
            });
        }
    }
}
