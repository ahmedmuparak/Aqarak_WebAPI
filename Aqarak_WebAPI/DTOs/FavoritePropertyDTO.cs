namespace Aqarak_WebAPI.DTOs
{
    public class FavoritePropertyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
