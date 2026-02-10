namespace Aqarak_WebAPI.DTOs
{
    public class CreatePropertyDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Insurance { get; set; }
        public DateTime CreatedAt { get; set; }

        // Category & Governorate
        public int CategoryId { get; set; }
        public int GovernorateId { get; set; }

        // Images
        public List<IFormFile> Images { get; set; }
    }
}
