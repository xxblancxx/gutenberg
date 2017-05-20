using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Gutenberg;

namespace GutenbergTests
{
    [TestClass]
    public class BooksTests
    {
        /// <summary>
        /// Test for method that gets a list of all the books and their authors, which mentions the input city - mysql db
        /// </summary>
        /// <precondition>A city name is input</precondition>
        /// <action>mysql database is called through facade layer</action>
        /// <postcondition>A list of books is given</postcondition>
        [TestMethod]
        public void GetBooksContainingCityMysql()
        {
            ////Setup of mock
            //var expectedBooks = new List<Book> {new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}};

            //Mock<IDependance> mock = new Mock<IDependance>();
            //mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}});


            //No longer mock data: but 700 book testing subset.
            var expectedBooks = new List<Gutenberg.Model.Book>();
            var book1 = new Gutenberg.Model.Book(638, "Modern India");
            book1.Authors.Add(new Gutenberg.Model.Author(400, "William Eleroy Curtis"));

            var book2 = new Gutenberg.Model.Book(141, "Sketches of the East Africa Campaign");
            book2.Authors.Add(new Gutenberg.Model.Author(108, "Robert Valentine Dolbey"));

            var book3 = new Gutenberg.Model.Book(4, "The Warriors");
            book3.Authors.Add(new Gutenberg.Model.Author(4, "Lindsay, Anna Robertson Brown"));

            var book4 = new Gutenberg.Model.Book(457, "The World of Waters");
            book4.Authors.Add(new Gutenberg.Model.Author(297, "Mrs. David Osborne"));

            var book5 = new Gutenberg.Model.Book(476, "Van Bibber and Others");
            book5.Authors.Add(new Gutenberg.Model.Author(313, "Richard Harding Davis"));

            //Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            //Test Mysql through facade
            var books = facade.GetBooksWithCityMysql("Zanzibar");
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Id, books[i].Id);
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
                Assert.AreEqual(expectedBooks[i].Authors[0].Id, books[i].Authors[0].Id);
                Assert.AreEqual(expectedBooks[i].Authors[0].Name, books[i].Authors[0].Name);
            }
        }


        /// <summary>
        /// Test for method that gets a list of all the books that mentions the input city - MongoDB db
        /// </summary>
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
        /// <precondition>A book title is input</precondition>
        /// <action>Mysql database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetCitiesInTitleMysql()
        {
            // Setup mock
            //var expectedCities = new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
            //                                      new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
            //                                      new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}};

            //Mock<IDependance> mock = new Mock<IDependance>();
            //mock.Setup(o => o.Cities()).Returns(new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
            //                                                     new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
            //                                                     new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}});

            // Setup expected data from 700 book subset.
            var expectedCities = new List<Gutenberg.Model.City>();
            var city1 = new Gutenberg.Model.City(5861897, "Fairbanks", 64.8377800, -147.7163900);
            var city2 = new Gutenberg.Model.City(5780993, "Salt Lake City", 40.7607800, -111.8910500);

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var cities = facade.GetCitiesInTitleMysql("Jingle Bells");
            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].Id, cities[i].Id);
                Assert.AreEqual(expectedCities[i].Name, cities[i].Name);
                Assert.AreEqual(expectedCities[i].Latitude, cities[i].Latitude);
                Assert.AreEqual(expectedCities[i].Longitude, cities[i].Longitude);
            }
        }

        /// <summary>
        /// Test for method that gets a list of all cities mentioned within a book title - MongoDB
        /// </summary>
        /// <precondition>A book title is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetCitiesInTitleMongoDB()
        {
            // Setup
            var expectedCities = new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                  new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                  new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Cities()).Returns(new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                                 new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                                 new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}});

            // Test
            ConnectionFacade facade = new ConnectionFacade();
            var cities = facade.GetCitiesInTitleMongoDB(mock.Object);
            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].Name, cities[i].Name);
                Assert.AreEqual(expectedCities[i].Latitude, cities[i].Latitude);
                Assert.AreEqual(expectedCities[i].Longitude, cities[i].Longitude);
            }
        }

        /// <summary>
        /// Test for method that gets a list of all cities mentioned within an author's collection of books - Mysql
        /// </summary>
        /// <precondition>An author name is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetCitiesWithAuthorMysql()
        {
            // Setup
            //var expectedCities = new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
            //                                      new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
            //                                      new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}};

            //Mock<IDependance> mock = new Mock<IDependance>();
            //mock.Setup(o => o.Cities()).Returns(new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
            //                                                     new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
            //                                                     new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}});

            var expectedCities = new List<Gutenberg.Model.City>();
            var city1 = new Gutenberg.Model.City(5861897, "Fairbanks", 64.8377800, -147.7163900);
            var city2 = new Gutenberg.Model.City(5780993, "Salt Lake City", 40.7607800, -111.8910500);

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var cities = facade.GetCitiesWithAuthorMysql("Helen Bannerman");
            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].Name, cities[i].Name);
                Assert.AreEqual(expectedCities[i].Latitude, cities[i].Latitude);
                Assert.AreEqual(expectedCities[i].Longitude, cities[i].Longitude);
            }
        }

        /// <summary>
        /// Test for method that gets a list of all cities mentioned within an author's collection of books - MongoDB
        /// </summary>
        /// <precondition>An author name is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetCitiesWithAuthorMongoDB()
        {
            // Setup
            var expectedCities = new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                  new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                  new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Cities()).Returns(new List<City> { new City {Name="København",Latitude=1.1234,Longitude=4.1234},
                                                                 new City {Name="Odense",Latitude=2.1234,Longitude=5.1234},
                                                                 new City {Name="Roskilde",Latitude=3.1234,Longitude=6.1234}});

            // Test
            ConnectionFacade facade = new ConnectionFacade();
            var cities = facade.GetCitiesWithAuthorMongoDB(mock.Object);
            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].Name, cities[i].Name);
                Assert.AreEqual(expectedCities[i].Latitude, cities[i].Latitude);
                Assert.AreEqual(expectedCities[i].Longitude, cities[i].Longitude);
            }
        }

        /// <summary>
        /// Test for method that gets a list of all books with mentioned cities in the local area - Mysql
        /// </summary>
        /// <precondition>A latitude and longtitude is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetBooksMentionedInAreaMysql()
        {
            // Setup
            //var expectedBooks = new List<Book> { new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}};

            //Mock<IDependance> mock = new Mock<IDependance>();
            //mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}});

            //No longer mock data: but 700 book testing subset.
            var expectedBooks = new List<Gutenberg.Model.Book>();
            var book1 = new Gutenberg.Model.Book(104, "Turkey: A Past and a Future");
            book1.Authors.Add(new Gutenberg.Model.Author(79, "Arnold Joseph Toynbee"));

            var book2 = new Gutenberg.Model.Book(110, "The Great Events by Famous Historians, Volume 5");
            book2.Authors.Add(new Gutenberg.Model.Author(14, "Various"));

            var book3 = new Gutenberg.Model.Book(261, "A General History and Collection of Voyages and Travels, Vol. 1");
            book3.Authors.Add(new Gutenberg.Model.Author(178, "Robert Kerr"));

            var book4 = new Gutenberg.Model.Book(282, "Beacon Lights of History, Volume XIV");
            book4.Authors.Add(new Gutenberg.Model.Author(147, "John Lord"));

            var book5 = new Gutenberg.Model.Book(390, "The Lands of the Saracen");
            book5.Authors.Add(new Gutenberg.Model.Author(257, "Bayard Taylor"));

            var book6 = new Gutenberg.Model.Book(491, "A Woman's Journey Round the World");
            book6.Authors.Add(new Gutenberg.Model.Author(323, "Ida Pfeiffer"));

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            //Test Mysql through facade
            var books = facade.GetBooksMentionedInAreaMysql(36, 43);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Id, books[i].Id);
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
                Assert.AreEqual(expectedBooks[i].Authors[0].Id, books[i].Authors[0].Id);
                Assert.AreEqual(expectedBooks[i].Authors[0].Name, books[i].Authors[0].Name);
            }
        }

        /// <summary>
        /// Test for method that gets a list of all books with mentioned cities in the local area - MongoDB
        /// </summary>
        /// <precondition>An latitude and longtitude is input</precondition>
        /// <action>MongoDB database is called through facade layer</action>
        /// <postcondition>A list of cities is given, containing geolocations</postcondition>
        [TestMethod]
        public void GetBooksMentionedInAreaMongoDB()
        {
            // Setup
            var expectedBooks = new List<Book> { new Book {Title="Den Lille Havfrue"},
                                                new Book {Title = "Den Grimme Ælling"},
                                                new Book {Title="Tommelise"}};

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
                                                new Book {Title = "Den Grimme Ælling"},
                                                new Book {Title="Tommelise"}});

            // Test
            ConnectionFacade facade = new ConnectionFacade();
            //Test MongoDB through facade
            var books = facade.GetBooksOfNearbyCoordinatesMongoDB(mock.Object);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
            }
        }

        /// <summary>
        /// Test for method that gets an image from the google static map API - MongoDB
        /// </summary>
        /// <precondition>Latitude and longitude for cities to plot on the map</precondition>
        /// <action>Google static map API is called</action>
        /// <postcondition>An image with geolocations is returned</postcondition>
        [TestMethod]
        public void GetStaticMap()
        {

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
        public virtual List<City> GetCitiesWithAuthorMysql(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Cities();
        }
        public virtual List<City> GetCitiesWithAuthorMongoDB(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Cities();
        }
        public virtual List<Book> GetBooksOfNearbyCoordinatesMysql(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Books();
        }
        public virtual List<Book> GetBooksOfNearbyCoordinatesMongoDB(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.Books();
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
