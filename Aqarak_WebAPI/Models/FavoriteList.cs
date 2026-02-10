namespace Aqarak_WebAPI.Models
{
    public class FavoriteList
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
