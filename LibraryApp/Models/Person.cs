using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Person : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Author Author { get; set; }

 

    }
 

}
