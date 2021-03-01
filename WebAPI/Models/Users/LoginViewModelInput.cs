using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Users
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "Login is mandatory")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}