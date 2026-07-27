namespace Aqarak_WebAPI.Interfaces
{
    public interface IMessageService
    {
        Task SendMessageAsync(int conversationId, string senderId, string content);
        Task<IEnumerable<MessageDTO>> GetMessagesAsync(int conversationId);
    }
}
