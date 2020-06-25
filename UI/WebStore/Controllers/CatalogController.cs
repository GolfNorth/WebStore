using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WebStore.Domain.Dtos.Products;
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
        private const string PageSize = "PageSize";

        public CatalogController(IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Route("shop")]
        public IActionResult Index(int? categoryId, int? brandId, int page = 1)
        {
            var pageSize = int.TryParse(_configuration[PageSize], out var size) ? size : (int?) null;
            
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
        
        #region API

        public IActionResult GetFilteredItems(int? sectionId, int? brandId, int page)
        {
            var products =
                GetProducts(sectionId, brandId, page)
                    .Select(_mapper.Map<ProductViewModel>)
                    .OrderBy(p => p.Order);
            return PartialView("Partial/_FeaturedItems", products);
        }

        private IEnumerable<ProductDto> GetProducts(int? catalogId, int? brandId, int page) =>
            _productService.GetProducts(new ProductFilter
                {
                    CategoryId = catalogId,
                    BrandId = brandId,
                    Page = page,
                    PageSize = int.Parse(_configuration[PageSize])
                })
                .Products;

        #endregion
    }
}