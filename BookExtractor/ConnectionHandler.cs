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
                string query = "truncate gutenberg.author; truncate gutenberg.book; truncate gutenberg.book_author;";
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
            
                Console.WriteLine("Inserting Books");
                int tmpNo = 1;
                foreach (var book in books)
                {
                    Console.Write("\r" + (int)(((double)tmpNo / (double)books.Count()) * 100) + "% done    ");
                    Console.Write("Author number: " + tmpNo + "                          ");

                    MySqlCommand comm = connection.CreateCommand();
                    comm.CommandText = "INSERT INTO `gutenberg`.`book`(`book_id`,`book_title`)VALUES(?id,?title); ";
                    comm.Parameters.AddWithValue("?id", book.book_id);
                    comm.Parameters.AddWithValue("?title", book.book_title);
                    int affectedRows = comm.ExecuteNonQuery();
                    if (affectedRows <= 0)
                    {
                        succesflag = false;
                        break;
                    }
                }
                tmpNo = 1;
                Console.WriteLine("Inserting Authors");
                foreach (var author in authors)
                {
                    Console.Write("\r" + (int)(((double)tmpNo / (double)authors.Count()) * 100) + "% done    ");
                    Console.Write("Book number: " + tmpNo + "                          ");

                    MySqlCommand comm = connection.CreateCommand();
                    comm.CommandText = "INSERT INTO `gutenberg`.`author`(`author_id`,`author_name`)VALUES(?id,?name); ";
                    comm.Parameters.AddWithValue("?id", author.author_id);
                    comm.Parameters.AddWithValue("?name", author.author_name);
                    int affectedRows = comm.ExecuteNonQuery();
                    if (affectedRows <= 0)
                    {
                        succesflag = false;
                        break;
                    }
                }

                if (succesflag)
                { // if nothing has failed so far, insert relations in junction table book_author
                    Console.WriteLine("Inserting Junction book_author");
                    tmpNo = 1;
                    foreach (var book in books)
                    {
                        Console.Write("\r" + (int)(((double)tmpNo / (double)books.Count()) * 100) + "% done    ");
                        Console.Write("Book number: " + tmpNo + "                          ");

                        foreach (var author in book.Authors)
                        {
                            MySqlCommand comm = connection.CreateCommand();
                            comm.CommandText = "INSERT INTO `gutenberg`.`book_author`(`fk_book_id`,`fk_author_id`)VALUES(?book,?author);";
                            comm.Parameters.AddWithValue("?book", book.book_id);
                            comm.Parameters.AddWithValue("?author", author.author_id);
                            int affectedRows = comm.ExecuteNonQuery();
                            if (affectedRows <= 0)
                            {
                                succesflag = false;
                                break;
                            }
                        }
                    }
                }
            }
            return succesflag;
        }
    }
}
