using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models
{
    public class CreditCard
    {
        [Key]
        public string Number { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CCV { get; set; }
        
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireYear { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireMonth { get; set; }
    }
}