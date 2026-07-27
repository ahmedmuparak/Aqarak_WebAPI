namespace Aqarak_WebAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
