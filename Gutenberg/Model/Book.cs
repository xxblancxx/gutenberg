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
        List<Author> Authors { get; set; }
        public Book(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}