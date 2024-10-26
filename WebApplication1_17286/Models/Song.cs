namespace WebApplication1_17286.Models
{
    public class Song
    {
        public int Id { get; set; }               // song ID
        public string Title { get; set; }         // song name
        public string Genre { get; set; }         // song genre
        public int ReleaseYear { get; set; }      // song year of release
        public Performer SongPerformer { get; set; }      // song performer
    }
}
