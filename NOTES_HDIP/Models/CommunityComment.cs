namespace NOTES_HDIP.Models
{
    public class CommunityComment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int PostId { get; set; }
        public CommunityPost? CommunityPost { get; set; }
        public string? UserID { get; set; }

        public ApplicationUser? User { get; set; }

    }
}
