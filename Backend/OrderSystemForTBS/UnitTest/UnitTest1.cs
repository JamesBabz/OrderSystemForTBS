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
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str1 = "1";
            var str2 = "1";
            Assert.AreEqual(str1,str2);
        }

        //[TestMethod]
        //public void CreateMethod()
        //{
        //    var c = new OrderSystemContext(new DbContextOptionsBuilder<OrderSystemContext>()
        //        .UseInMemoryDatabase("Database").Options);
            
        //    Mock<IDALFacade> dalFacadeMock = new Mock<IDALFacade>();
        //    dalFacadeMock.Setup(x => x.UnitOfWork).Returns(new UnitOfWork(c));

        //    IService<PropositionBO> service = new PropositionService(dalFacadeMock.Object);
        //    var snurf = service.Create(new PropositionBO() { Title = "ost" });

        //    Assert.IsNotNull(snurf);
        //}
    }
}
