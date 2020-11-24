using API.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
    public class ApiMappings : Profile
    {
        public ApiMappings()
        {
            CreateMap<Data.Layer.Models.User, UserDto>().ReverseMap();
        }
    }
}
