using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        [Required]
        public string StreetName
        {
            get;
            set;
        }

        [Required]
        public string HouseNumber
        {
            get;
            set;
        }

        [Required]
        public virtual ZipCode ZipCode
        {
            get;
            set;
        }

        [Required]
        public string State
        {
            get;
            set;
        }

        [Required]
        public string Country
        {
            get;
            set;
        }

        [Required]
        [InverseProperty("Addresses")]
        public virtual User Addressee { get; set; }
    }
}