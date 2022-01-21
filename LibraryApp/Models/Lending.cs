using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Lending : BaseModel
    {
        
        

        public long BookId { get; set; }

        public Book Books { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime LendedDate { get; set; }
        
        public DateTime ReturnedDate { get; set; }


    }

}
