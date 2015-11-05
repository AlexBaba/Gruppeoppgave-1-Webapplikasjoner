using Nettbutikk.Model;
using System.Collections.Generic;

namespace Nettbutikk.Models
{
    public class EditProduct : ProductView
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}