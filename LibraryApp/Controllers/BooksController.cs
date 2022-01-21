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
using AutoMapper.QueryableExtensions;
using LibraryApp.DataTransferObjects.Incoming;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public BooksController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        



        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            

            return await _context.Books
                .Include(x => x.Publishers)
                .Include(x => x.Categories)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }





        // GET: api/Books/5 With publishers,Categorys,Authors
        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookDto>> GetBook(long bookId)
        {
            var book = await _context.Books
                 .Include(x => x.Publishers)
                 .Include(x => x.Categories)
                 .Include(x => x.Authors)
                 .ThenInclude(x=> x.Person)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(x => x.Id == bookId); 
            

            if (book == null)
            {
                return NotFound();
            }

         
             var  bookDto= _mapper.Map<BookDto>(book);
             bookDto.Person = _mapper.Map<List<PersonDto>>(book.Authors.Where(x => x.Person != null).Select(x => x.Person)).ToList();

            return bookDto;

        }

        [HttpGet("GetBooksByYear/{bookYear}")] // get books by year

        public   async Task<ActionResult<IEnumerable<BookDto>>> GetBookByYear( long bookYear)
        {

            var books = await _context.Books 
               .Where(x => x.Year == bookYear)
               .Include(x => x.Publishers)
               .Include(x => x.Categories)
               .Include(x => x.Authors)
               .ThenInclude(x => x.Person)
               .AsNoTracking()
               .ToArrayAsync();

            return _mapper.Map<List<BookDto>>(books); 

            
        }


        [HttpGet("GetBooksByCategory/{categoryName}")] // get books by Category

        public async Task<ActionResult<IEnumerable<BookDto>>> GetBookByCategory(string categoryName)
        {

            var books = await _context.Categories
                .Include(x => x.Book)
                .ThenInclude(x => x.Publishers)
                .Include(x => x.Book)
                .ThenInclude(x => x.Authors)
                .ThenInclude(x => x.Person)
                .Where(x => x.Name == categoryName)?
                .AsNoTracking()
                .Select(x => x.Book).ToArrayAsync();

            return _mapper.Map<List<BookDto>>(books);

        }


        [HttpGet("GetBooksByAuthorLastNameAndFirstName/{authorLastName},{authorFirstName}")] // get books by author last- and firstname
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByAuthor(string authorLastName,string authorFirstName)
        {
            var book = await _context.Author
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Categories)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Publishers)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Authors)
                    .ThenInclude(x => x.Person)
                    .Where(x => x.Person.LastName == authorLastName && x.Person.FirstName == authorFirstName)
                    .AsNoTracking()
                    .Select(x =>  x.Book).ToArrayAsync();

                    return _mapper.Map<List<BookDto>>(book); 

        }

        [HttpGet("GetBooksByYearAndPublisher/{bookYear},{bookPublisher}")] // get books by bookyear and publisher
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByYearAndPublisher(long bookYear, string bookPublisher)
        {
            var book = await _context.Publishers
                    .Include(x => x.Books)
                    .ThenInclude(x => x.Categories)
                    .Include(x => x.Books)
                    .ThenInclude(x => x.Authors)
                    .ThenInclude(x => x.Person)
                    .Where(x => x.Name == bookPublisher && x.Books.Year == bookYear)
                    .AsNoTracking()
                    .Select(x => x.Books).ToArrayAsync();



            return _mapper.Map<List<BookDto>>(book);

        }


        [HttpGet("GetBooksByYearAndCategory/{bookYear},{bookCategory}")] // get books by bookyear and book category
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByYearAndCategory(long bookYear, string bookCategory)
        {
            var book = await _context.Categories
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Publishers)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Authors)
                    .ThenInclude(x => x.Person)
                    .Where(x => x.Name == bookCategory && x.Book.Year == bookYear)
                    .AsNoTracking()
                    .Select(x => x.Book).ToArrayAsync();

            return _mapper.Map<List<BookDto>>(book);

        }

        [HttpGet("GetBooksByLanguage/{bookLanguage}")] // get books language
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByLanguage(string bookLanguage)
        {
            var book = await _context.Books
                    .Include(x => x.Categories)
                    .Include(x => x.Publishers)
                    .Include(x => x.Authors)
                    .ThenInclude(x => x.Person)
                    .Where(x => x.Language == bookLanguage)
                    .AsNoTracking()
                    .ToArrayAsync();

            return _mapper.Map<List<BookDto>>(book);

        }





        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // update book information
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(long id, BookDtoIn book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

           

            try
            {
                var bookEntity = await _context.Books.FindAsync(id);


                bookEntity = _mapper.Map(book, bookEntity);


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // post  new book
        //WHEN POSTING DONT EDIT BASEDTO LEAVE IT ZERO AND IT GENERATES OWND ID
        [HttpPost]
        public async Task<ActionResult<BookDtoIn>> PostBook(BookDtoIn book)
        {
            var entityBook = _mapper.Map<Book>(book);

            _context.Books.Add(entityBook);
            
            

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostBook", new { id = entityBook.Id }, book);
        }

   

        private bool BookExists(long id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
