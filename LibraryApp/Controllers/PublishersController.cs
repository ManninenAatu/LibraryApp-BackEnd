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
    public class PublishersController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public PublishersController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       




        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
        {
            return await    _context.Publishers
                           .Include(x => x.Books)
                           .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
                           .AsNoTracking()
                           .ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{publisherId}")]
        public async Task<ActionResult<PublisherDto>> GetPublisher(long publisherId)
        
        {
            var category = await _context.Publishers
                                  .Include(x => x.Books)
                                  .AsNoTracking()
                                  .SingleOrDefaultAsync(x => x.Id == publisherId);



            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<PublisherDto>(category);

        }

        [HttpGet("GetPublisherByBookName/{bookName}")] // get Publisher by bookName
        public async Task<ActionResult<IEnumerable<string>>> GetCategoryByBookName(string bookName)
        {
            var publishers = await _context.Publishers
                    .Include(x => x.Books)
                    .Where(x => x.Books.Name == bookName)
                    .Select(x => x.Name)
                    .AsNoTracking()
                    .ToListAsync();

            return publishers;

        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // update publisher information
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(long id, PublisherDtoIn publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

           

            try
            {
                var publisherEntity = await _context.Publishers.FindAsync(id);


                publisherEntity = _mapper.Map(publisher, publisherEntity);


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // post new publisher 
        //WHEN POSTING DONT EDIT BASEDTO LEAVE IT ZERO AND IT GENERATES OWND ID
        [HttpPost]
        public async Task<ActionResult<PublisherDtoIn>> PostPublisher(PublisherDtoIn publisher)
        {
            var entityPublisher = _mapper.Map<Publisher>(publisher);

            _context.Publishers.Add(entityPublisher);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostPublisher", new { id = entityPublisher.Id }, publisher);
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(long id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(long id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}
