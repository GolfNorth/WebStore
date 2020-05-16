using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

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
                new ProductFilter {BrandId = brandId, CategoryId = categoryId});

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(ProductMapping.ToView).OrderBy(p => p.Order)
            };

            return View(model);
        }

        [Route("product/{id}")]
        public IActionResult View(int id)
        {
            var product = _productService.GetProductById(id);

            return View(product.ToView());
        }
    }
}