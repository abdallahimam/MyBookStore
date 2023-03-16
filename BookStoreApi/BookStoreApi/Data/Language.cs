using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class Language
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public ICollection<Book>? Books { get; set; }
	}
}
