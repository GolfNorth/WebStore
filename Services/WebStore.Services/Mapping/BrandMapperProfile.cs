using AutoMapper;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public class BrandMapperProfile : Profile
    {
        public BrandMapperProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();

            CreateMap<Brand, BrandViewModel>();
        }
    }
}