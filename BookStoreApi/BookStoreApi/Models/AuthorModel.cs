using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookStoreApi.Models
{
	// [DataContract(IsReference = true)]
	public class AuthorModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Gender { get; set; }
		public DateOnly? BirthDate { get; set; }

		[IgnoreDataMember]
		public ICollection<BookModel>? Books { get; set; }

	}
}
