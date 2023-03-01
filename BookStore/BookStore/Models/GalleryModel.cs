using BookStore.Enums;
using BookStore.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class GalleryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Path { get; set; }

    }
}
