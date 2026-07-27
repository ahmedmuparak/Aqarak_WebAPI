namespace Aqarak_WebAPI.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }
        [ForeignKey(nameof(PropertyId))]

        public Property Property { get; set; }
        [Required]
        public string CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public AppUser Customer { get; set; }
        [Required]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public AppUser Owner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
