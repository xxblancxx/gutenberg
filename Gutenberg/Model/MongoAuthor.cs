using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gutenberg.Model
{
    public class MongoAuthor : Author
    {
        public MongoAuthor(int id, string name): base(id, name)
        {
            Books = new List<Book>();
        }

        public List<Book> Books { get; set; }
    }
}