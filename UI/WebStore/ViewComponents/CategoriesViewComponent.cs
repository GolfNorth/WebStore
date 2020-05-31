using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CategoriesViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke(string categoryId)
        {
            var currentCategoryId = int.TryParse(categoryId, out var id) ? id : (int?)null;
            
            return View(new CategoryCompleteViewModel
            {
                Categories = GetCategories(currentCategoryId, out var parentCategoryId),
                CurrentCategoryId = currentCategoryId,
                ParentCategoryId = parentCategoryId
            });
        }

        private IEnumerable<CategoryViewModel> GetCategories(int? categoryId, out int? parentCategoryId)
        {
            parentCategoryId = null;
            
            var categories = _productService.GetCategories();
            var parentSections = categories.Where(p => !p.ParentId.HasValue).ToArray();
            var parentCategories = new List<CategoryViewModel>();
            
            foreach (var parentCategory in parentSections)
                parentCategories.Add(new CategoryViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                });
            
            foreach (var parentCategory in parentCategories)
            {
                var childCategories = categories.Where(c => c.ParentId == parentCategory.Id);
                
                foreach (var childCategory in childCategories)
                {
                    if (childCategory.Id == categoryId)
                        parentCategoryId = parentCategory.Id;
                    
                    parentCategory.ChildCategories.Add(new CategoryViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = parentCategory
                    });
                }
                
                parentCategory.ChildCategories = parentCategory.ChildCategories.OrderBy(c => c.Order).ToList();
            }

            parentCategories = parentCategories.OrderBy(c => c.Order).ToList();

            return parentCategories;
        }
    }
}