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
    class UnitTestProposition
    {
        [TestMethod]
        public void CreateMethod()
        {
            var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
                .UseInMemoryDatabase("Database").Options);
            Mock<UnitOfWork> uowMock = new Mock<UnitOfWork>(c);
           
            Mock<IDALFacade> dalFacaeMock = new Mock<IDALFacade>();
            dalFacaeMock.Setup(x => x.UnitOfWork).Returns(uowMock.Object);

            IService<PropositionBO> service = new PropositionService(dalFacaeMock.Object);
            var snurf = service.Create(new PropositionBO(){Title = "ost"});

            Assert.IsNotNull(snurf);
            Assert.AreEqual(snurf.Title, "ost");
        }

    }
}
