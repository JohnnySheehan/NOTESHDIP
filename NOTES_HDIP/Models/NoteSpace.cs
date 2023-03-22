namespace NOTES_HDIP.Models
{
    public class NoteSpace
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? UserID { get; set; }
        public ApplicationUser? User { get; set; }

        public List<Note>? Notes { get; set;} = new List<Note>();
    }
}
