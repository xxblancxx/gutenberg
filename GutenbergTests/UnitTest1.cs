using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GutenbergTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void GetBooksTest()
        {
            const string expectedBook = "Huckleberry Finn";

            Mock<IDependance> myMock = new Mock<IDependance>();
            myMock.Setup(m => m.GiveMeABook()).Returns("Huckleberry Finn");

            MyMockObject myobject = new MyMockObject();

            string someString = myobject.GiveMeABook(myMock.Object);

            Assert.AreEqual(expectedBook, someString);
            myMock.VerifyAll();
        }
    }

    public class MyMockObject
    {
        public virtual string GiveMeABook(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.GiveMeABook();
        }
    }

    public interface IDependance
    {
        string GiveMeABook();
    }
}
