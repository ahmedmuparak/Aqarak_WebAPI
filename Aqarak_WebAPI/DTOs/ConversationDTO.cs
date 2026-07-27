namespace Aqarak_WebAPI.DTOs
{
    public class ConversationDTO
    {
        public int ConversationId { get; set; }

        public string ReceiverName { get; set; }

        public List<MessageDTO> Messages { get; set; } = new();
    }
}
