using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    using BLL.BusinessObjects;
    using BLL.Services;

    using DAL;
    using DAL.Entities;
    using DAL.Facade;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTestCustomer
    {
        [TestMethod]
        public void TestCreateAndGetAllCustomer()
        {
            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));
            CustomerBO customer = new CustomerBO()
                                    {
                                        Firstname = "Bo",
                                        Lastname = "Jensen",
                                        Address = "Skolevej 3",
                                        ZipCode = 4510,
                                        City = "Dumby",
                                        Email = "Email@mail.dk",
                                        CVR = 12345678
                                    };
            service.Create(customer);
            Assert.AreEqual(service.GetAll().Count, 1);
        }

        [TestMethod]
        public void TestGetCustomerById()
        {
            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));
            CustomerBO customer1 = new CustomerBO()
                                      {
                                          Firstname = "Bo",
                                          Lastname = "Jensen",
                                          Address = "Skolevej 3",
                                          ZipCode = 4510,
                                          City = "Dumby",
                                          Email = "Email@mail.dk",
                                          CVR = 12345678
                                      };
            service.Create(customer1);
            CustomerBO customer2 = new CustomerBO()
                                       {
                                           Firstname = "Lars",
                                           Lastname = "Jensen",
                                           Address = "Skolevej 3",
                                           ZipCode = 4510,
                                           City = "Dumby",
                                           Email = "Email@mail.dk",
                                           CVR = 12345678
                                       };
            customer2 = service.Create(customer2);
            Assert.AreEqual(service.Get(customer2.Id).Firstname, "Lars");

        }

        [TestMethod]
        public void TestDeleteCustomer()
        {
            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));
            CustomerBO customer1 = new CustomerBO()
                                       {
                                           Firstname = "Bo",
                                           Lastname = "Jensen",
                                           Address = "Skolevej 3",
                                           ZipCode = 4510,
                                           City = "Dumby",
                                           Email = "Email@mail.dk",
                                           CVR = 12345678
                                       };
            customer1 = service.Create(customer1);
            CustomerBO customer2 = new CustomerBO()
                                       {
                                           Firstname = "Lars",
                                           Lastname = "Jensen",
                                           Address = "Skolevej 3",
                                           ZipCode = 4510,
                                           City = "Dumby",
                                           Email = "Email@mail.dk",
                                           CVR = 12345678
                                       };
            customer2 = service.Create(customer2);
            service.Delete(customer2.Id);
            Assert.AreEqual(service.GetAll().Count, 1);
            Assert.AreEqual(service.Get(customer1.Id).Firstname, "Bo");

        }

        [TestMethod]
        public void TestUpdateCustomer()
        {
            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));
            CustomerBO customer1 = new CustomerBO()
                                       {
                                           Firstname = "Bo",
                                           Lastname = "Jensen",
                                           Address = "Skolevej 3",
                                           ZipCode = 4510,
                                           City = "Dumby",
                                           Email = "Email@mail.dk",
                                           CVR = 12345678
                                       };
            customer1 = service.Create(customer1);

            customer1.Firstname = "Lars";

            service.Update(customer1);
            Assert.AreEqual(service.Get(1).Firstname, "Lars");

        }
    }
}
