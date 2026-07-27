namespace Aqarak_WebAPI.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(int Id);
        Task<Conversation?> GetConversation(int propertyId, string customerId);
        Task<IEnumerable<Conversation>> GetUserConversationsAsync(string userId);
        Task<string> GetOwnerIdByPropertyAsync(int propertyId);
        Task AddConversationAsync(Conversation conversation);
        Task SaveAsync();
    }
}
