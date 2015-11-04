using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Model
{
    public class Person
    {
        private Postal postal;

        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get { return string.Format("{0} {1}", Firstname, Lastname); } }

        public string Address { get; set; }
        
        public string Zipcode { get { return Postal.Zipcode; } set { Postal.Zipcode = value; } }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string City
        {
            get { return Postal.City; }
        }
        
        public virtual Postal Postal {
            get {
                return postal ?? (postal = new Postal());
            }
            set { postal = value; }
        }
    }
}
