using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookExtractor
{
    class Extractor
    {
        //Path to folder containing books
        private readonly string filepath = Assembly.GetExecutingAssembly().Location.ToString().Replace("BookExtractor.exe", "") + "books";//@"D:\EksamenGutenberg\subset_books";

        private int _bookNo;
        public List<Book> ExtractedBooks { get; set; }
        public List<Author> ExtractedAuthors { get; set; }
        public List<City> AllCities { get; set; }

        private string[] allBookFiles;

        public Extractor()
        {
            allBookFiles = Directory.GetFiles(filepath, "*.txt", SearchOption.AllDirectories);
            ExtractedAuthors = new List<Author>();
            ExtractedBooks = new List<Book>();
            AllCities = new List<City>();
            GetAllCities();
        }

        public void CheckBooks()
        {
            _bookNo = 1;
            for (int i = 0; i < allBookFiles.Length; i++)
            {
                ExtractBookAuthorAndCitiesFromFile(allBookFiles[i]);
                _bookNo++;
            }
            for (int i = 0; i < ExtractedAuthors.Count(); i++)
            {
                ExtractedAuthors[i].author_id = (i + 1);
            }
            for (int i = 0; i < ExtractedBooks.Count(); i++)
            {
                ExtractedBooks[i].book_id = (i + 1);
            }
        }

        private void ExtractBookAuthorAndCitiesFromFile(string pathToFIle)
        {
            Console.Write("\r" + (int)(((double)_bookNo / (double)allBookFiles.Count()) * 100) + "% done    ");
            Console.Write("Book number: " + _bookNo + "                          ");

            //try
            //{   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(pathToFIle))
            {
                Book book = new Book("ERROR IN TITLE");
                string line = "";
                // Read the stream to a string, and write the string to the console.
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    // Stuff goes in here
                    if (line.Contains("Title:"))
                    {
                        string[] separator = new string[] { "Title:" };
                        string title = line.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        book = new Book(title);
                        //ExtractedBooks.Add(book);
                        //Console.Write("Title is: " + book.book_title);
                    }
                    else if (line.Contains("Author:"))
                    {


                        string[] separator = new string[] { " and ", " And ", "&" };
                        string[] authorSeparator = new string[] { "Author:" };
                        if (!separator.Any(line.Contains))
                        {

                            string name = line.Split(authorSeparator, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                            book.Authors.Add(CreateAuthor(name));

                        }
                        else
                        {
                            string[] names = line.Split(authorSeparator, StringSplitOptions.RemoveEmptyEntries)[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var name in names)
                            {
                                Author author = new Author(name.Trim());
                                book.Authors.Add(CreateAuthor(name));
                            }
                        }
                        line = sr.ReadToEnd();
                    }

                }
                //Cities
                List<string> capWords = Regex.Matches(line, "((?:[A-Z][a-z]+ ?)+)").Cast<Match>().Select(match => match.Value).Distinct().ToList();

                //Parallel.ForEach(AllCities, (city) =>
                //     {
                //         if (capWords.Any(city.Name.Contains))
                //         {
                //             book.Cities.Add(city);
                //         }
                //     });
                Parallel.ForEach(capWords, (word) =>
                     {
                         var matches = AllCities.Where(c => c.Name == word).Distinct();
                         if (matches.Count() > 0)
                         {
                             foreach (var city in matches)
                             {
                                 book.Cities.Add(city);
                             }
                         }
                     });


                if (book.book_title == "ERROR IN TITLE")
                {
                    throw new NullReferenceException("Book wasn't initialized with title");
                }
                else
                {
                    ExtractedBooks.Add(book);
                }
            }



            //}
            //catch (Exception e)
            //{
            //    Console.Write("The file could not be read:");
            //    Console.Write(e.Message);
            //}
        }

        public void InsertBooks()
        {

            var dbcontext = new ConnectionHandler();
            dbcontext.InsertBooksAndAuthors(ExtractedBooks, ExtractedAuthors);

        }
        public Author CreateAuthor(string name)
        {
            List<Author> existingAuthor = ExtractedAuthors.Where(a => a.author_name == name).ToList();
            if (existingAuthor.Count() == 0)
            { // Author isn't used 
                Author author = new Author(name);
                ExtractedAuthors.Add(author);
                return author;
                //ExtractedAuthors.Add(author);
                //  Console.Write("Author is: " + author.author_name);
            }
            else if (existingAuthor.Count() == 1)
            {
                Author author = existingAuthor[0];
                return author;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Not 0 or 1 authors matching name?");
            }
        }
        public void GetAllCities()
        {
            //Dummy Data
            //AllCities.Add(new City { city_id = 1, Name = "Copenhagen", AlternativeNames = new List<string> { "København", "KBH", "Havnen", "CPH" } });
            //AllCities.Add(new City { city_id = 2, Name = "Amsterdam", AlternativeNames = new List<string> { "Amsterdammer", "A'dammer" } });
            //AllCities.Add(new City { city_id = 3, Name = "Berlin", AlternativeNames = new List<string> { "Berliner" } });
            var dbcontext = new ConnectionHandler();
            AllCities = dbcontext.GetAllCities();
        }
    }
}
