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
    public class CategoriesController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return await _context.Categories
                           .Include(x => x.Book)
                           .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                           .AsNoTracking()
                           .ToListAsync();

        }

         // GET: api/Categories/5
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(long categoryId)
        {
            var category = await _context.Categories
                                  .Include(x => x.Book)
                                  .AsNoTracking()
                                  .SingleOrDefaultAsync(x => x.Id == categoryId);
                
                                 

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryDto>(category);

        }

        [HttpGet("GetCategorysByBookName/{bookName}")] // get categoryName by bookName
       
            public async Task<ActionResult<IEnumerable<string>>> GetBookByCategory(string bookName)
            {

            var category = await _context.Categories
                     .Include(x => x.Book)
                     .Where(x => x.Book.Name == bookName)
                     .AsNoTracking()
                     .Select(x => x.Name).ToListAsync();



            return category;
        }


        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // update categories
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(long id, CategoryDtoIn category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            

            try
            {
                var categoryEntity = await _context.Categories.FindAsync(id);


                categoryEntity = _mapper.Map(category, categoryEntity);


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // post new category
        //WHEN POSTING DONT EDIT BASEDTO LEAVE IT ZERO AND IT GENERATES OWND ID
        [HttpPost]
        
        public async Task<ActionResult<CategoryDtoIn>> PostCategory (CategoryDtoIn category)
        {
            var entityCategory = _mapper.Map<Category>(category);
            
            _context.Categories.Add(entityCategory);
            
            await _context.SaveChangesAsync();
            

            return CreatedAtAction("PostCategory", new { id = entityCategory.Id }, category);
        }


        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(long id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
