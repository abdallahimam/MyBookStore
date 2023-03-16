using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Data
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public ICollection<Section>? Sections { get; set; }
	}
}
