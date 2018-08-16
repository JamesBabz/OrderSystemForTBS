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
using BLL.IServices;

namespace UnitTest
{
    [TestClass]
    public class UnitTestReceipt
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

        public IReceiptService GetMockService()
        {

            var c = GetInMemoryContext();
            var dalFacadeMock = GetDalFacadeMock(c);

            //Creates the Service
            IReceiptService service = new ReceiptService(dalFacadeMock.Object);
            return service;
        }

        [TestMethod]
        public void CreateReceiptMethod()
        {
            GetInMemoryContext().Database.EnsureDeleted();

            var snurf = new ReceiptBO()
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
        public void GetReceiptMethod()
        {

            GetInMemoryContext().Database.EnsureDeleted();

            ReceiptBO prop1 = new ReceiptBO()
            {
                Title = "ost",
                Description = "beskrivelse",
                CreationDate = DateTime.MinValue,
                EmployeeId = 2,
                FileId = 3,
                CustomerId = 4
            };

            prop1 = GetMockService().Create(prop1);

            ReceiptBO prop2 = new ReceiptBO()
            {
                Title = "marmelade",
                Description = "en ny beskrivelse",
                CreationDate = DateTime.MinValue.Add(TimeSpan.FromMinutes(3)),
                EmployeeId = 3,
                FileId = 4,
                CustomerId = 5
            };

            prop2 = GetMockService().Create(prop2);

            ReceiptBO snurf = GetMockService().Get(prop1.Id);

            //Expected results
            Assert.IsNotNull(snurf);
            Assert.AreEqual("ost", snurf.Title);

        }

        [TestMethod]
        public void GetAllReceiptsMethod()
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

            ReceiptBO prop1 = new ReceiptBO()
            {
                Title = "qwe1",
                CustomerId = cust1.Id,
                EmployeeId = employee.Id

            };

            prop1 = GetMockService().Create(prop1);

            ReceiptBO prop2 = new ReceiptBO()
            {
                Title = "qwe2",
                CustomerId = cust1.Id,
                EmployeeId = employee.Id
            };

            prop2 = GetMockService().Create(prop2);

            ReceiptBO prop3 = new ReceiptBO()
            {
                Title = "qwe3",
                CustomerId = cust1.Id,
                EmployeeId = employee.Id
            };

            prop3 = GetMockService().Create(prop3);

            ReceiptBO prop4 = new ReceiptBO()
            {
                Title = "qwe4",
                CustomerId = cust2.Id,
                EmployeeId = employee.Id
            };

            prop4 = GetMockService().Create(prop4);

            ReceiptBO prop5 = new ReceiptBO()
            {
                Title = "qwe5",
                CustomerId = cust2.Id,
                EmployeeId = 1
            };

            prop5 = GetMockService().Create(prop5);

            ReceiptBO prop6 = new ReceiptBO()
            {
                Title = "qwe6",
                CustomerId = cust2.Id,
                EmployeeId = employee.Id
            };

            prop6 = GetMockService().Create(prop6);

            List<ReceiptBO> allProps = GetMockService().GetAllById(cust1.Id);

            Assert.IsNotNull(allProps);
            Assert.AreEqual(3, allProps.Count);
            Assert.AreEqual(cust1.Firstname, allProps.Find(x => x.Title == "qwe3").Customer.Firstname);
            Assert.IsNull(allProps.Find(x => x.Title == "qwe4"));


        }

        [TestMethod]
        public void DeletePropositionMethod()
        {
            GetInMemoryContext().Database.EnsureDeleted();

            var prop1 = new ReceiptBO()
            {
                Title = "nummerEt"
            };
            var prop2 = new ReceiptBO()
            {
                Title = "nummerTo"
            };
            var prop3 = new ReceiptBO()
            {
                Title = "nummerTre"
            };
            var prop4 = new ReceiptBO()
            {
                Title = "nummerFire"
            };
            prop1 = GetMockService().Create(prop1);
            prop2 = GetMockService().Create(prop2);
            prop3 = GetMockService().Create(prop3);
            prop4 = GetMockService().Create(prop4);

            Assert.IsNotNull(GetMockService().Get(prop3.Id));
            prop3 = GetMockService().Delete(prop3.Id);
            Assert.IsNull(GetMockService().Get(prop3.Id));


        }

        [TestMethod]
        public void UpdatePropositionMethod()
        {
            GetInMemoryContext().Database.EnsureDeleted();

            var prop = new ReceiptBO()
            {
                Title = "Tit",
                Description = "Desc"
            };

            var newProp = new ReceiptBO()
            {
                Title = "Title",
                Description = "Description"
            };

            prop = GetMockService().Create(prop);

            Assert.AreEqual("Tit", prop.Title);
            prop = GetMockService().Update(GetMockService().Create(newProp));
            Assert.AreEqual("Title", prop.Title);

        }
    }
}
