namespace Aqarak_WebAPI.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsMine { get; set; }
    }
}
