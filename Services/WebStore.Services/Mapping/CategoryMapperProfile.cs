using AutoMapper;
using WebStore.Domain.Dtos;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}