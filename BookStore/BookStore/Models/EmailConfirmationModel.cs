using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class EmailConfirmationModel
    {
        public string Email { get; set; }
        public bool EmailSent { get; set;}
        public bool EmailVerified { get; set; }
    }
}
