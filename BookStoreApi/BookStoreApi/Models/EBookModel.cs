using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
	public class EBookModel
	{
		public int Id { get; set; }
		[StringLength(10)]
		public string Extension { get; set; }

		public int BookId { get; set; }
		public BookModel? Book { get; set; }
	}
}
