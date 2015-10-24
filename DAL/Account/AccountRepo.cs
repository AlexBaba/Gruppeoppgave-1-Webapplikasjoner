﻿using System;
using System.Collections.Generic;
using System.Linq;
using Oblig1_Nettbutikk.Model;

namespace Oblig1_Nettbutikk.DAL
{
    public class AccountRepo : IAccountRepo
    {
        public List<PersonModel> GetAllPeople()
        {
            using (var db = new TankshopDbContext())
            {
                var people = db.People.Select(p => new PersonModel()
                {
                    Email = p.Email,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Address = p.Address,
                    Zipcode = p.Zipcode,
                    City = p.Postal.City
                }).ToList();

                // 
                //foreach (var person in people)
                //{
                //    // If not admin / customer -> id == -1
                //    int adminId = -1, customerId = -1;

                //    var admin = db.Administrators.FirstOrDefault(a => a.PersonId == person.PersonId);
                //    var customer = db.Customers.FirstOrDefault(c => c.PersonId == person.PersonId);

                //    if (admin != null)
                //    {
                //        adminId = admin.AdministratorId;
                //    }

                //    if (customer != null)
                //    {
                //        customerId = customer.CustomerId;
                //    }
                //}

                return people;
            }
        }

        public bool AddPerson(PersonModel person, Role role, string password)
        {
            var newPerson = new Person()
            {
                Email = person.Email,
                Firstname = person.Firstname,
                Lastname = person.Lastname,
                Address = person.Address,
                Zipcode = person.Zipcode,

            };
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var personPostal = db.Postals.Find(person.Zipcode);
                    if (personPostal == null)
                    {
                        personPostal = new Postal()
                        {
                            Zipcode = person.Zipcode,
                            City = person.City
                        };
                    }
                    personPostal.People.Add(newPerson);
                    newPerson.Postal = personPostal;

                    db.People.Add(newPerson);
                    db.SaveChanges();


                    if (CreateCredentials(person.Email, password))
                        if (SetRole(person.Email, role, true))
                            return true;

                    return false;

                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool CreateCredentials(string email, string password)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var existingCredentials = db.Credentials.Find(email);
                    if (existingCredentials != null)
                        return false;

                    var passwordHash = CreateHash(password);
                    var newCredentials = new Credential()
                    {
                        Email = email,
                        Password = passwordHash
                    };
                    db.Credentials.Add(newCredentials);

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }
        }

