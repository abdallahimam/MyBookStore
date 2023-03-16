using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookStoreApi.Models
{
	public class PublisherModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Location { get; set; }

		[IgnoreDataMember]
		public ICollection<BookModel>? Books { get; set; }
	}
}
