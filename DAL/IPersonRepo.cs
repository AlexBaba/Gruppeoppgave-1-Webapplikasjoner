﻿using System.Collections.Generic;
using Oblig1_Nettbutikk.Model;

namespace Oblig1_Nettbutikk.DAL
{
    public interface IPersonRepo
    {
        bool AddPerson(PersonModel person);
        bool DeletePerson(int personId);
        AdminModel GetAdmin(int adminId);
        List<PersonModel> GetAllPeople();
        CustomerModel GetCustomer(int customerId);
        PersonModel GetPerson(int personId);
        bool UpdatePerson(PersonModel personUpdate, int personId);
    }
}