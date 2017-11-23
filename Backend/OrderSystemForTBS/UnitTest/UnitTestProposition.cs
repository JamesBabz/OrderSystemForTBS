using System;
using BLL;
using BLL.BusinessObjects;
using BLL.Facade;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class UnitTestProposition
    {

        public IService<PropositionBO> getMockService()
        {

            //Creates an InMemoryDatabaseContext for use in the UOW
            var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
                .UseInMemoryDatabase("Database").Options);
            c.Database.EnsureDeleted();

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
            var service = getMockService();

            var snurf = service.Create(new PropositionBO()
            {
                Id = 1,
                Title = "ost",
                Description = "beskrivelse",
                CreationDate = DateTime.MinValue,
                EmployeeId = 2,
                FileId = 3,
                CustomerId = 4
            });

            //Expected results
            Assert.IsNotNull(snurf);
            Assert.AreEqual("ost", snurf.Title);
        }

        [TestMethod]
        public void GetPropositionMethod()
        {
            var service = getMockService();

            var snurf = service.Create(new PropositionBO()
            {
                Id = 1,
                Title = "ost",
                Description = "beskrivelse",
                CreationDate = DateTime.MinValue,
                EmployeeId = 2,
                FileId = 3,
                CustomerId = 4
            });

            var snurf2 = service.Create(new PropositionBO()
            {
                Id = 2,
                Title = "ost",
                Description = "beskrivelse",
                CreationDate = DateTime.MinValue,
                EmployeeId = 3,
                FileId = 4,
                CustomerId = 5
            });

            snurf = service.Get(1);

            //Expected results
            Assert.IsNotNull(snurf);
            Assert.AreEqual("ost", snurf.Title);
        }
    }}
