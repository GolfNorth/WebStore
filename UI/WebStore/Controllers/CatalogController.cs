using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private MvcBreadcrumbNode _indexNode;

        public CatalogController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
            _indexNode = new MvcBreadcrumbNode(
                "Index",
                "Catalog",
                "Каталог");
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
                Products = products.Select(_mapper.Map<ProductViewModel>).OrderBy(p => p.Order)
            };
            
            var categoryNode = categoryId != null
                ? new MvcBreadcrumbNode(
                    "Index",
                    "Catalog",
                    _productService.GetCategory((int) categoryId).Name)
                {
                    RouteValues = new
                    {
                        categoryId = categoryId
                    },
                    Parent = _indexNode
                }
                : null;

            var brandNode = brandId != null
                ? new MvcBreadcrumbNode(
                    "Index",
                    "Catalog",
                    _productService.GetBrand((int) brandId).Name)
                {
                    RouteValues = new
                    {
                        brandId = brandId
                    },
                    Parent = categoryNode ?? _indexNode
                }
                : null;

            ViewData["BreadcrumbNode"] = brandNode ?? categoryNode ?? _indexNode;
            
            return View(model);
        }
        
        [Route("product/{id}")]
        public IActionResult View(int id)
        {
            var product = _productService.GetProduct(id);
            
            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode(
                "View", 
                "Catalog", 
                product.Name)
            {
                Parent = new MvcBreadcrumbNode(
                    "Index",
                    "Catalog",
                    product.Brand.Name)
                {
                    RouteValues = new
                    {
                        brandId = product.Brand.Id
                    },
                    Parent = new MvcBreadcrumbNode(
                        "Index",
                        "Catalog",
                        product.Category.Name)
                    {
                        RouteValues = new
                        {
                            categoryId = product.Category.Id
                        },
                        Parent = _indexNode
                    }
                }
            };

            return View(_mapper.Map<ProductViewModel>(product));
        }
    }
}