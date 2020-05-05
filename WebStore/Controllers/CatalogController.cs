using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("shop")]
        public IActionResult Index(int? categoryId, int? brandId)
        {
            var products = _productService.GetProducts(
                new ProductFilter { BrandId = brandId, CategoryId = categoryId });

            var model = new CatalogViewModel()
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

        [Route("product/{id}")]
        public IActionResult View(int id)
        {
            var product = _productService.GetProductById(id);

            return View(new ProductViewModel()
            {
                Id = product.Id,
                Brand = product.Brand.Name,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price
            });
        }
    }
}