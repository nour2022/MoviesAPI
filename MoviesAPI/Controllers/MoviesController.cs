using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private string[] _allowedExtentions = new string []{ ".png", ".jpg" };
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var Movies = await _context.Movies
                .Include(m=>m.Genre)
                .Select(m=> new
                        {
                            m.Title,m.Year, m.Genre.Name,m.Rate,m.Story,m.Poster
                        })
                .ToListAsync();
            return Ok(Movies);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound("Incorrect Id");
            return Ok(movie);
        }
        [HttpGet("genreId={Id}")]
        public async Task<IActionResult> GetByGenreID(int id)
        {
            var movies = _context.Movies
                .Where(m=>m.GenreId == id)
                .Select(m=> new
            {
                m.Id, m.Title, m.Year,m.Rate,m.Story,m.GenreId
            })
                .ToList();
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm]MovieDto movieDto)
        {
            if(!_allowedExtentions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
            {
                return BadRequest("Allowed extentions are : .jpg , .png ");
            }
            var DateStream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(DateStream);
            Movie movie = new Movie()
            {
                Title = movieDto.Title,
                Story = movieDto.Story,
                Poster = DateStream.ToArray(),
                Rate = movieDto.Rate,
                Year = movieDto.Year,
                GenreId = movieDto.GenreId
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }
        //[HttpPut("{Id}")]
        //public async Task<IActionResult> UpdateMovie(int id, MovieDto movieDto)
        //{
        //    var movie = _context.Movies.Find(id);
        //    if (movie == null) return NotFound();
        //    movie.Title = movieDto.Title;
        //    movie.Story = movieDto.Story;
        //    movie.Rate = movieDto.Rate;
        //    movie.Year = movieDto.Year;
        //    movie.GenreId = movieDto.GenreId;
        //    movie.Poster = movieDto.Poster;

        //}

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null) return NotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
