using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace WebStore.Controllers
{
    [DefaultBreadcrumb("Главная")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}