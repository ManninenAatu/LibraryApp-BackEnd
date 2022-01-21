using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Incoming
{
    public class PersonDtoIn: BaseDto
    {
        public long PersonId { get; set; }

        public string FirstName { get; set; } //person firstname

        public string LastName { get; set; } // person lastname




    }
    public class PersonDtoProfile : Profile
    {
        public PersonDtoProfile()
        {
            CreateMap<Models.Person, PersonDtoIn>()
                .ForMember(x => x.PersonId, o => o.MapFrom(f => f.Id)).ReverseMap();
        }
    }

}
    

