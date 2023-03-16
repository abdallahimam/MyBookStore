using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookStoreApi.Models
{
	public class LanguageModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		[IgnoreDataMember]
		public ICollection<BookModel>? Books { get; set; }
	}
}
