using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Gutenberg;
using System.Drawing;
using System.Net;
using System.IO;
using System.Linq;

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
            var book1 = new Gutenberg.Model.Book(0, "Denmark");
            book1.Authors.Add(new Gutenberg.Model.Author(79, "M. Pearson Thomson"));

            var book2 = new Gutenberg.Model.Book(0, "The 1990 CIA World Factbook");
            book2.Authors.Add(new Gutenberg.Model.Author(14, "United States.  Central Intelligence Agency"));

            var book3 = new Gutenberg.Model.Book(0, "The 1994 CIA World Factbook");
            book3.Authors.Add(new Gutenberg.Model.Author(177, "United States Central Intelligence Agency"));

            var book4 = new Gutenberg.Model.Book(0, "The 1997 CIA World Factbook");
            book4.Authors.Add(new Gutenberg.Model.Author(146, "United States. Central Intelligence Agency."));

            var book5 = new Gutenberg.Model.Book(0, "The 1998 CIA World Factbook");
            book5.Authors.Add(new Gutenberg.Model.Author(257, "United States.  Central Intelligence Agency."));


            expectedBooks.AddRange(new List<Gutenberg.Model.Book>() { book1, book2, book3, book4, book5 });

            //Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            //Test Mysql through facade
            var books = facade.GetBooksWithCityMysql("Esbjerg");
            books = books.OrderByDescending(x => x.Title).ToList();
            expectedBooks = expectedBooks.OrderByDescending(x => x.Title).ToList();
            Assert.AreEqual(expectedBooks.Count, books.Count);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                //Assert.AreEqual(expectedBooks[i].Id, books[i].Id);
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
                //Assert.AreEqual(expectedBooks[i].Authors[0].Id, books[i].Authors[0].Id);

                Assert.AreEqual(expectedBooks[i].Authors.Count, books[i].Authors.Count);
                expectedBooks[i].Authors = expectedBooks[i].Authors.OrderByDescending(a => a.Name).ToList();
                books[i].Authors = books[i].Authors.OrderByDescending(a => a.Name).ToList();
                for (int l = 0; l < books[i].Authors.Count; l++)
                {
                    Assert.AreEqual(expectedBooks[i].Authors[l].Name, books[i].Authors[l].Name);
                }
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
            //var expectedBooks = new List<Book> {new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}};

            //Mock<IDependance> mock = new Mock<IDependance>();
            //mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}});

            //No longer mock data: but 700 book testing subset.
            var expectedBooks = new List<Gutenberg.Model.Book>();
            var book1 = new Gutenberg.Model.Book(0, "Denmark");
            book1.Authors.Add(new Gutenberg.Model.Author(79, "M. Pearson Thomson"));

            var book2 = new Gutenberg.Model.Book(0, "The 1990 CIA World Factbook");
            book2.Authors.Add(new Gutenberg.Model.Author(14, "United States.  Central Intelligence Agency"));

            var book3 = new Gutenberg.Model.Book(0, "The 1994 CIA World Factbook");
            book3.Authors.Add(new Gutenberg.Model.Author(177, "United States Central Intelligence Agency"));

            var book4 = new Gutenberg.Model.Book(0, "The 1997 CIA World Factbook");
            book4.Authors.Add(new Gutenberg.Model.Author(146, "United States. Central Intelligence Agency."));

            var book5 = new Gutenberg.Model.Book(0, "The 1998 CIA World Factbook");
            book5.Authors.Add(new Gutenberg.Model.Author(257, "United States.  Central Intelligence Agency."));


            expectedBooks.AddRange(new List<Gutenberg.Model.Book>() { book1, book2, book3, book4, book5 });
            //Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            //Test MongoDB through facade
            var books = facade.GetBooksWithCityMongoDB("Esbjerg");
            books = books.OrderByDescending(x => x.Title).ToList();
            expectedBooks = expectedBooks.OrderByDescending(x => x.Title).ToList();

            Assert.AreEqual(expectedBooks.Count, books.Count);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
                //Assert.AreEqual(sortedExpectedBooks[i].Authors[0].Id, sortedBooks[i].Authors[0].Id);
                Assert.AreEqual(expectedBooks[i].Authors.Count, books[i].Authors.Count);

                expectedBooks[i].Authors = expectedBooks[i].Authors.OrderByDescending(a => a.Name).ToList();
                books[i].Authors = books[i].Authors.OrderByDescending(a => a.Name).ToList();
                for (int l = 0; l < books[i].Authors.Count; l++)
                {
                    Assert.AreEqual(expectedBooks[i].Authors[l].Name, books[i].Authors[l].Name);
                }
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
            expectedCities.Add(city1);
            expectedCities.Add(city2);

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var cities = facade.GetCitiesInTitleMysql("Jingle Bells");
            cities = cities.OrderByDescending(c => c.Latitude).ThenByDescending(c => c.Longitude).ToList();
            expectedCities = expectedCities.OrderByDescending(c => c.Latitude).ThenByDescending(c => c.Longitude).ToList();

            Assert.AreEqual(expectedCities.Count, cities.Count);
            for (int i = 0; i < expectedCities.Count; i++)
            {
                //Assert.AreEqual(expectedCities[i].Id, cities[i].Id);
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
            expectedCities.Add(city1);
            expectedCities.Add(city2);
            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var cities = facade.GetCitiesInTitleMongoDB("Jingle Bells");
            cities = cities.OrderByDescending(c => c.Latitude).ThenByDescending(c => c.Longitude).ToList();
            expectedCities = expectedCities.OrderByDescending(c => c.Latitude).ThenByDescending(c => c.Longitude).ToList();

            Assert.AreEqual(expectedCities.Count, cities.Count);
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
            expectedCities.Add(city1);
            expectedCities.Add(city2);
            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var cities = facade.GetCitiesWithAuthorMysql("Helen Bannerman");

           // Assert.AreEqual(expectedCities.Count, cities.Count);
            foreach (var city in expectedCities)
            {
                Assert.AreEqual(true, cities.Any(c => c.Name == city.Name && c.Latitude == city.Latitude && c.Longitude == city.Longitude));
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
            expectedCities.Add(city1);
            expectedCities.Add(city2);

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var cities = facade.GetCitiesWithAuthorMongoDB("Helen Bannerman");


            //Assert.AreEqual(expectedCities.Count, cities.Count);
            
            foreach (var city in expectedCities)
            {
                Assert.AreEqual(true, cities.Any(c => c.Name == city.Name && c.Latitude == city.Latitude && c.Longitude == city.Longitude));
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
            var book2 = new Gutenberg.Model.Book(0, "The 1990 CIA World Factbook");
            book2.Authors.Add(new Gutenberg.Model.Author(14, "United States.  Central Intelligence Agency"));

            var book3 = new Gutenberg.Model.Book(0, "The 1994 CIA World Factbook");
            book3.Authors.Add(new Gutenberg.Model.Author(177, "United States Central Intelligence Agency"));

            var book4 = new Gutenberg.Model.Book(0, "The 1997 CIA World Factbook");
            book4.Authors.Add(new Gutenberg.Model.Author(146, "United States. Central Intelligence Agency."));

            var book5 = new Gutenberg.Model.Book(0, "The 1998 CIA World Factbook");
            book5.Authors.Add(new Gutenberg.Model.Author(257, "United States.  Central Intelligence Agency."));


            expectedBooks.AddRange(new List<Gutenberg.Model.Book>() { book2, book3, book4, book5 });

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            //Test Mysql through facade

            var books = facade.GetBooksMentionedInAreaMysql(15, 43).OrderBy(b => b.Title).ToList();
            expectedBooks.OrderBy(b => b.Title);

            Assert.AreEqual(expectedBooks.Count, books.Count);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                //Assert.AreEqual(expectedBooks[i].Id, books[i].Id);
                Assert.AreEqual(expectedBooks[i].Title, books[i].Title);
                // Assert.AreEqual(expectedBooks[i].Authors[0].Id, books[i].Authors[0].Id);
                Assert.AreEqual(expectedBooks[i].Authors.Count, books[i].Authors.Count);
                expectedBooks[i].Authors = expectedBooks[i].Authors.OrderByDescending(a => a.Name).ToList();
                books[i].Authors = books[i].Authors.OrderByDescending(a => a.Name).ToList();
                for (int l = 0; l < books[i].Authors.Count; l++)
                {
                    Assert.AreEqual(expectedBooks[i].Authors[l].Name, books[i].Authors[l].Name);
                }
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
            //var expectedBooks = new List<Book> { new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}};

            //Mock<IDependance> mock = new Mock<IDependance>();
            //mock.Setup(o => o.Books()).Returns(new List<Book> { new Book {Title="Den Lille Havfrue"},
            //                                    new Book {Title = "Den Grimme Ælling"},
            //                                    new Book {Title="Tommelise"}});

            //No longer mock data: but 700 book testing subset.
            var expectedBooks = new List<Gutenberg.Model.Book>();
            var book2 = new Gutenberg.Model.Book(0, "The 1990 CIA World Factbook");
            book2.Authors.Add(new Gutenberg.Model.Author(14, "United States.  Central Intelligence Agency"));

            var book3 = new Gutenberg.Model.Book(0, "The 1994 CIA World Factbook");
            book3.Authors.Add(new Gutenberg.Model.Author(177, "United States Central Intelligence Agency"));

            var book4 = new Gutenberg.Model.Book(0, "The 1997 CIA World Factbook");
            book4.Authors.Add(new Gutenberg.Model.Author(146, "United States. Central Intelligence Agency."));

            var book5 = new Gutenberg.Model.Book(0, "The 1998 CIA World Factbook");
            book5.Authors.Add(new Gutenberg.Model.Author(257, "United States.  Central Intelligence Agency."));


            expectedBooks.AddRange(new List<Gutenberg.Model.Book>() {  book2, book3, book4, book5 });

            //Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            //Test Mysql through facade
            var books = facade.GetBooksMentionedInAreaMongoDB(15, 43);

            books = books.OrderByDescending(x => x.Title).ToList();
            expectedBooks = expectedBooks.OrderByDescending(x => x.Title).ToList();

            Assert.AreEqual(expectedBooks.Count, books.Count);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                // Assert.AreEqual(expectedBooks[i].Id, books[i].Id);
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
            var expectedCities = new List<Gutenberg.Model.City>();
            var city1 = new Gutenberg.Model.City(5861897, "Fairbanks", 64.8377800, -147.7163900);
            var city2 = new Gutenberg.Model.City(5780993, "Salt Lake City", 40.7607800, -111.8910500);

            Mock<IDependance> mock = new Mock<IDependance>();
            mock.Setup(m => m.imageBytes()).Returns(new byte[123456789]);

            // Test
            Gutenberg.Common.ConnectionFacade facade = new Gutenberg.Common.ConnectionFacade();
            var imageByteArray = facade.GetStaticMap(expectedCities);
            Assert.AreEqual(typeof(byte[]), imageByteArray.GetType());
            if (imageByteArray == null || imageByteArray.Length <= 1)
            {
                Assert.Fail();
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
        public virtual byte[] GetStaticMap(IDependance objectThatITalkTo)
        {
            return objectThatITalkTo.imageBytes();
        }
    }

    public interface IDependance
    {
        List<Book> Books();
        List<City> Cities();
        Byte[] imageBytes();
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
