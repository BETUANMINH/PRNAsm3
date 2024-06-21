using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class Post : PostBase
    {
        [Key]
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public PublishStatus PublishStatus { get; set; }
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
