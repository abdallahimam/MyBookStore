using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApi.Data
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		[StringLength(20)]
		public string ISBNNumber { get; set; }
		public int Pages { get; set; }
		public int Edition { get; set; }
		public int Publication { get; set; }
		public string? Description { get; set; }

		public int AuthorId { get; set; }
		public Author? Author { get; set; }

		public int SectionId { get; set; }
		public Section? Section { get; set; }

		public int PublisherId { get; set; }
		public Publisher? Publisher { get; set; }

		public int LanguageId { get; set; }
		public Language? Language { get; set; }

		public ICollection<BookCopy>? BookCopies { get; set; }
		public ICollection<EBook>? EBooks { get; set; }
	}
}
