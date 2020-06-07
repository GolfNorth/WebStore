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

        public IViewComponentResult Invoke(string brandId)
        {
            return View(new BrandCompleteViewModel
            {
                Brands = GetBrands(),
                CurrentBrandId = int.TryParse(brandId, out var id) ? id : (int?)null
            });
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            return _productService
                .GetBrands()
                .Select(brand =>
                {
                    var brandViewModel = _mapper.Map<BrandViewModel>(brand);
                    
                    brandViewModel.Products = _productService
                        .GetProducts(
                            new ProductFilter {BrandId = brandViewModel.Id}
                        )
                        .Products
                        .Count();
                    
                    return brandViewModel;
                })
                .AsEnumerable();
        }
    }
}