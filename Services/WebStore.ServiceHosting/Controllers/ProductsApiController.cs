using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductService _productService;

        public ProductsApiController(IProductService productService) => _productService = productService;

        [HttpGet("categories")]
        public IEnumerable<Category> GetCategories() => _productService.GetCategories();

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => _productService.GetBrands();

        [HttpPost]
        public IEnumerable<ProductDto> GetProducts([FromBody] ProductFilter filter = null) => _productService.GetProducts(filter);

        [HttpGet("{id}")]
        public ProductDto GetProduct(int id) => _productService.GetProduct(id);

        [NonAction]
        public void Add(Product product)
        {
            _productService.Add(product);
        }

        [NonAction]
        public void Edit(int id, Product product)
        {
            _productService.Edit(id, product);
        }

        [NonAction]
        public bool Delete(int id)
        {
            return _productService.Delete(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _productService.SaveChanges();
        }
    }
}