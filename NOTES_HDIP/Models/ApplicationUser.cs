using Microsoft.AspNetCore.Identity;

namespace NOTES_HDIP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<NoteSpace>? NoteSpace { get; set; } = new List<NoteSpace>();
    }
}
