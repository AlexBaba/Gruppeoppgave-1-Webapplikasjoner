using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nettbutikk.Models
{
    [NotMapped]
    public class Cart : Order
    {
    }
}