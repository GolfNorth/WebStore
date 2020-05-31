using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;
using WebStore.Interfaces.Services;

namespace WebStore.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public BreadcrumbsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        
        public IViewComponentResult Invoke()
        {
            var controller = ViewContext.RouteData.Values["Controller"].ToString();
            var action = ViewContext.RouteData.Values["Action"].ToString();
            var isHomePage = string.Equals("Home", controller) && string.Equals("Index", action); 
            
            if (isHomePage)
                return View(true);
            
            if (!string.Equals("Catalog", controller))
                return View(false);

            var indexNode = new MvcBreadcrumbNode(
                "Index",
                "Catalog",
                "Каталог");
            
            if (string.Equals("Index", action))
            {
                var categoryId = int.TryParse(Request.Query["categoryId"].ToString(), out var id)
                    ? (int?) id
                    : null;
                
                var brandId = int.TryParse(Request.Query["brandId"].ToString(), out id)
                    ? (int?) id
                    : null;

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
                        Parent = indexNode
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
                        Parent = categoryNode ?? indexNode
                    }
                    : null;

                ViewData["BreadcrumbNode"] = brandNode ?? categoryNode ?? indexNode;
            }

            if (string.Equals("View", action))
            {
                var productId = int.Parse(ViewContext.ModelState["id"].AttemptedValue);
                var product = _productService.GetProduct(productId);
            
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
                            Parent = indexNode
                        }
                    }
                };
            }
            
            return View(false);
        }
    }
}