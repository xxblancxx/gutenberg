using Gutenberg.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Gutenberg.Common
{
    public class ConnectionFacade
    {
        // ----------------- Production Database  Setup ------------------------------
        //private readonly static string mongodatabase = "gutenberg";
        //private readonly static string mysqldatabase = "gutenberg";

        // ---------------------- Test Database Setup -------------------------------------
        private readonly static string mongodatabase = "gutenbergtest";
        private readonly static string mysqldatabase = "gutenbergtest";


        // private readonly string mysqlconnstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", mysqldatabase);
        //default command timeout=20;
        private readonly string mysqlconnstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life;default command timeout=3600;", mysqldatabase);
        private readonly string mongoconnstring = "mongodb://root:sushi4life@159.203.164.55:27017/";

        public List<Book> GetBooksWithCityMysql(string city)
        {
            var books = new List<Book>();
            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                connection.Open();
                string query = @"SELECT DISTINCT  book.book_id, book.book_title, author.author_id, author.author_name FROM city
                                INNER JOIN book_city ON city.city_id = book_city.fk_city_id
                                INNER JOIN book ON book_city.fk_book_id = book.book_id
                                INNER JOIN book_author ON book.book_id = book_author.fk_book_id
                                INNER JOIN author ON author.author_id = book_author.fk_author_id
                                WHERE city_asciiname = ?city
                                ORDER BY book.book_title;";
                MySqlCommand comm = connection.CreateCommand();
                comm.CommandText = query;
                comm.Parameters.AddWithValue("?city", city);

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book(Int32.Parse(reader.GetString(0)), reader.GetString(1));
                    book.Authors.Add(new Author(Int32.Parse(reader.GetString(2)), reader.GetString(3)));
                    books.Add(book);
                }
            }
            // OBS: Might have several objects of same book, if multiple authors.
            // This solves that
            var collapse = books.GroupBy(c => c.Title).Where(g => g.Skip(1).Any()).SelectMany(c => c).ToList();
            List<Book> deleteObjects = new List<Book>();
            int firstOfItsNameIndex = 0;
            for (int i = 0; i < collapse.Count(); i++)
            {
                if (i != 0)
                {
                    if (collapse[(i - 1)].Title != collapse[i].Title)
                    { // its the first occurance of this title
                        firstOfItsNameIndex = i;
                    }
                    else
                    { // it's the same as previous title
                        books[firstOfItsNameIndex].Authors.Add(books[i].Authors[0]);
                        deleteObjects.Add(books[i]);
                    }
                }
            }
            foreach (var delete in deleteObjects)
            {
                books.Remove(delete);
            }

            return books;
        }

        public List<Book> GetBooksWithCityMongoDB(string cityname)
        {
            var client = new MongoClient(mongoconnstring);
            var database = client.GetDatabase(mongodatabase);

            var collection = database.GetCollection<BsonDocument>("authors");
            var filter = Builders<BsonDocument>.Filter.Eq("books.cities.name", cityname);
            var res = collection.Find(filter).ToList();


            List<Book> books = new List<Book>();
            foreach (var author in res)
            {
                foreach (var book in author["books"].AsBsonArray)
                {
                    if (book["cities"].AsBsonArray.Any(cd => cd["name"].AsString == cityname))
                    {
                        // Only add book if it isn't already in book list.
                        if (!books.Any(b => b.Title == book["title"].AsString))
                        {
                            Book newBook = new Book(0, book["title"].AsString);
                            foreach (var city in book["cities"].AsBsonArray)
                            {
                                City newCity = new City(0, city["name"].AsString, city["latitude"].AsDouble, city["longitude"].AsDouble);
                                newBook.Cities.Add(newCity);
                            }
                            books.Add(newBook);
                        }
                    }
                }
            }
            foreach (var author in res)
            {
                foreach (var book in books)
                {
                    if (author["books"].AsBsonArray.Any(b => b["title"].AsString == book.Title))
                    {
                        book.Authors.Add(new Author(0, author["name"].AsString));
                    }
                }
            }

            return books;
        }


        public List<City> GetCitiesInTitleMysql(string title)
        {
            var cities = new List<City>();
            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                connection.Open();
                string query = @"SELECT DISTINCT city.city_id, city.city_asciiname, city.city_latitude, city.city_longitude FROM book
                                INNER JOIN book_city ON book.book_id = book_city.fk_book_id
                                INNER JOIN city ON city.city_id = book_city.fk_city_id
                                WHERE book.book_title = ?title;";
                MySqlCommand comm = connection.CreateCommand();
                comm.CommandText = query;
                comm.Parameters.AddWithValue("?title", title);

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new City(Int32.Parse(reader.GetString(0)), reader.GetString(1), Double.Parse(reader.GetString(2)), Double.Parse(reader.GetString(3))));
                }
            }
            return cities;
        }

        public List<City> GetCitiesInTitleMongoDB(string title)
        {
            List<City> cities = new List<City>();
            var client = new MongoClient(mongoconnstring);
            var database = client.GetDatabase(mongodatabase);

            var collection = database.GetCollection<BsonDocument>("authors");
            var filter = Builders<BsonDocument>.Filter.Eq("books.title", title);
            var res = collection.Find(filter).ToList();

            foreach (var authorDoc in res)
            {
                foreach (var book in authorDoc["books"].AsBsonArray)
                {
                    if (book["title"].AsString == title)
                    {
                        foreach (var city in book["cities"].AsBsonArray)
                        {
                            cities.Add(new City(0, city["name"].AsString, city["latitude"].AsDouble, city["longitude"].AsDouble));
                        }
                    }
                }
            }
            return cities;
        }

        public List<City> GetCitiesWithAuthorMysql(string author)
        {
            var cities = new List<City>();
            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                connection.Open();
                string query = @"SELECT city_id, city_asciiname, city_latitude, city_longitude FROM city where city_id in
                                (
		                                SELECT min(city_id) FROM city 
		                                INNER JOIN book_city ON city.city_id = fk_city_id
		                                INNER JOIN book ON book_city.fk_book_id = book_id
		                                INNER JOIN book_author ON book.book_id = book_author.fk_book_id
		                                INNER JOIN author ON book_author.fk_author_id = author_id
		                                WHERE author_name = ?author GROUP BY city_asciiname
                                );
                                ";
                MySqlCommand comm = connection.CreateCommand();
                comm.CommandText = query;
                comm.Parameters.AddWithValue("?author", author);

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new City(Int32.Parse(reader.GetString(0)), reader.GetString(1), Double.Parse(reader.GetString(2)), Double.Parse(reader.GetString(3))));
                }
            }
            return cities;
        }


        public List<City> GetCitiesWithAuthorMongoDB(string author)
        {
            List<City> cities = new List<City>();
            var client = new MongoClient(mongoconnstring);
            var database = client.GetDatabase(mongodatabase);

            var collection = database.GetCollection<BsonDocument>("authors");
            var filter = Builders<BsonDocument>.Filter.Eq("name", author);
            var res = collection.Find(filter).ToList();

            foreach (var authordoc in res)
            {
                foreach (var book in authordoc["books"].AsBsonArray)
                {
                    foreach (var city in book["cities"].AsBsonArray)
                    {
                        // If no entries in cities with same name and lat/long
                        if (!(cities.Any(c => c.Name == city["name"].AsString && c.Longitude == city["longitude"].AsDouble && c.Latitude == city["latitude"].AsDouble)))
                        {
                            cities.Add(new City(0, city["name"].AsString, city["latitude"].AsDouble, city["longitude"].AsDouble));
                        }
                    }
                }
            }

            return cities;
        }

        public List<Book> GetBooksMentionedInAreaMysql(double latitude, double longitude)
        {
            var books = new List<Book>();
            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                double lat1, lat2, long1, long2;

                lat1 = latitude + 0.5;
                lat2 = latitude - 0.5;

                long1 = longitude + 0.5;
                long2 = longitude - 0.5;

                connection.Open();
                string query = @"Select DISTINCT book.book_id, book.book_title, author.author_id, author.author_name from book
                INNER JOIN book_city ON book.book_id = book_city.fk_book_id
                INNER JOIN city ON book_city.fk_city_id = city.city_id
                INNER JOIN book_author ON book.book_id = book_author.fk_book_id
                INNER JOIN author ON author.author_id = book_author.fk_author_id
                WHERE city_latitude < ?lat1 AND city_latitude > ?lat2 AND city_longitude < ?long1 AND city_longitude > ?long2";
                MySqlCommand comm = connection.CreateCommand();
                comm.CommandText = query;
                comm.Parameters.AddWithValue("?lat1", lat1);
                comm.Parameters.AddWithValue("?lat2", lat2);
                comm.Parameters.AddWithValue("?long1", long1);
                comm.Parameters.AddWithValue("?long2", long2);

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book(Int32.Parse(reader.GetString(0)), reader.GetString(1));
                    book.Authors.Add(new Author(Int32.Parse(reader.GetString(2)), reader.GetString(3)));
                    books.Add(book);
                }
            }
            // OBS: Might have several objects of same book, if multiple authors.
            // This solves that
            var collapse = books.GroupBy(c => c.Title).Where(g => g.Skip(1).Any()).SelectMany(c => c).ToList();
            List<Book> deleteObjects = new List<Book>();
            int firstOfItsNameIndex = 0;
            for (int i = 0; i < collapse.Count(); i++)
            {
                if (i != 0)
                {
                    if (collapse[(i - 1)].Title != collapse[i].Title)
                    { // its the first occurance of this title
                        firstOfItsNameIndex = i;
                    }
                    else
                    { // it's the same as previous title
                        books[firstOfItsNameIndex].Authors.Add(books[i].Authors[0]);
                        deleteObjects.Add(books[i]);
                    }
                }
            }
            foreach (var delete in deleteObjects)
            {
                books.Remove(delete);
            }
            return books;
        }

        public List<Book> GetBooksMentionedInAreaMongoDB(double latitude, double longitude)
        {
            double lat1, lat2, long1, long2;

            lat1 = latitude + 0.5;
            lat2 = latitude - 0.5;

            long1 = longitude + 0.5;
            long2 = longitude - 0.5;

            var client = new MongoClient(mongoconnstring);
            var database = client.GetDatabase(mongodatabase);

            var collection = database.GetCollection<BsonDocument>("authors");
            // USE GEOSPATIAL TUTORIAL AS REFERENCE
            //https://docs.mongodb.com/manual/tutorial/geospatial-tutorial/

            //var filter = Builders<BsonDocument>.Filter.NearSphere("books.cities.location", longitude, latitude, 50000);
            string originalCommand = "{ \"books.cities.location\": { $nearSphere: {  type: \"Point\", coordinates: [ " + longitude + ", " + latitude + " ] }, $maxDistance:  50000 } }";
            BsonDocument query = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(originalCommand);

            var res = collection.Find(query).ToList();
            List<Book> books = new List<Book>();

            foreach (var author in res)
            {
                foreach (var book in author["books"].AsBsonArray)
                {
                    if (book["cities"].AsBsonArray.Any(c => c["longitude"].AsDouble <= (longitude+0.5) && c["longitude"].AsDouble >= (longitude - 0.5) && c["latitude"].AsDouble <= (latitude + 0.5) && c["latitude"].AsDouble >= (latitude - 0.5)))
                    {
                        if (!(books.Any(b => b.Title == book["title"].AsString)))
                        {
                            books.Add(new Book(0,book["title"].AsString));
                        }
                    }
                }
            }
            return books;
        }


        public byte[] GetStaticMap(List<City> cityList)
        {
            // Google limits to ~8000 characters in request
            int maxlength = 500; // Anything above 500 and google makes trouble
            if (cityList.Count > maxlength)
            {
                cityList = cityList.GetRange(0, maxlength);
            }

            string citieslist = "";

            foreach (var city in cityList)
            {
                //citieslist += "|"+ city.Latitude + "," + city.Longitude;
                citieslist += "|" + Math.Round(city.Latitude, 2).ToString().Replace(',', '.') + "," + Math.Round(city.Longitude, 2).ToString().Replace(',', '.');
            }

            string linkStart = "https://maps.googleapis.com/maps/api/staticmap?maptype=terrain&zoom=1&size=1280x1280&scale=2&markers=size:tiny";
            string linkEnd = "&key=AIzaSyAkjegOKY4oRKzYi7N9hI5nwrtTpz8hRRg";

            string imageLink = linkStart + citieslist + linkEnd;

            using (WebClient wc = new WebClient())
            {

                var byteArray = wc.DownloadData(imageLink);
                return byteArray;
            }
        }

    }
}