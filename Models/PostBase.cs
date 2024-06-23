using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class PostBase
    {
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public AppUser Author { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
    }
}
