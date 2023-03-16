using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class Section
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public int CategoryId { get; set; }
		public Category? Category { get; set; }

		public ICollection<Book>? Books { get; set; }
	}
}
