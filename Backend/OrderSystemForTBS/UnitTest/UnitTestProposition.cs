using System;
using System.Collections.Generic;
using BLL;
using BLL.BusinessObjects;
using BLL.Facade;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        public IService<PropositionBO> GetMockService()
        {

            var c = GetInMemoryContext();
            //Creates a mock DALFacade and initiates it with the InMemoeryDatabaseContext
            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));

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

            List<PropositionBO> allProps = GetMockService().GetAll();

            Assert.IsNotNull(allProps);
            Assert.AreEqual(2, allProps.Count);
            Assert.IsNotNull(allProps.Find(x => x.Description == "beskrivelse"));
            Assert.IsNotNull(allProps.Find(x => x.Title == "marmelade"));
        }
    }
}
