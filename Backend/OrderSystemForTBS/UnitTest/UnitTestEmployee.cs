
 using System;
using System.Data.Entity;
using System.Transactions;
using BLL;
using BLL.BusinessObjects;
using BLL.Converters;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Entities;
using DAL.Facade;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
  [TestClass]
   public class UnitTestEmployee
    {
        private EmployeeConverter employeeConverter = new EmployeeConverter();

        [TestMethod]
        public void getEmployees()
        {
            this.GetMemoContext().Database.EnsureDeleted();
            EmployeeBO employee1 = new EmployeeBO()
            {

                Firstname = "Bo",
                Lastname = "Pedersen",
                Username = "jbs",
                Password = "1234",
                MacAddress = "asdDDFASFDSF"
            };
            this.GetService().Create(employee1);
            EmployeeBO employee2 = new EmployeeBO()
            {
                Firstname = "Bent",
                Lastname = "Nygaard",
                Username = "jbs",
                Password = "1234",
                MacAddress = "asdDDFASFDSF"
            };
            employee2 = this.GetService().Create(employee2);
            Assert.AreEqual(this.GetService().Get(employee2.Id).Firstname, "Bent");
        }

        [TestMethod]
        public void TestCreateEmployee()
        {
            this.GetMemoContext().Database.EnsureDeleted();
            EmployeeBO employee = new EmployeeBO()
            {
                Firstname = "Bent",
                Lastname = "Nygaard",
                Username = "jbs",
                Password = "1234",
                MacAddress = "asdDDFASFDSF"
            };
            employee = this.GetService().Create(employee);
            Assert.IsNotNull(employee);
        }

        //Generer samme ny bll, skal have ny service hvergang. 
        public IService<EmployeeBO> GetService()
        {
            var c = this.GetMemoContext();

            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));
            IService<EmployeeBO> service = new EmployeeService(dalFacadeMock.Object);

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



