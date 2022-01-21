using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Publisher : BaseModel
    {
        public string Name { get; set; } // Publisher name

        public string Where { get; set; } //  Publisher place 

        public Book Books { get; set; }

        public long BookId { get; set; }
    }
}
