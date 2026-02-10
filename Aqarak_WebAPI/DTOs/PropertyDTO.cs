namespace Aqarak_WebAPI.DTOs
{
    public class PropertyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Insurance { get; set; }
        public DateTime CreatedAt { get; set; }

        // Owner
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }

        // Category & Governorate
        public string CategoryName { get; set; }
        public string GovernorateName { get; set; }

        // Images
        public List<string> Images { get; set; }
        public int CategoryId { get; set; }
        public int GovernorateId { get; set; }
    }
}
