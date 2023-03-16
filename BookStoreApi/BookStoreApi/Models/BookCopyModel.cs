using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
	public class BookCopyModel
	{
		public int Id { get; set; }
		[Required]
		[StringLength(20)]
		public string Status { get; set; }

		public int BookId { get; set; }
		public BookModel? BookModel { get; set; }
	}
}
