using Moq;

namespace UnitTest
{
    using BLL;
    using BLL.BusinessObjects;
    using BLL.Services;
    using DAL;
    using DAL.Context;
    using DAL.UOW;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            this.GetService().Create(customer);
            Assert.AreEqual(this.GetService().GetAll().Count, 1);
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
        }

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

        }


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

        [TestMethod]
        public void TestUpdateCustomer()
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
            customer1.Firstname = "Knud";
            this.GetService().Update(customer1);
            Assert.AreEqual("Knud", this.GetService().Get(1).Firstname);

        }
    

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

