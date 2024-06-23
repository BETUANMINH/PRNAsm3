using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class Post : PostBase
    {
        [Key]
        [Required]
        public int PostID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public PublishStatus PublishStatus { get; set; } = PublishStatus.Draft;
        [Required]
        public int CategoryID { get; set; }

        // Navigation properties

        public PostCategory Category { get; set; }
    }
    public enum PublishStatus
    {
        Draft = 0,
        Published = 1,
    }
}
