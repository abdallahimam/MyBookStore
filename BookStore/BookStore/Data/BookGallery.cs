using BookStore.Enums;
using BookStore.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data
{
    public class BookGallery
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Path { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
