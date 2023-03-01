using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class UpdateUserNameModel
    {
        [Required(ErrorMessage = "Enter Your First Name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Your Last Name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
