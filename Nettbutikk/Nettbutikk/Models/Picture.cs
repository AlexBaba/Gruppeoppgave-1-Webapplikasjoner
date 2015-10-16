using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nettbutikk.Models
{
    public class Image
    {
        [Key]
        [Required]
        public string Path { get; set; }

        [Key]
        [Required]
        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}