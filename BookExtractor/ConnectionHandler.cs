using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookExtractor
{
    class ConnectionHandler
    {


        // ----------------- Production Database  Setup ------------------------------
        private readonly static string productionmongodatabase = "gutenberg";
        private readonly static string productionmysqldatabase = "gutenberg";

        // ---------------------- Test Database Setup -------------------------------------
        private readonly static string testmongodatabase = "gutenbergtest";
        private readonly static string testmysqldatabase = "gutenbergtest";

        private static string mongodatabase;
        private static string mysqldatabase;
        private readonly string mysqlconnstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", mysqldatabase);
        private readonly string mongoconnstring = "mongodb://root:sushi4life@159.203.164.55:27017/";


        public ConnectionHandler(bool isTest)
        {
            if (isTest)
            {
                mongodatabase = testmongodatabase;
                mysqldatabase = testmysqldatabase;
            }
            else
            {
                mongodatabase = productionmongodatabase;
                mysqldatabase = productionmysqldatabase;
            }
        }

        public void CleanUpBeforeInsert()
        { // deletes all rows in book, author and junction table. all AutoIncrementals are reset

            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                connection.Open();
                string query = "truncate author; truncate book; truncate book_author; truncate book_city;";
                var cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public List<City> GetAllCities()
        {
            List<City> cities = new List<City>();
            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                connection.Open();
                string query = "SELECT city_id, city_asciiname, city_latitude, city_longitude FROM city";
                var cmdReader = new MySqlCommand(query, connection);
                var reader = cmdReader.ExecuteReader();

                while (reader.Read())
                {
                    int cityid = Int32.Parse(reader.GetString(0));
                    string cityName = reader.GetString(1);
                    double lat = double.Parse(reader.GetString(2));
                    double lon = double.Parse(reader.GetString(3));
                    // string cityAltNamesCSV = reader.GetString(2);
                    cities.Add(new City(cityid, cityName, lat, lon));
                }
            }

            return cities;
        }

        public bool MysqlInsertBooksAndAuthors(List<Book> books, List<Author> authors)
        {
            CleanUpBeforeInsert();
            bool succesflag = true;
            using (var connection = new MySqlConnection(mysqlconnstring))
            {
                connection.Open();

                Console.WriteLine("\n Inserting Books");
                MySqlCommand comm = connection.CreateCommand();
                string commandTxt = "";
                for (int i = 0; i < books.Count(); i++)
                {
                    commandTxt += "INSERT INTO `"+mysqldatabase+"`.`book`(`book_id`,`book_title`)VALUES(?id" + i + ",?title" + i + "); ";
                }
                comm.CommandText = commandTxt;
                for (int i = 0; i < books.Count(); i++)
                {
                    comm.Parameters.AddWithValue("?id" + i, books[i].book_id);
                    comm.Parameters.AddWithValue("?title" + i, books[i].book_title);
                }
                int affectedRows = comm.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    succesflag = false;
                }
                Console.WriteLine("\n Inserting Authors");
                comm = connection.CreateCommand();
                commandTxt = "";
                for (int i = 0; i < authors.Count(); i++)
                {
                    commandTxt += "INSERT INTO `"+mysqldatabase+"`.`author`(`author_id`,`author_name`)VALUES(?id" + i + ",?name" + i + "); ";
                }
                comm.CommandText = commandTxt;
                for (int i = 0; i < authors.Count(); i++)
                {
                    comm.Parameters.AddWithValue("?id" + i, authors[i].author_id);
                    comm.Parameters.AddWithValue("?name" + i, authors[i].author_name);
                }
                affectedRows = comm.ExecuteNonQuery();
                if (affectedRows <= 0)
                {
                    succesflag = false;
                }

                int tmpNo = 1;
                if (succesflag)
                { // if nothing has failed so far, insert relations in junction table book_author
                    Console.WriteLine("\n Inserting Junction book_author");

                    foreach (var book in books)
                    {
                        Console.Write("\r" + (int)(((double)tmpNo / (double)books.Count()) * 100) + "% done    ");
                        Console.Write("Book number: " + tmpNo + "                          ");

                        comm = connection.CreateCommand();
                        commandTxt = "";
                        if (book.Authors.Count > 0)
                        {
                            for (int i = 0; i < book.Authors.Count(); i++)
                            {
                                commandTxt += "INSERT INTO `"+mysqldatabase+"`.`book_author`(`fk_book_id`,`fk_author_id`)VALUES(?book" + i + ",?author" + i + "); ";
                            }
                            comm.CommandText = commandTxt;
                            for (int i = 0; i < book.Authors.Count(); i++)
                            {
                                comm.Parameters.AddWithValue("?book" + i, book.book_id);
                                comm.Parameters.AddWithValue("?author" + i, book.Authors[i].author_id);
                            }
                            affectedRows = comm.ExecuteNonQuery();
                            if (affectedRows <= 0)
                            {
                                succesflag = false;

                            }
                        }
                        tmpNo++;
                    }

                }
                tmpNo = 1;
                if (succesflag)
                {
                    Console.WriteLine("\n Inserting Junction book_city");

                    foreach (var book in books)
                    {
                        Console.Write("\r" + (int)(((double)tmpNo / (double)books.Count()) * 100) + "% done    ");
                        Console.Write("Book number: " + tmpNo + "                          ");
                        if (book.Cities.Count() > 0)
                        {
                            comm = connection.CreateCommand();
                            commandTxt = "";
                            //Console.Write("\r" + (int)(((double)tmpNo / (double)books.Count()) * 100) + "% done    ");
                            //Console.Write("Book number: " + tmpNo + "                          ");
                            for (int i = 0; i < book.Cities.Count(); i++)
                            {
                                commandTxt += "INSERT INTO `"+mysqldatabase+"`.`book_city`(`fk_book_id`,`fk_city_id`)VALUES(?book" + i + ",?city" + i + "); ";
                            }
                            comm.CommandText = commandTxt;
                            for (int i = 0; i < book.Cities.Count(); i++)
                            {
                                comm.Parameters.AddWithValue("?book" + i, book.book_id);
                                comm.Parameters.AddWithValue("?city" + i, book.Cities[i].city_id);
                            }
                            affectedRows = comm.ExecuteNonQuery();
                            if (affectedRows <= 0)
                            {
                                succesflag = false;
                            }
                        }
                        tmpNo++;
                    }
                }
            }
            return succesflag;
        }

        public void MongoDBInsertBooksAndAuthors(List<Book> books, List<Author> authors)
        {
            // bool successflag = true;
            var mauthors = new List<MongoAuthor>();
            var client = new MongoClient(mongoconnstring);
            var database = client.GetDatabase(mongodatabase);

            //Reset collection. Our attempt at mongo'ing a truncate
            database.DropCollection("authors");
            foreach (var author in authors)
            {
                var mauthor = new MongoAuthor(author.author_name);
                foreach (var book in books)
                {
                    if (book.Authors.Any(a => a.author_name == author.author_name))
                    {
                        mauthor.Books.Add(book);
                    }
                }
                mauthors.Add(mauthor);
            }
            SynchronizedCollection<BsonDocument> authorDocuments = new SynchronizedCollection<BsonDocument>();

            Parallel.ForEach(mauthors, (author) =>
            {
                BsonArray bookDocuments = new BsonArray(author.Books.Count());
                foreach (var book in author.Books)
                {
                    BsonArray cityDocuments = new BsonArray(book.Cities.Count());
                    foreach (var city in book.Cities)
                    {
                        var citydocument = new BsonDocument
                        {
                            { "name" , city.Name },
                            {"latitude" , city.Latitude },
                            {"longitude" , city.Longitude }
                        };
                        cityDocuments.Add(citydocument);
                    }
                    var bookdocument = new BsonDocument
                        {
                            { "title" , book.book_title },
                            {"cities" , cityDocuments }
                        };
                    bookDocuments.Add(bookdocument);
                }
                var document = new BsonDocument
                {
                    {"name",author.author_name },
                    {"books", bookDocuments }
                };
                authorDocuments.Add(document);
            });
            var collection = database.GetCollection<BsonDocument>("authors");
            collection.InsertMany(authorDocuments);
            //return successflag;
        }
    }
}
