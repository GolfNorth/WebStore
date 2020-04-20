using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IEntityService<ProductViewModel> productService)
        {
            _productService = productService as IProductService;
        }
        
        public IActionResult Index(int? categoryId, int? brandId)
        {
            var products = _productService.GetProducts(
                new ProductFilter { BrandId = brandId, CategoryId = categoryId });

            var model = new ShopViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(p => new ProductViewModel()
                    {
                        Id = p.Id,
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price
                    }).OrderBy(p => p.Order)
                    .ToList()
            };

            return View(model);
        }

        public IActionResult View(int id)
        {
            return View();
            //return View(((IEntityService<ProductViewModel>) _productService).GetById(id));
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}