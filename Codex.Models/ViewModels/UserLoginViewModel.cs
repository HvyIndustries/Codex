namespace Codex.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string PlainTextPassword { get; set; }
    }
}
