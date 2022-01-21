using AutoMapper;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Outgoing
{
    public class LendingDto : BaseDto
    {
        public long BookId { get; set; }

        public Book Books { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime LendedDate { get; set; }

        public DateTime ReturnedDate { get; set; }

    }

    public class LendingDtoProfile : Profile
    {
        public LendingDtoProfile()
        {
            CreateMap<Models.Lending, LendingDto>().ReverseMap();
        }
    }
}
