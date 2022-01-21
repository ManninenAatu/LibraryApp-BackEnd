using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Outgoing
{
    public class CustomerDto : BaseDto
    {
        public string FirstName { get; set; } // customer firstname

        public string LastName { get; set; } // customer lastname

        public string PhoneNumber { get; set; } // customer phonenumber

        public string Email { get; set; } // customer email

        public string Address { get; set; } // customer address

        public List<LendingDto> Lendings { get; set; } // customer lendgis

        public bool HaveLoanBan { get; set; } // customer have or not have loan ban

    }

    public class CustomerDtoProfile : Profile
    {
        public CustomerDtoProfile()
        {
            CreateMap<Models.Customer, CustomerDto>().ReverseMap();
        }
    }

}
