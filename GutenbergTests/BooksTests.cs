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

        /// <summary>
        /// Test for method that gets a list of all cities mentioned within a book title - Mysql
        /// </summary>
        /// <param name="city"></param>
        /// <precondition>A book title is input</precondition>
        /// <action>Mysql database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetCitiesInTitleMysql()
        {
            var expectedCities = new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                  new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                  new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Cities()).Returns(new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                                 new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                                 new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}});
            ConnectionFacade facade = new ConnectionFacade();
            var cities = facade.GetCitiesInTitleMysql(mock.Object);
            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].Name, cities[i].Name);
                Assert.AreEqual(expectedCities[i].Latitude, cities[i].Latitude);
                Assert.AreEqual(expectedCities[i].Longitude, cities[i].Longitude);
            }
        }

        /// <summary>
        /// Test for method that gets a list of all cities mentioned within a book title - MongoDB
        /// </summary>
        /// <param name="city"></param>
        /// <precondition>A book title is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetCitiesInTitleMongoDB()
        {
            var expectedCities = new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                  new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                  new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Cities()).Returns(new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                                 new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                                 new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}});
            ConnectionFacade facade = new ConnectionFacade();
            var cities = facade.GetCitiesInTitleMongoDB(mock.Object);
            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].Name, cities[i].Name);
                Assert.AreEqual(expectedCities[i].Latitude, cities[i].Latitude);
                Assert.AreEqual(expectedCities[i].Longitude, cities[i].Longitude);
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
        public virtual List<City> GetCitiesInTitleMysql(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Cities();
        }
        public virtual List<City> GetCitiesInTitleMongoDB(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Cities();
        }
    }

    public interface IDependance
    {
        List<Book> Books();
        List<City> Cities();
    }

    public class Book
    {
        public string Title { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
