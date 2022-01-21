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
    public class LendingsController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public LendingsController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        

        // GET: api/Lendings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LendingDto>>> GetLendings()
        {
            return await _context.Lendings
                         .ProjectTo<LendingDto>(_mapper.ConfigurationProvider)
                         .ToListAsync();
        }

        // GET: api/Lendings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lending>> GetLending(long id)
        {
            var lending = await _context.Lendings.FindAsync(id);

            if (lending == null)
            {
                return NotFound();
            }

            return lending;
        }

        // PUT: api/Lendings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLending(long id, LendingDtoIn lending)
        {
            if (id != lending.Id)
            {
                return BadRequest();
            }

            

            try
            {
                var LendingEntity = await _context.Lendings.FindAsync(id);


                LendingEntity = _mapper.Map(lending, LendingEntity);


                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LendingExists(id))
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

        // POST: api/Lendings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //WHEN POSTING DONT EDIT BASEDTO LEAVE IT ZERO AND IT GENERATES OWND ID
        [HttpPost]
        public async Task<ActionResult<LendingDtoIn>> PostLending(LendingDtoIn lending)
        {
            

            var lendingEntitity = _mapper.Map<Lending>(lending);

                

               _context.Lendings.Add(lendingEntitity);

               await _context.SaveChangesAsync();

               return CreatedAtAction("PostLending", new { id = lending.Id }, lending);



        }


       

        // DELETE: api/Lendings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLending(long id)
        {
            var lending = await _context.Lendings.FindAsync(id);
            if (lending == null)
            {
                return NotFound();
            }

            _context.Lendings.Remove(lending);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LendingExists(long id)
        {
            return _context.Lendings.Any(e => e.Id == id);
        }
    }
}
