using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewComponents
{
    public class SignInSignOutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
