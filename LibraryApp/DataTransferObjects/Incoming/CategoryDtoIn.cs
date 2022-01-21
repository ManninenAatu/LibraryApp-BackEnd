using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Incoming
{
    public class CategoryDtoIn:BaseDto
    {
        
       

        public string Name { get; set; } // Category Name

        public long BookId { get; set; } 
    }

    public class CategoryDtoProfile : Profile
    {
        public CategoryDtoProfile()
        {
            CreateMap<Models.Category, CategoryDtoIn>().ReverseMap();
        }
    }
}
