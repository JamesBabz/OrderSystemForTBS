using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
