using Nettbutikk.Model;
using System.Collections.Generic;

namespace Nettbutikk.BusinessLogic
{
    public interface IAccountLogic
    {
        bool AttemptLogin(string email, string password);
        bool AddPerson(Person person, Role role, string password);
        bool ChangePassword(string email, string newPassword);
        bool DeletePerson(string email);
        AdminModel GetAdmin(int adminId);
        AdminModel GetAdmin(string email);
        List<PersonModel> GetAllPeople();
        CustomerModel GetCustomer(int customerId);
        CustomerModel GetCustomer(string email);
        bool isAdmin(string email);

        Person GetPerson(string email);
        bool UpdatePerson(Person personUpdate, string email);
    }
}