using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Incoming
{
    public class CustomerDtoIn:BaseDto
    {

        
        [Required]
        public string FirstName { get; set; } // customer firstname
        [Required]
        public string LastName { get; set; } // customer lastname
        [Required]
        public string PhoneNumber { get; set; } // customer phonenumber
        [Required]
        public string Email { get; set; } // customer email
        [Required]
        public string Address { get; set; } // customer address
        [Required]
        public bool HaveLoanBan { get; set; } // customer have or not have loan ban
    }
    public class CustomerDtoProfile : Profile
    {
        public CustomerDtoProfile()
        {
            CreateMap<Models.Customer, CustomerDtoIn>().ReverseMap();
        }
    }

}
