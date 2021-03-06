using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Outgoing
{
    public class PublisherDto: BaseDto


    {
        
        public string Name { get; set; } // Publisher name

        public string Where { get; set; } //  Publisher place 

        public long BookId { get; set; }
    }
    public class PublisherDtoProfile : Profile
    {
        public PublisherDtoProfile() 
        {
            CreateMap<Models.Publisher, PublisherDto>().ReverseMap();
        }
    }

}