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

        [Breadcrumb("Контакты")]
        [Route("contactus")]
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}