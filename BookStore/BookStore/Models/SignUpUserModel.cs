using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Enter Your First Name.")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter Your Last Name.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Enter an Email")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a Strong Password")]
        public string? Password { get; set; }

        [Display(Name = "Confirmed Password")]
        [Required(ErrorMessage = "Please Confirm Your Password")]
        [Compare("Password", ErrorMessage = "Password Does not match!")]
        [DataType(DataType.Password)]
        public string? ConfirmedPassword { get; set; }
    }
}
