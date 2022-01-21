using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Book : BaseModel
    {

        
        public string Name { get; set; } // book name

        public string Language { get; set; } // book language
        public string? Description { get; set; } // book descripion

        public long Year { get; set; } // book year

        public string? AdditionalInformation { get; set; } // book additionalinformation
        
        public string Isbn { get; set; } // book isbn number serial
        
        public long? NumberOfCopies { get; set; } // nubmer of copies

        public ICollection<Author> Authors { get; set; } // books Authors

        public ICollection<Category> Categories { get; set; } // books categories

        public ICollection<Lending> Lendings { get; set; } // book lendings

        public ICollection<Publisher> Publishers { get; set; } // book publishers





    }
}
