using MoviesAPI.Models;

namespace MoviesAPI.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Story { get; set; }
        public IFormFile Poster { get; set; }
        public int Year { get; set; }
        public float Rate { get; set; }
        public int GenreId { get; set; }
    }
}
