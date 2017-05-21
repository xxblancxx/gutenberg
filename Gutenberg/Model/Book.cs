using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gutenberg.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public List<City> Cities { get; set; }
        public Book(int id, string title)
        {
            Id = id;
            Title = title;
            Authors = new List<Author>();
            Cities = new List<City>();
        }
    }
}