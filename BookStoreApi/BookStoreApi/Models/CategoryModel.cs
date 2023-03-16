using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookStoreApi.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		[IgnoreDataMember]
		public ICollection<SectionModel>? Sections { get; set; }
	}
}
