using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class Author
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Gender { get; set; }
		public DateOnly? BirthDate { get; set; }

		public ICollection<Book>? Books { get; set;}
	}
}
