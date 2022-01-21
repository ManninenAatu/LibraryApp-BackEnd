using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;
using LibraryApp.DataTransferObjects.Outgoing;
using AutoMapper;
using LibraryApp.DataTransferObjects.Incoming;
using AutoMapper.QueryableExtensions;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public CustomersController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            return await _context.Customers
                         .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                         .AsNoTracking()
                         .ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(long customerId)
        {
            var customer = await _context.Customers
                                  .AsNoTracking()
                                 .SingleOrDefaultAsync(x => x.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // update customers
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, CustomerDtoIn customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            

            try
            {
                var customerEntity = await _context.Customers.FindAsync(id);


                customerEntity = _mapper.Map(customer, customerEntity);


                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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





        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // post new customer
        //WHEN POSTING DONT EDIT BASEDTO LEAVE IT ZERO AND IT GENERATES OWND ID
        [HttpPost]
        public async Task<ActionResult<CustomerDtoIn>> PostCustomer(CustomerDtoIn customer)
        {
            var entityCustomer = _mapper.Map<Customer>(customer);
            
            _context.Customers.Add(entityCustomer);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostCustomer", new { id = entityCustomer.Id }, customer);
        }


        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 // update customers
        [HttpPut("{customerId}")]
        public async Task<IActionResult> PostCustomerByEmployee(long customerId,  CustomerDtoIn customer)
        {
            if (customerId != customer.Id)
            {
                return BadRequest();
            }


            try
            {
                

                var customerEntity = await _context.Customers.FindAsync(customerId);


                customerEntity = _mapper.Map(customer, customerEntity);


                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customerId))
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

        // DELETE: api/Customers/5 // delete customer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
