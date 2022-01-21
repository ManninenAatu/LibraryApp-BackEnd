using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;
using AutoMapper;
using LibraryApp.DataTransferObjects.Outgoing;
using LibraryApp.DataTransferObjects.Incoming;
using AutoMapper.QueryableExtensions;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthor()
        {
            return await _context.Author
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{authorId}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(long authorId)
        {
            var author = await _context.Author
                  .Include(x => x.Person)
                  .Include(x => x.Book)
                  .Where(x => x.Id == authorId)
                  .Select(x => x.Person)
                  .AsNoTracking()
                  .ToListAsync();


            if (author == null)
            {
                return NotFound();
            }


            var authorDto = _mapper.Map<AuthorDto>(author);
           
            

            return authorDto;
        }

        // GET: api/Actors/GetBookAuthor
        [HttpGet("GetAuthorsByBookName/{bookName}")] // get authors by book Name
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetMovieActors(string bookName)
        {
            var bookAuthor = await _context.Author
                .Include(x => x.Book)
                .Include(x => x.Person)
                .Where(x => x.Book.Name == bookName)
                .AsNoTracking()
                .OrderBy(x => x.Person.LastName).ToListAsync();

            return _mapper.Map<List<AuthorDto>>(bookAuthor);
        }

        

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(long id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(long id)
        {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
