using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Incoming
{
    public class LendingDtoIn:BaseDto
    {

        public long BookId { get; set; }

        public long CustomerId { get; set; }

        public DateTime LendedDate { get; set; }

    }

    public class LendingDtoInProfile : Profile
    {
        public LendingDtoInProfile()
        {
            CreateMap<Models.Lending, LendingDtoIn>().ReverseMap();
        }
    }
}
