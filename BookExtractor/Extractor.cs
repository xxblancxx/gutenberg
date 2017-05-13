using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class Extractor
    {
        private int _bookNo;
        public List<Book> ExtractedBooks { get; set; }
        public List<Author> ExtractedAuthors { get; set; }
        public List<City> AllCities { get; set; }

        private string[] allBookFiles;

        public Extractor()
        {
            allBookFiles = Directory.GetFiles(@"D:\EksamenGutenberg\subset_books", "*.txt", SearchOption.AllDirectories);
            ExtractedAuthors = new List<Author>();
            ExtractedBooks = new List<Book>();
            AllCities = new List<City>();
            GetAllCities(); // Dummy - method needs to get from DB
        }

        public void CheckBooks()
        {
            _bookNo = 1;
            for (int i = 0; i < allBookFiles.Length; i++)
            {
                ExtractBookAuthorAndCitiesFromFile(allBookFiles[i]);
                _bookNo++;
            }
        }

        private void ExtractBookAuthorAndCitiesFromFile(string pathToFIle)
        {
            Console.WriteLine("Book number: " + _bookNo);
            Console.WriteLine((int)(((double)_bookNo/ (double)allBookFiles.Count())*100) + "% done");
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
                        //Console.WriteLine("Title is: " + book.book_title);
                    }
                    else if (line.Contains("Author:"))
                    {


                        string[] separator = new string[] { " and ", " And ", "&" };
                        string[] authorSeparator = new string[] { "Author:" };
                        if (!separator.Any(line.Contains))
                        {

                            string name = line.Split(authorSeparator, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                            Author author = new Author(name);
                            book.Authors.Add(author);
                            //ExtractedAuthors.Add(author);
                          //  Console.WriteLine("Author is: " + author.author_name);
                        }
                        else
                        {
                            string[] names = line.Split(authorSeparator, StringSplitOptions.RemoveEmptyEntries)[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var name in names)
                            {
                                Author author = new Author(name.Trim());
                                book.Authors.Add(author);
                                // ExtractedAuthors.Add(new Author(name));
                                //Console.WriteLine("Author is: " + name);
                            }
                        }
                        line = sr.ReadToEnd();
                    }

                }
                //Cities
                Parallel.ForEach(AllCities, (city) =>
                     {
                         if (/*city.AlternativeNames.ConvertAll(d => " " + d).Any(line.Contains) || */line.Contains(" " + city.Name + " ") || line.Contains(" " + city.Name + ",") || line.Contains(" " + city.Name + "."))
                         {
                             book.Cities.Add(city);
                             //Console.WriteLine("         Added city " + city.Name);
                         }
                     });

                //foreach (var city in AllCities)
                //{   
                //        if (/*city.AlternativeNames.ConvertAll(d => " " + d).Any(line.Contains) || */line.Contains(" " + city.Name + " ") || line.Contains(" " + city.Name + ",") || line.Contains(" " + city.Name + "."))
                //        {
                //            book.Cities.Add(city);
                //            Console.WriteLine("         Added city " + city.Name);
                //        }
                //}
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
            //    Console.WriteLine("The file could not be read:");
            //    Console.WriteLine(e.Message);
            //}
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
