using BookStore.Enums;
using BookStore.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [CheckNameValidation("ah")]
        public string Author { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int LanguageId { get; set; }
        public LanguageModel? Language { get; set; }
        [Required]
        [Display(Name = "Total number of pages")]
        public int? TotalPages { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public List<string> CategoryM { get; set; }
        public LanguageEnum LanguageEnum { get; set; }

        [Required, Display(Name = "Cover Photo")]
        public IFormFile Cover { get; set; }
        public string? CoverPath { get; set; }
        public IFormFile BookPdf { get; set; }
        public string? BookPdfPath { get; set; }

        [Required, Display(Name = "Book Gallery Files")]
        public IFormFileCollection? GalleryFiles { get; set; }
        public List<GalleryModel>? Gallery { get; set; }
    }
}
