using System.ComponentModel.DataAnnotations;

namespace Aqarak_WebAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Property> Properties { get; set; }

    }
}
