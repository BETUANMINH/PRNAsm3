using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class PostCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<Post> Posts { get; set; }
    }

}
