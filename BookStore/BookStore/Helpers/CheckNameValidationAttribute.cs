using System.ComponentModel.DataAnnotations;

namespace BookStore.Helpers
{
    public class CheckNameValidationAttribute : ValidationAttribute
    {
        public string Text { get; set; }

        public CheckNameValidationAttribute(string text)
        {
            Text = text;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value.ToString().Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "Desired value not exist!");
        }
    }
}
