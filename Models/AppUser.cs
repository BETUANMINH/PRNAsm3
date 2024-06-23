using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class AppUser
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Navigation property
        public ICollection<Post> Posts { get; set; }
    }

}
