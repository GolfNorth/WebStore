using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class WebAPITestController : Controller
    {
        private readonly IValueServices _valueServices;

        public WebAPITestController(IValueServices valueServices) => _valueServices = valueServices;

        public IActionResult Index() => View(_valueServices.Get());
    }
}