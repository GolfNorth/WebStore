﻿using System.Linq;
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
                Products = products.Products.Select(_mapper.Map<ProductViewModel>).OrderBy(p => p.Order)
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