        public bool SetRole(string email, Role role, bool isRole)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    if (role == Role.Admin)
                    {
                        var dbAdmin = db.Administrators.Find(email);
                        if (isRole)
                        {
                            if (dbAdmin == null)
                            {
                                var newAdmin = new Administrator()
                                {
                                    Email = email
                                };
                                db.Administrators.Add(newAdmin);
                            }
                        }
                        else
                        {
                            db.Administrators.Remove(dbAdmin);
                        }
                    }
                    if (role == Role.Customer)
                    {
                        var dbCustomer = db.Customers.Find(email);
                        if (isRole)
                        {
                            if (dbCustomer == null)
                            {
                                var newCustomer = new Customer()
                                {
                                    Email = email
                                };
                                db.Customers.Add(newCustomer);
                            }
                        }
                        else
                        {
                            db.Customers.Remove(dbCustomer);
                        }
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public PersonModel GetPerson(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {

                    var person = db.People.Where(p => p.Email == email).Select(p => new PersonModel()
                    {
                        Email = p.Email,
                        Firstname = p.Firstname,
                        Lastname = p.Lastname,
                        Address = p.Address,
                        Zipcode = p.Zipcode,
                        City = p.Postal.City
                    }).Single();

                    return person;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public CustomerModel GetCustomer(int customerId)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    var dbPerson = GetPerson(dbCustomer.Email);
                    var orderRepo = new OrderRepo();

                    var customer = new CustomerModel()
                    {
                        Email = dbPerson.Email,
                        Firstname = dbPerson.Firstname,
                        Lastname = dbPerson.Lastname,
                        Address = dbPerson.Address,
                        Zipcode = dbPerson.Zipcode,
                        City = dbPerson.City,
                        CustomerId = customerId,
                        Orders = orderRepo.GetOrders(customerId)
                    };

                    return customer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public CustomerModel GetCustomer(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbPerson = GetPerson(email);
                    var customerId = db.Customers.Find(email).CustomerId;

                    var orderRepo = new OrderRepo();

                    var customer = new CustomerModel()
                    {
                        Email = dbPerson.Email,
                        Firstname = dbPerson.Firstname,
                        Lastname = dbPerson.Lastname,
                        Address = dbPerson.Address,
                        Zipcode = dbPerson.Zipcode,
                        City = dbPerson.City,
                        CustomerId = customerId,
                        Orders = orderRepo.GetOrders(customerId)
                    };

                    return customer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public AdminModel GetAdmin(int adminId)
        {
            using (var db = new TankshopDbContext())
            {
                var dbAdmin = db.Administrators.FirstOrDefault(a => a.AdministratorId == adminId);
                var dbPerson = GetPerson(dbAdmin.Email);
                var admin = new AdminModel()
                {
                    Email = dbPerson.Email,
                    Firstname = dbPerson.Firstname,
                    Lastname = dbPerson.Lastname,
                    Address = dbPerson.Address,
                    Zipcode = dbPerson.Zipcode,
                    City = dbPerson.City,
                    AdminId = adminId
                };

                return admin;
            }
        }


        public AdminModel GetAdmin(string email)
        {
            using (var db = new TankshopDbContext())
            {
                var dbAdmin = db.Administrators.Find(email);
                var dbPerson = GetPerson(dbAdmin.Email);
                var admin = new AdminModel()
                {
                    Email = dbPerson.Email,
                    Firstname = dbPerson.Firstname,
                    Lastname = dbPerson.Lastname,
                    Address = dbPerson.Address,
                    Zipcode = dbPerson.Zipcode,
                    City = dbPerson.City,
                    AdminId = dbAdmin.AdministratorId
                };

                return admin;
            }

        }

        // Return true / false on update ok / error
        public bool UpdatePerson(PersonModel personUpdate, string email)
        {
            // TODO: update admin/customer -id
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var editPerson = db.People.Find(email);
                    var editPersonModel = GetPerson(email);
                                        
                    editPerson.Firstname = personUpdate.Firstname;
                    editPerson.Lastname = personUpdate.Lastname;
                    editPerson.Address = personUpdate.Address;

                    var personPostal = db.Postals.Find(personUpdate.Zipcode);
                    if (personPostal == null)
                    {
                        var oldPostal = db.Postals.Find(editPerson.Zipcode);
                        if (oldPostal != null)
                            oldPostal.People.Remove(editPerson);

                        personPostal = new Postal()
                        {
                            Zipcode = personUpdate.Zipcode,
                            City = personUpdate.City
                        };
                        personPostal.People.Add(editPerson);
                    }

                    editPerson.Zipcode = personUpdate.Zipcode;

                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        // Return PersonModel of deleted person or null on error
        public bool DeletePerson(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var deletePerson = db.People.Find(email);
                    var deletePersonModel = GetPerson(email);

                    db.People.Remove(deletePerson);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AttemptLogin(string email, string password)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var passwordHash = CreateHash(password);
                    var existingUser = db.Credentials.FirstOrDefault(c => c.Email == email && c.Password == passwordHash);

                    if (existingUser == null)
                        return false;

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private byte[] CreateHash(string password)
        {
            byte[] inData, outData;
            var alg = System.Security.Cryptography.SHA256.Create();
            inData = System.Text.Encoding.Default.GetBytes(password);
            outData = alg.ComputeHash(inData);
            return outData;
        }

        public bool ChangePassword(string email, string newPassword)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var newPasswordHash = CreateHash(newPassword);
                    var existingUser = db.Credentials.Find(email);
                    if (existingUser == null)
                        return false;

                    existingUser.Password = newPasswordHash;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }        
    }
}