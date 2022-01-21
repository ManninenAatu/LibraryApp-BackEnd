using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; } // category name

        public long BookId { get; set; } 

        public Book Book { get; set; }


    }
}
