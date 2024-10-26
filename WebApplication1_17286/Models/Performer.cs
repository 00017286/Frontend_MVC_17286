namespace WebApplication1_17286.Models
{
    public class Performer
    {
        public int Id { get; set; }               // performer's ID
        public string Name { get; set; }          // performer's name
        public string Genre { get; set; }         // performer's main genre
        public DateTime BirthDate { get; set; }   // performer's DoB
        public string Country { get; set; }       // performer's country
    }
}
