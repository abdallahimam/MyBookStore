using BookStoreApi.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookStoreApi.Models
{
	public class SectionModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public int CategoryId { get; set; }
		public CategoryModel? Category { get; set; }

		[IgnoreDataMember]
		public ICollection<BookModel>? Books { get; set; }
	}
}
