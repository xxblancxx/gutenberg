using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class MongoAuthor : Author
    {
        public MongoAuthor(string name) : base(name)
        {
            Books = new List<Book>();
        }

        public List<Book> Books { get; set; }
    }
}
