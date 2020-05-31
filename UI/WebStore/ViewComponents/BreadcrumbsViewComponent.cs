using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}