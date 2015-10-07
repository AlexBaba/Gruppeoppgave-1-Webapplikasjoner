using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models.Binding
{
    public class Search
    {
        [Required]
        [MaxLength(127)]
        string SearchTerm;
    }
}