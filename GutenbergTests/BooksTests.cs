using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace GutenbergTests
{
    [TestClass]
    public class BooksTests
    {
        /// <summary>
        /// Test for method that gets a list of all the books that mentions the input city - mysql db
        /// </summary>
        /// <param name="city"></param>
        /// <precondition>A city name is input</precondition>
        /// <action>mysql database is called through facade layer</action>
        /// <postcondition>A list of books is given</postcondition>
        [TestMethod]
        public void GetBooksContainingCityMysql()
        {
            //Setup of mock
            var expectedBooks = new List<Book> {new Book {Title="Den Lille Havfrue"},
                                                new Book {Title = "Den Grimme Ælling"},
                                                new Book {Title="Tommelise"}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
                                                new Book {Title = "Den Grimme Ælling"},
                                                new Book {Title="Tommelise"}});

            //Test
            ConnectionFacade facade = new ConnectionFacade();
            //Test Mysql through facade
            var books = facade.GetBooksWithCityMysql(mock.Object);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
            }
        }


        /// <summary>
        /// Test for method that gets a list of all the books that mentions the input city - MongoDB db
        /// </summary>
        /// <param name="city"></param>
        /// <precondition>A city name is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of books is given</postcondition>
        [TestMethod]
        public void GetBooksContainingCityMongoDB()
        {
            //Setup of mock
            var expectedBooks = new List<Book> {new Book {Title="Den Lille Havfrue"},
                                                new Book {Title = "Den Grimme Ælling"},
                                                new Book {Title="Tommelise"}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
                                                new Book {Title = "Den Grimme Ælling"},
                                                new Book {Title="Tommelise"}});

            //Test
            ConnectionFacade facade = new ConnectionFacade();
            //Test MongoDB through facade
            var books = facade.GetBooksWithCityMongoDB(mock.Object);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
            }
        }
    }

    public class ConnectionFacade
    {
        public virtual List<Book> GetBooksWithCityMysql(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Books();
        }
        public virtual List<Book> GetBooksWithCityMongoDB(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Books();
        }
    }

    public interface IDependance
    {
        List<Book> Books();
    }

    public class Book
    {
        public string Title { get; set; }
    }

    public class Geolocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
