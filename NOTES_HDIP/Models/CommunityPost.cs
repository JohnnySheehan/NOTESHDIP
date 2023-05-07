using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NOTES_HDIP.Models
{
    public class CommunityPost
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public string? UserID { get; set; }
        public ApplicationUser? User { get; set;  }

    }
}
