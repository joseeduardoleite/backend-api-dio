using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage = "Login is mandatory")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}