using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace WebStore.Controllers
{
    [Breadcrumb("Блог")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View("Page");
        }

        public IActionResult Page(int id)
        {
            return View();
        }

        [Breadcrumb("Запись")]
        public IActionResult View(int id)
        {
            return View();
        }
    }
}