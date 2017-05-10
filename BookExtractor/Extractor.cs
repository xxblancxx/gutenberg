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

        public List<Book> ExtractedBooks { get; set; }
        public List<Author> ExtractedAuthors { get; set; }

        private string[] allBookFiles;

        public Extractor()
        {
            allBookFiles = Directory.GetFiles(@"D:\EksamenGutenberg\subset_books", "*.txt", SearchOption.AllDirectories);
            ExtractedAuthors = new List<Author>();
            ExtractedBooks = new List<Book>();
        }

        public void CheckBooks()
        {
            for (int i = 0; i < allBookFiles.Length; i++)
            {
                ExtractBookAndAuthorFromFile(allBookFiles[i]);
            }
        }

        private void ExtractBookAndAuthorFromFile(string pathToFIle)
        {

            //try
            //{   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(pathToFIle))
            {
                // Read the stream to a string, and write the string to the console.
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // Stuff goes in here
                    if (line.Contains("Title:"))
                    {
                        string[] separator = new string[] { "Title:" };
                        string title = line.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        Book book = new Book(title);
                        ExtractedBooks.Add(book);
                        Console.WriteLine("Title is: " + book.book_title);
                    }
                    else if (line.Contains("Author:"))
                    {
                        string[] separator = new string[] { " and ", " And ", "&" };
                        string[] authorSeparator = new string[] { "Author:" };
                        if (!separator.Any(line.Contains))
                        {

                            string name = line.Split(authorSeparator, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                            Author author = new Author(name);
                            ExtractedAuthors.Add(author);
                            Console.WriteLine("Author is: " + author.author_name);
                        }
                        else
                        {
                            string[] names = line.Split(authorSeparator, StringSplitOptions.RemoveEmptyEntries)[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var author in names)
                            {
                                ExtractedAuthors.Add(new Author(author));
                                Console.WriteLine("Author is: " + author);
                            }
                        }
                    }


                }
            }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The file could not be read:");
            //    Console.WriteLine(e.Message);
            //}
        }
    }
}
