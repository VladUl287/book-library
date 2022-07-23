using AutoMapper;
using Domain.Dtos;
using DataAccess.Models;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookView>().ReverseMap();
            CreateMap<Book, BookCreate>().ReverseMap();
        }
    }
}
