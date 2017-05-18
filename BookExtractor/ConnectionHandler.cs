using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class ConnectionHandler
    {
        private readonly string connstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", "gutenberg");


        public void CleanUpBeforeInsert()
        { // deletes all rows in book, author and junction table. all AutoIncrementals are reset

            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "truncate gutenberg.author; truncate gutenberg.book; truncate gutenberg.book_author; truncate gutenberg.book_city;";
                var cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public List<City> GetAllCities()
        {
            List<City> cities = new List<City>();
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT city_id, city_name FROM city";
                var cmdReader = new MySqlCommand(query, connection);
                var reader = cmdReader.ExecuteReader();

                while (reader.Read())
                {
                    int cityid = Int32.Parse(reader.GetString(0));
                    string cityName = reader.GetString(1);
                    // string cityAltNamesCSV = reader.GetString(2);
                    cities.Add(new City(cityid, cityName));
                }
            }

            return cities;
        }

        public bool InsertBooksAndAuthors(List<Book> books, List<Author> authors)
        {
            CleanUpBeforeInsert();
            bool succesflag = true;
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();

                Console.WriteLine("\n Inserting Books");
                MySqlCommand comm = connection.CreateCommand();
                string commandTxt = "";
                for (int i = 0; i < books.Count(); i++)
                {
                    commandTxt += "INSERT INTO `gutenberg`.`book`(`book_id`,`book_title`)VALUES(?id" + i + ",?title" + i + "); ";
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
                    commandTxt += "INSERT INTO `gutenberg`.`author`(`author_id`,`author_name`)VALUES(?id" + i + ",?name" + i + "); ";
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
                                commandTxt += "INSERT INTO `gutenberg`.`book_author`(`fk_book_id`,`fk_author_id`)VALUES(?book" + i + ",?author" + i + "); ";
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
                                commandTxt += "INSERT INTO `gutenberg`.`book_city`(`fk_book_id`,`fk_city_id`)VALUES(?book" + i + ",?city" + i + "); ";
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
    }
}
