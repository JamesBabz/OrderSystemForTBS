using System;
using System.Collections.Generic;
using System.Text;

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

    using Moq;

    [TestClass]
    public class UnitTestVisit
    {
        [TestMethod]
        public void TestCreateVisit()
        {
            this.GetMemoContext().Database.EnsureDeleted();
            VisitBO visit = new VisitBO()
                                {
                                    Title = "Besøg",
                                    Description = "Godt besøg",
                                    DateOfVisit = DateTime.Today,
                                    IsDone = true,
                                    CustomerId = 2,
                                    EmployeeId = 1
                                };
            this.GetService().Create(visit);
            Assert.IsNotNull(visit);
            Assert.AreEqual("Besøg", visit.Title);
            Assert.AreEqual("Godt besøg", visit.Description);
        }

        [TestMethod]
        public void TestGetAll()
        {
            this.GetMemoContext().Database.EnsureDeleted();
            VisitBO visit1 = new VisitBO()
                                 {
                                     Title = "Besøg",
                                     Description = "Godt besøg",
                                     DateOfVisit = DateTime.Today,
                                     IsDone = true,
                                     CustomerId = 2,
                                     EmployeeId = 1
                                 };
            this.GetService().Create(visit1);
            Assert.AreEqual(1, this.GetService().GetAll().Count);
            VisitBO visit2 = new VisitBO()
                                 {
                                     Title = "Besøg2",
                                     Description = "Godt besøg2",
                                     DateOfVisit = DateTime.Today,
                                     IsDone = false,
                                     CustomerId = 2,
                                     EmployeeId = 1
                                 };
            this.GetService().Create(visit2);
            Assert.AreEqual(2, this.GetService().GetAll().Count);
        }

        [TestMethod]
        public void TestGetVisitById()
        {
            this.GetMemoContext().Database.EnsureDeleted();

            VisitBO visit1 = new VisitBO()
                                 {
                                     Title = "Besøg",
                                     Description = "Godt besøg",
                                     DateOfVisit = DateTime.Today,
                                     IsDone = true,
                                     CustomerId = 2,
                                     EmployeeId = 1
                                 };
            this.GetService().Create(visit1);
            Assert.AreEqual(1, this.GetService().GetAll().Count);
            VisitBO visit2 = new VisitBO()
                                 {
                                     Title = "Besøg2",
                                     Description = "Godt besøg2",
                                     DateOfVisit = DateTime.Today,
                                     IsDone = false,
                                     CustomerId = 2,
                                     EmployeeId = 1
                                 };
            visit2 = this.GetService().Create(visit2);

            Assert.AreEqual("Besøg2", this.GetService().Get(visit2.Id).Title);
            Assert.AreEqual(2, this.GetService().GetAll().Count);
        }

        [TestMethod]
        public void TestUpdateVisit()
        {
            this.GetMemoContext().Database.EnsureDeleted();

            VisitBO visit = new VisitBO()
                                {
                                    Id = 1,
                                    Title = "Besøg",
                                    Description = "Godt besøg",
                                    DateOfVisit = DateTime.Today,
                                    IsDone = true,
                                    CustomerId = 2,
                                    EmployeeId = 1
                                };
            visit = this.GetService().Create(visit);
            Assert.AreEqual("Besøg", this.GetService().Get(visit.Id).Title);
            visit.Title = "Visit";
            visit.Description = "Nice visit";
            this.GetService().Update(visit);
            visit.IsDone = false;
            visit.DateOfVisit = DateTime.Now;
            this.GetService().Update(visit);
            Assert.AreEqual("Visit", this.GetService().Get(1).Title);
            Assert.AreEqual("Nice visit", this.GetService().Get(1).Description);
            Assert.AreEqual(false, this.GetService().Get(1).IsDone);
           
        }

        private IVisitService GetService()
        {
            var c = this.GetMemoContext();

            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));
            IVisitService service = new VisitService(dalFacadeMock.Object);

            return service;
        }

        private OrderSystemContext GetMemoContext()
        {
            var c = new OrderSystemContext(
                new DbContextOptionsBuilder<OrderSystemContext>().UseInMemoryDatabase("Database").Options);
            return c;
        }
    }
}