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
    public class PeopleController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public PeopleController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPerson()
        {
            return await _context.Person
                        .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, PersonDtoIn person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            

            try
            {
                var personEntity = await _context.Person.FindAsync(id);


                personEntity = _mapper.Map(person, personEntity);


                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //WHEN POSTING DONT EDIT BASEDTO LEAVE IT ZERO AND IT GENERATES OWND ID
        [HttpPost]
        public async Task<ActionResult<PersonDtoIn>> PostPerson(PersonDtoIn person)
        {
            var entityPerson = _mapper.Map<Person>(person);

            _context.Person.Add(entityPerson);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(long id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
