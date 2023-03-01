using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ChangePasswordModel
    {

        [DataType(DataType.Password),
            Display(Name = "Current Password"),
            Required(ErrorMessage = "Enter The Current Password")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password),
            Display(Name = "New Password"),
            Required(ErrorMessage = "Enter a New Strong Password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password),
            Display(Name = "Conrirm Password"),
            Required(ErrorMessage = "Confirm New Password"),
            Compare("NewPassword", ErrorMessage = "Password Does Not Match!")]
        public string? ConfirmNewPassword { get; set; }
    }
}
