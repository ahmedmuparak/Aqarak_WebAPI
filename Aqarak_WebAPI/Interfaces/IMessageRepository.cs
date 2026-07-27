namespace Aqarak_WebAPI.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetConversationMessagesAsync(int conversationId);
        Task AddAsync(Message message);
        Task SaveAsync();
    }
}
