using System.ComponentModel.DataAnnotations;

namespace HovyFridge.Web.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? PasswordRepeat { get; set; }
    }
}
