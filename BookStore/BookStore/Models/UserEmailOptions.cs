using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class UserEmailOptions
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<KeyValuePair<string, string>> Placeholders { get; set; }
    }
}
