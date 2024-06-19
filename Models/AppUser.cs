using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class AppUser
    {
        [Key]
        public int UserID { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation property
        public ICollection<Post> Posts { get; set; }
    }

}
