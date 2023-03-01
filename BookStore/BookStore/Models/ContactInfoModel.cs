using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ContactInfoConfig
    {
        public string Number { get; set; }
        public string CorporationName { get; set; }
        public string FacebookPage { get; set; }
        public string Whats { get; set;}
        public string Address { get; set;}
    }
}
