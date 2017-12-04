using System;
using System.Collections.Generic;
using System.Text;
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
    public class UnitTestEquipment
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

        public IService<EquipmentBO> GetMockService()
        {

            var c = GetInMemoryContext();
            var dalFacadeMock = GetDalFacadeMock(c);

            //Creates the Service
            IService<EquipmentBO> service = new EquipmentService(dalFacadeMock.Object);
            return service;
        }

        [TestMethod]
        public void CreateEquipmentMethod()
        {

            GetInMemoryContext().Database.EnsureDeleted();

            var equipment = new EquipmentBO()
            {
                Name = "Traktor",
                CustomerId = 1
            };

            GetMockService().Create(equipment);

            //Expected results
            Assert.IsNotNull(equipment);
            Assert.AreEqual("Traktor", equipment.Name);
        }

        [TestMethod]
        public void GetEquipmentMethod()
        {

            GetInMemoryContext().Database.EnsureDeleted();

            EquipmentBO equip1 = new EquipmentBO()
            {
                Name = "Traktor",
                CustomerId = 1
            };

            equip1 = GetMockService().Create(equip1);

            EquipmentBO equip2 = new EquipmentBO()
            {
                Name = "Plov",
                CustomerId = 2
            };

            equip2 = GetMockService().Create(equip2);

            EquipmentBO equip = GetMockService().Get(equip2.Id);

            //Expected results
            Assert.IsNotNull(equip);
            Assert.AreEqual("Plov", equip.Name);

        }

        [TestMethod]
        public void GetAllEquipmentMethod()
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

            cust2 = new CustomerService(GetDalFacadeMock(GetInMemoryContext()).Object).Create(cust2);

            EquipmentBO equip1 = new EquipmentBO()
            {
                Name = "Traktor",
                CustomerId = cust1.Id
            };

            equip1 = GetMockService().Create(equip1);

            EquipmentBO equip2 = new EquipmentBO()
            {
                Name = "Plov",
                CustomerId = cust1.Id
            };

            equip2 = GetMockService().Create(equip2);

            EquipmentBO equip3 = new EquipmentBO()
            {
                Name = "Vogn",
                CustomerId = cust1.Id
            };

            equip3 = GetMockService().Create(equip3);

            EquipmentBO equip4 = new EquipmentBO()
            {
                Name = "AndenVogn",
                CustomerId = cust2.Id
            };

            equip4 = GetMockService().Create(equip4);

            EquipmentBO equip5 = new EquipmentBO()
            {
                Name = "AndenTraktor",
                CustomerId = cust2.Id
            };

            equip5 = GetMockService().Create(equip5);

            EquipmentBO equip6 = new EquipmentBO()
            {
                Name = "AndenPlov",
                CustomerId = cust2.Id
            };

            equip6 = GetMockService().Create(equip6);

            List<EquipmentBO> allEquipment = GetMockService().GetAllById(cust1.Id);

            Assert.IsNotNull(allEquipment);
            Assert.AreEqual(3, allEquipment.Count);
            Assert.AreEqual(cust1.Firstname, allEquipment.Find(x => x.Name == "Plov").Customer.Firstname);
            Assert.IsNull(allEquipment.Find(x => x.Name == "hej"));
        }
    }
}

