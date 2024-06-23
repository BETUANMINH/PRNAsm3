using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class PostCategory
    {
        [Key]
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

        // Navigation property
        public ICollection<Post> Posts { get; set; }
    }

}
