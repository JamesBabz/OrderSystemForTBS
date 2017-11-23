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
        [TestMethod]
        public void CreateMethod()
        {

            //Creates an InMemoryDatabaseContext for use in the UOW
            var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
                .UseInMemoryDatabase("Database").Options);

            //Creates a mock DALFacade and initiates it with the InMemoeryDatabaseContext
            Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
            dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));

            //Creates the Service
            IService<PropositionBO> service = new PropositionService(dalFacadeMock.Object);


            var snurf = service.Create(new PropositionBO() { Title = "ost" });

            //Expected results
            Assert.IsNotNull(snurf);
            Assert.AreEqual(snurf.Title, "ost");
        }

    }
}
