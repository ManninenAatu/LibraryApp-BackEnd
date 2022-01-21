using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataTransferObjects.Outgoing
{
    public class CategoryDto: BaseDto
    {
        public string Name { get; set; } // CategoryName

        public long BookId { get; set; }

        

                
    }

    public class CategoryDtoProfile : Profile
    {
        public CategoryDtoProfile()
        {
            CreateMap<Models.Category, CategoryDto>().ReverseMap();

        }
    }
}
