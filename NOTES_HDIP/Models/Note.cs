namespace NOTES_HDIP.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NoteField { get; set; } = string.Empty;

        public int NoteSpaceID { get; set; }
        public NoteSpace? NoteSpaces { get; set; }
    }
}
