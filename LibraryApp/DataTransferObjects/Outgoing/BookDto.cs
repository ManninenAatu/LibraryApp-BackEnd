using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Outgoing
{
    public class BookDto : BaseDto
    {
        

        public string Name { get; set; } // book name

        public string? Description { get; set; }  // book descripion

        public string? AdditionalInformation { get; set; } // book additionalinformation

        public string Isbn { get; set; } // book isbn number serial

        public long Year { get; set; } // book year

        public long? NumberOfCopies { get; set; } // nubmer of copies

        public string Language { get; set; } // book language

        public List<AuthorDto> Authors { get; set; } // books Authors

        public List<CategoryDto> Categories { get; set; } // books categories

        public List<LendingDto> Lendings { get; set; } // book lendings

        public List<PublisherDto> Publishers { get; set; } // book publishers

        public List<PersonDto> Person { get; set; } // author person




    }

    public class BookDtoProfile: Profile
    {
        public BookDtoProfile()
        {
            CreateMap<Models.Book, BookDto>().ReverseMap();

                
                


        }
    }
}
