using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Model
{
    public class Customer : Person
    {
        public Customer(Person p)
        {
            Id = p.Id;
            Email = p.Email;
            Firstname = p.Firstname;
            Lastname = p.Lastname;
            Address = p.Address;
            Postal = p.Postal;
        }

        public Customer()
        {
            Orders = new List<Order>();
        }
        
        public IEnumerable<Order> Orders { get; set; }
    }

}
