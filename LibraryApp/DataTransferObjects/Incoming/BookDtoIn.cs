using AutoMapper;
using LibraryApp.DataTransferObjects.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Incoming
{
    public class BookDtoIn:BaseDto
    {
        

        public string Name { get; set; } // book name

        public string? Description { get; set; } // book description

        public string? AdditionalInformation { get; set; } // book additionaliformation

        public string Isbn { get; set; } // book isbn

        public long Year { get; set; } // book year

        public long? NumberOfCopies { get; set; } // nubmer of copies

        public string Language { get; set; } // book language

        public List<CategoryDtoIn> Categories { get; set; } //  categories

        public List<PublisherDtoIn> Publishers { get; set; } // publishers

        public List<PersonDtoIn> Author { get; set; } // author








    }
    public class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<Models.Book, BookDtoIn>().ReverseMap();
            CreateMap<Models.Category, CategoryDtoIn>().ReverseMap();
            CreateMap<Models.Publisher, PublisherDtoIn>().ReverseMap();

        }
    }
}
