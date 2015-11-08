using System.Collections.Generic;
using Nettbutikk.Model;

namespace Nettbutikk.Customer
{
    public interface ICustomerRepo
    {
        bool DeleteCustomer(string email);
        List<CustomerModel> GetAllCustomers();
        CustomerModel GetCustomer(int customerId);
        CustomerModel GetCustomer(string email);
    }
}