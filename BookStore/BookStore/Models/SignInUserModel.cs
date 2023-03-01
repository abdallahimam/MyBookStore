using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignInUserModel
    {
        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Display(Name = "Your Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a Strong Password")]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
