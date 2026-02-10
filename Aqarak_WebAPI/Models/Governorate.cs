namespace Aqarak_WebAPI.Models
{
    public class Governorate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
