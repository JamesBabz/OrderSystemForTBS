using System;
using System.Collections.Generic;
using BLL;
using BLL.BusinessObjects;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class UnitTestProposition
    {

        public OrderSystemContext GetInMemoryContext()
        {
            //Creates an InMemoryDatabaseContext for use in the UOW
            var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
                .UseInMemoryDatabase("Database").Options);
            return c;
        }


        private static Mock<IDALFacade> GetDalFacadeMock(OrderSystemContext c)
        {
            //Creates a mock DALFacade and initiates it with the InMemoeryDatabaseContext
            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));
            return dalFacadeMock;
        }

        public IService<PropositionBO> GetMockService()
        {

            var c = GetInMemoryContext();
            var dalFacadeMock = GetDalFacadeMock(c);

            //Creates the Service
            IService<PropositionBO> service = new PropositionService(dalFacadeMock.Object);
            return service;
        }

        [TestMethod]
        public void CreatePropositionMethod()
        {

            GetInMemoryContext().Database.EnsureDeleted();

            var snurf = new PropositionBO()
            {
                Title = "ostemad",
                Description = "ostemad beskrivelse",
                CreationDate = DateTime.MinValue,
                EmployeeId = 2,
                FileId = 3,
                CustomerId = 4
            };

            GetMockService().Create(snurf);

            //Expected results
            Assert.IsNotNull(snurf);
            Assert.AreEqual("ostemad", snurf.Title);


        }

        [TestMethod]
        public void GetPropositionMethod()
        {

            GetInMemoryContext().Database.EnsureDeleted();

            PropositionBO prop1 = new PropositionBO()
            {
                Title = "ost",
                Description = "beskrivelse",
                CreationDate = DateTime.MinValue,
                EmployeeId = 2,
                FileId = 3,
                CustomerId = 4
            };

            prop1 = GetMockService().Create(prop1);

            PropositionBO prop2 = new PropositionBO()
            {
                Title = "marmelade",
                Description = "en ny beskrivelse",
                CreationDate = DateTime.MinValue.Add(TimeSpan.FromMinutes(3)),
                EmployeeId = 3,
                FileId = 4,
                CustomerId = 5
            };

            prop2 = GetMockService().Create(prop2);

            PropositionBO snurf = GetMockService().Get(prop1.Id);

            //Expected results
            Assert.IsNotNull(snurf);
            Assert.AreEqual("ost", snurf.Title);

        }

        [TestMethod]
        public void GetAllPropositionsMethod()
        {
            GetInMemoryContext().Database.EnsureDeleted();

            CustomerBO cust1 = new CustomerBO()
            {
                Firstname = "ham her",
                Lastname = "Fuhlendorff",
                Address = "testvej1",
                CVR = 12312312,
                City = "Kolding",
                Email = "sf@dfdssdffds.dk",
                Phone = 12345678,
                ZipCode = 6000

            };

            cust1 = new CustomerService(GetDalFacadeMock(GetInMemoryContext()).Object).Create(cust1);

            CustomerBO cust2 = new CustomerBO()
            {
                Firstname = "ikke ham her",
                Lastname = "Meyer",
                Address = "testvej2222",
                CVR = 12312312,
                City = "Esbjerg",
                Email = "tm@sefd.dk",
                Phone = 12345678,
                ZipCode = 6700

            };
            EmployeeBO employee = new EmployeeBO()
                                      {
                                          Firstname = "Sigurd",
                                          Lastname = "Hansen",
                                          Username = "User",
                                          Password = "Pass",
                                          MacAddress = "dfkmgkldfnmg"
                                      };
            employee = new EmployeeService(GetDalFacadeMock(this.GetInMemoryContext()).Object).Create(employee);

            cust2 = new CustomerService(GetDalFacadeMock(GetInMemoryContext()).Object).Create(cust2);

            PropositionBO prop1 = new PropositionBO()
            {
                Title = "qwe1",
                CustomerId = cust1.Id,
                EmployeeId = employee.Id

            };

            prop1 = GetMockService().Create(prop1);

            PropositionBO prop2 = new PropositionBO()
            {
                Title = "qwe2",
                CustomerId = cust1.Id,
                EmployeeId = employee.Id
            };

            prop2 = GetMockService().Create(prop2);

            PropositionBO prop3 = new PropositionBO()
            {
                Title = "qwe3",
                CustomerId = cust1.Id,
                EmployeeId = employee.Id
            };

            prop3 = GetMockService().Create(prop3);

            PropositionBO prop4 = new PropositionBO()
            {
                Title = "qwe4",
                CustomerId = cust2.Id,
                EmployeeId = employee.Id
            };

            prop4 = GetMockService().Create(prop4);

            PropositionBO prop5 = new PropositionBO()
            {
                Title = "qwe5",
                CustomerId = cust2.Id,
                EmployeeId = 1
            };

            prop5 = GetMockService().Create(prop5);

            PropositionBO prop6 = new PropositionBO()
            {
                Title = "qwe6",
                CustomerId = cust2.Id,
                EmployeeId = employee.Id
            };

            prop6 = GetMockService().Create(prop6);

            List<PropositionBO> allProps = GetMockService().GetAllById(cust1.Id);

            Assert.IsNotNull(allProps);
            Assert.AreEqual(3, allProps.Count);
            Assert.AreEqual(cust1.Firstname, allProps.Find(x => x.Title == "qwe3").Customer.Firstname);
            Assert.IsNull(allProps.Find(x => x.Title == "qwe4"));


        }
    }
}
