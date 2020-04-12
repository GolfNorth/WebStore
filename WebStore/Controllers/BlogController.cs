using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class Blog : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}