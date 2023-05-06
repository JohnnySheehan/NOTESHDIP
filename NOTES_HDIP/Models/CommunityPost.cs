namespace NOTES_HDIP.Models
{
    public class CommunityPost
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public string? UserID { get; set; }
        public ApplicationUser? User { get; set;  }

    }
}
