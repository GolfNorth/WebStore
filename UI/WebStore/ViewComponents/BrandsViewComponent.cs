using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BrandsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();


            return View(brands);
        }

        private List<BrandViewModel> GetBrands()
        {
            var brands = new List<BrandViewModel>();

            foreach (var brand in _productService.GetBrands())
            {
                brands.Add(new BrandViewModel()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Order = brand.Order
                });

                ViewData["ProductsByBrand" + brand.Id] = _productService.GetProducts(
                    new ProductFilter {BrandId = brand.Id}).Count();
            }

            return brands;
        }
    }
}