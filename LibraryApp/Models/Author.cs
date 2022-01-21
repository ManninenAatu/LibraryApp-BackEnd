using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Author : BaseModel
    {
        public long PersonId { get; set; } // author personId

        public Person Person { get; set; }

        public long BookId { get; set; } // bookId

        public Book Book { get; set; }

       
    }
}
