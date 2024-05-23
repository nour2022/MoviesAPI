using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
