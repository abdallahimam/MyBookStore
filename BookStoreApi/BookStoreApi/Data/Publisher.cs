using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class Publisher
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Location { get; set; }

		public ICollection<Book>? Books { get; set; }
	}
}
