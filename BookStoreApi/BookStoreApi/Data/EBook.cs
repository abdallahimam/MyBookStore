using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class EBook
	{
		public int Id { get; set; }
		[StringLength(10)]
		public string Extension { get; set; }

		public int BookId { get; set; }
		public Book? Book { get; set; }
	}
}
