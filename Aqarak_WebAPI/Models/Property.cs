namespace Aqarak_WebAPI.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Insurance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }

        public ICollection<PropertyImage> Images { get; set; }
        public ICollection<FavoriteList> Favorites { get; set; }
    }
}
