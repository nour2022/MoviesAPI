using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto genreDto)
        {
            var genre = new Genre() { Name = genreDto.Name };
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return Ok(genre);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int id, GenreDto genreDto)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound($" the genre with id = {id} is not Found");

            }
            genre.Name = genreDto.Name;
            _context.SaveChanges();
            return Ok(genre);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound($" the genre with id = {id} is not Found");

            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return Ok(genre);

        }
    }
}
