using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShopController : Controller
    {
        readonly List<ProductViewModel> _products;
        
        public ShopController()
        {
            _products = new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Туалетная бумага",
                    Description = "Незаменимая вещь в период пандемии",
                    Price = 199.9m
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Гречка",
                    Description = "Еда на вес золота",
                    Price = 59.9m
                }
            };

        }
        
        public IActionResult Index()
        {
            return View(_products);
        }

        public IActionResult View(int id)
        {
            return View(_products.FirstOrDefault(p => p.Id == id));
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}