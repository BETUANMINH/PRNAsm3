using System.ComponentModel.DataAnnotations;

namespace HE176084_MinhBT_A3.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public int AuthorID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool PublishStatus { get; set; }
        public int CategoryID { get; set; }

        // Navigation properties
        public AppUser Author { get; set; }
        public PostCategory Category { get; set; }
    }

}
