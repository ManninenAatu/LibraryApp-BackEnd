using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Customer : BaseModel
    {

        
        public string FirstName { get; set; } // customer firstname
       
        public string LastName { get; set; } // customer lastname
        
        public string PhoneNumber { get; set; } // customer phonenumber
        
        public string Email { get; set; } // customer email
        
        public string Address { get; set; } // customer address

        public ICollection<Lending> Lendings { get; set; } // customer lendings

        public bool HaveLoanBan { get; set; } // customer have or not have loan ban


    }
}
