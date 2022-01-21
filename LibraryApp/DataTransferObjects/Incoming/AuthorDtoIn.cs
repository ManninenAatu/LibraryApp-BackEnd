using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Incoming
{
    public class AuthorDtoIn:BaseDto
    {
        public long PersonId { get; set; } // author personId

        public string FirstName { get; set; } // author firstname

        public string LastName { get; set; } // author lastname


    }

    public class AuthorDtoProfile : Profile
    {
        public AuthorDtoProfile()
        {
            CreateMap<Models.Author, AuthorDtoIn>()
                 .ForMember(authorDto => authorDto.FirstName, x => x.MapFrom(author => author.Person.FirstName))
                .ForMember(authorDto => authorDto.LastName, x => x.MapFrom(author => author.Person.LastName)); 
               

        }

    }
}