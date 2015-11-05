using System.Collections.Generic;

namespace Nettbutikk.Models
{
    public class CreateProduct : ProductView
    {
        public ICollection<CategoryView> Categories { get; set; }
    }
}