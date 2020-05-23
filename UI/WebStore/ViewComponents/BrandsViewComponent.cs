using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public BrandsViewComponent(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke() => View(GetBrands());

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _productService
                .GetBrands()
                .Select(_mapper.Map<BrandViewModel>)
                .AsEnumerable();

            foreach (var brand in brands)
            {
                brand.Products = _productService.GetProducts(
                    new ProductFilter {BrandId = brand.Id}).Count();
            }

            return brands;
        }
    }
}