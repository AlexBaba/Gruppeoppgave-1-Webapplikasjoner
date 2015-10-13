using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models
{
    public class CreditCard
    {
        [Key]
        public string Number { get; set; }

        public string NameOnCard { get; set; }

        public string CCV { get; set; }

        public string ExpireYear { get; set; }

        public string ExpireMonth { get; set; }
    }
}