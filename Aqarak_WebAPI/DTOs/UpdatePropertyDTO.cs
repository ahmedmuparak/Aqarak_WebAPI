namespace Aqarak_WebAPI.DTOs
{
    public class UpdatePropertyDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Insurance { get; set; }
        public int CategoryId { get; set; }
        public int GovernorateId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
