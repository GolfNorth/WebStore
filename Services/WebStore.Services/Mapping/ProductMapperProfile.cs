﻿using AutoMapper;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(
                    dest => dest.Brand, 
                    opt => opt.MapFrom(src => src.Brand.Name));
            CreateMap<ProductViewModel, Product>();

            CreateMap<ProductDto, ProductViewModel>()
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Brand.Name));
            CreateMap<ProductViewModel, ProductDto>();

            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}