using AutoMapper;
using Common.Dtos;
using DataAccess.Models;

namespace Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Book, CreateBookModel>().ReverseMap();
        }
    }
}
