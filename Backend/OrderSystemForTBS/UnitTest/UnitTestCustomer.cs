//using System;
//using System.Collections.Generic;
//using System.Text;
//using BLL;
//using DAL.Context;
//using DAL.UOW;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace UnitTest
//{
//    using BLL.BusinessObjects;
//    using BLL.Services;

//    using DAL;
//    using DAL.Entities;
//    using DAL.Facade;

//    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class UnitTestCustomer
    {
        [TestMethod]
        public void TestCreateCustomer()
        {
            this.GetMemoContext().Database.EnsureDeleted();
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
            customer = this.GetService().Create(customer);
            Assert.IsNotNull(customer);

        }

        [TestMethod]
        public void GetallCustomers()
        {
            this.GetMemoContext().Database.EnsureDeleted();
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
            this.GetService().Create(customer);
            Assert.AreEqual(this.GetService().GetAll().Count, 1);

//    [TestClass]
//    public class UnitTestCustomer
//    {
//        [TestMethod]
//        public void TestCreateAndGetAllCustomer()
//        {

//            var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
//                .UseInMemoryDatabase("Database").Options);

//            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
//            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));

//            IService<CustomerBO> service = new CustomerService(dalFacadeMock.Object);
//            CustomerBO customer = new CustomerBO()
//                                    {
//                                        Firstname = "Bo",
//                                        Lastname = "Jensen",
//                                        Address = "Skolevej 3",
//                                        ZipCode = 4510,
//                                        City = "Dumby",
//                                        Email = "Email@mail.dk",
//                                        CVR = 12345678
//                                    };
//            customer = service.Create(customer);
//            Assert.IsNotNull(customer);
//            //service.Create(customer);
//            //Assert.AreEqual(service.GetAll().Count, 1);
//        }

//        [TestMethod]
//        public void TestGetCustomerById()
//        {
//            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));
//            CustomerBO customer1 = new CustomerBO()
//                                      {
//                                          Firstname = "Bo",
//                                          Lastname = "Jensen",
//                                          Address = "Skolevej 3",
//                                          ZipCode = 4510,
//                                          City = "Dumby",
//                                          Email = "Email@mail.dk",
//                                          CVR = 12345678
//                                      };
//            service.Create(customer1);
//            CustomerBO customer2 = new CustomerBO()
//                                       {
//                                           Firstname = "Lars",
//                                           Lastname = "Jensen",
//                                           Address = "Skolevej 3",
//                                           ZipCode = 4510,
//                                           City = "Dumby",
//                                           Email = "Email@mail.dk",
//                                           CVR = 12345678
//                                       };
//            customer2 = service.Create(customer2);
//            Assert.AreEqual(service.Get(customer2.Id).Firstname, "Lars");


//        }


        [TestMethod]
        public void TestGetCustomerById()
        {

            this.GetMemoContext().Database.EnsureDeleted();
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
            this.GetService().Create(customer1);
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
            customer2 = this.GetService().Create(customer2);
            Assert.AreEqual(this.GetService().Get(customer2.Id).Firstname, "Lars");

//        [TestMethod]
//        public void TestDeleteCustomer()
//        {
//            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));
//            CustomerBO customer1 = new CustomerBO()
//                                       {
//                                           Firstname = "Bo",
//                                           Lastname = "Jensen",
//                                           Address = "Skolevej 3",
//                                           ZipCode = 4510,
//                                           City = "Dumby",
//                                           Email = "Email@mail.dk",
//                                           CVR = 12345678
//                                       };
//            customer1 = service.Create(customer1);
//            CustomerBO customer2 = new CustomerBO()
//                                       {
//                                           Firstname = "Lars",
//                                           Lastname = "Jensen",
//                                           Address = "Skolevej 3",
//                                           ZipCode = 4510,
//                                           City = "Dumby",
//                                           Email = "Email@mail.dk",
//                                           CVR = 12345678
//                                       };
//            customer2 = service.Create(customer2);
//            service.Delete(customer2.Id);
//            Assert.AreEqual(service.GetAll().Count, 1);
//            Assert.AreEqual(service.Get(customer1.Id).Firstname, "Bo");


//        }


        [TestMethod]
        public void TestDeleteCustomer()
        {
            this.GetMemoContext().Database.EnsureDeleted();
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
            customer1 = GetService().Create(customer1);
            Assert.AreEqual(GetService().GetAll().Count, 1);
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
            customer2 = GetService().Create(customer2);
            Assert.AreEqual(GetService().GetAll().Count, 2);
            GetService().Delete(customer2.Id);
            Assert.AreEqual(GetService().GetAll().Count, 1);
            Assert.AreEqual(GetService().Get(customer1.Id).Firstname, "Bo");

        }

//        [TestMethod] NOT WORKING
//        public void TestUpdateCustomer()
//        {
//            this.GetMemoContext().Database.EnsureDeleted();

//        [TestMethod]
//        public void TestUpdateCustomer()
//        {
//            CustomerService service = new CustomerService(new DALFacade(new DbOptions()));

//            CustomerBO customer1 = new CustomerBO()
//                                       {
//                                           Firstname = "Bo",
//                                           Lastname = "Jensen",
//                                           Address = "Skolevej 3",
//                                           ZipCode = 4510,
//                                           City = "Dumby",
//                                           Email = "Email@mail.dk",
//                                           CVR = 12345678
//                                       };

//            customer1 = GetService().Create(customer1);
//            Assert.AreEqual(GetService().Get(1).Firstname, "Bo");
//            this.GetMemoContext().Database.EnsureDeleted();
//            customer1.Firstname = "Lars";
//
//            GetService().Update(customer1);
//            Assert.AreEqual(GetService().Get(1).Firstname, "Lars");
//
//        }

        //Generer samme ny bll, skal have ny service hvergang. 
        public IService<CustomerBO> GetService()
        {
            var c = this.GetMemoContext();

            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));
            IService<CustomerBO> service = new CustomerService(dalFacadeMock.Object);

            return service;
        }

        public OrderSystemContext GetMemoContext()
        {
            var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
                .UseInMemoryDatabase("Database").Options);
            return c;
        }
    }
}

//            customer1 = service.Create(customer1);

//            customer1.Firstname = "Lars";

//            service.Update(customer1);
//            Assert.AreEqual(service.Get(1).Firstname, "Lars");

//        }
//    }
//}

