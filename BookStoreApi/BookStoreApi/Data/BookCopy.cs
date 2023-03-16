using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class BookCopy
	{
		public int Id { get; set; }
		[Required]
		[StringLength(20)]
		public string Status { get; set; }

		public int BookId { get; set; }
		public Book? Book { get; set; }
	}
}
