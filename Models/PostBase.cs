namespace HE176084_MinhBT_A3.Models
{
    public class PostBase
    {
        public int AuthorID { get; set; }
        public AppUser Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
