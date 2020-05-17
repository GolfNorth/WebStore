using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Регистрация

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User {UserName = model.UserName, Email = model.Email};
            var createResult = await _userManager.CreateAsync(user, model.Password);

            if (!createResult.Succeeded)
            {
                foreach (var identityError in createResult.Errors)
                    ModelState.AddModelError("", identityError.Description);

                return View(model);
            }

            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, "Users");

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Авторизация

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var loginResult =
                await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка авторизации");

                return View(model);
            }

            if (Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Выход

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}