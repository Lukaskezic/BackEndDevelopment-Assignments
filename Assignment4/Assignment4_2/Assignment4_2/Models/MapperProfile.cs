using System;
using AutoMapper;

namespace Assignment4_2.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MapCard, Card>().ReverseMap();
        }
    }
}

