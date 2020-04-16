using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class BlogController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View("Page");
        }

        public IActionResult Page(int id)
        {
            return View();
        }

        public IActionResult View(int id)
        {
            return View();
        }
    }
}