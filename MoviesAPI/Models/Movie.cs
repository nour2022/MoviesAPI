namespace MoviesAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public int Year { get; set; }
        public float Rate { get; set; }
        public byte[] Poster { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}
