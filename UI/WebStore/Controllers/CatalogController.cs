using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartBreadcrumbs.Nodes;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Route("shop")]
        public IActionResult Index(int? categoryId, int? brandId, int page = 1)
        {
            var pageSize = int.TryParse(_configuration["PageSize"], out var size) ? size : (int?) null;
            
            var products = _productService.GetProducts(
                new ProductFilter
                {
                    BrandId = brandId, 
                    CategoryId = categoryId,
                    PageSize = pageSize,
                    Page = page
                });

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Products.Select(_mapper.Map<ProductViewModel>).OrderBy(p => p.Order),
                PageViewModel = new PageViewModel
                {
                    PageSize = pageSize ?? 0,
                    PageNumber = page,
                    TotalItems = products.TotalCount
                }
            };

            return View(model);
        }
        
        [Route("product/{id}")]
        public IActionResult View(int id)
        {
            var product = _productService.GetProduct(id);
            
            return View(_mapper.Map<ProductViewModel>(product));
        }
    }
}