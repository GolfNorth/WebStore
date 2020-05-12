using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.Identity
{
    public class SignInViewModel
    {
        [Required] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить")] public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}