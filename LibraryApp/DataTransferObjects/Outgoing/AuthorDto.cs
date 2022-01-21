using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Outgoing
{
    public class AuthorDto : BaseDto
    {
        public long PersonId { get; set; } // author person Id

        public string FirstName { get; set; } // author firstname

        public string LastName { get; set; } // author lastname


    }

    public class AuthorDtoProfile : Profile
    {
        public AuthorDtoProfile()
        {
            CreateMap<Models.Author, AuthorDto>()
                .ForMember(authorDto => authorDto.FirstName, x => x.MapFrom(author => author.Person.FirstName))
                .ForMember(authorDto => authorDto.LastName, x => x.MapFrom(author => author.Person.LastName));
        }
    }
}
 
