namespace Aqarak_WebAPI.Interfaces
{
    public interface IConversationService
    {
        Task<int> CreateOrGetConversationAsync(int propertyId, string customerId);
        Task<IEnumerable<ConversationDTO>> GetUserConversationsAsync(string userId);
        Task<ConversationDTO?> GetConversationAsync(int conversationId, string currentUserId);
        Task<bool> DeleteConversationAsync(int conversationId, string userId);
    }
}
