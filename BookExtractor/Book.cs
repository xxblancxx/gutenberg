using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class Book
    {
        public string book_title { get; set; }
        // public int book_id { get; set; }
        public Book(string title)
        {
            book_title = title;
        }

    }
}
