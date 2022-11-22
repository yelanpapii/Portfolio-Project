using AutoMapper;
using NoSqlExample.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostDTO, Post>().ReverseMap();
        }
    }
}
