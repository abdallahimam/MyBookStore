using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookStoreApi.Models
{
	public class BookModel
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
		public AuthorModel? Author { get; set; }

		public int SectionId { get; set; }
		public SectionModel? Section { get; set; }

		public int PublisherId { get; set; }
		public PublisherModel? Publisher { get; set; }

		public int LanguageId { get; set; }
		public LanguageModel? Language { get; set; }

		[IgnoreDataMember]
		public ICollection<BookCopyModel>? BookCopies { get; set; }

		[IgnoreDataMember]
		public ICollection<EBookModel>? EBooks { get; set; }
	}
}
