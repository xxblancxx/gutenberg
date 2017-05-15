using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class Author
    {
        public int author_id { get; set; }
        public string author_name { get; set; }

        public Author(string name)
        {
            author_name = name;
        }
    }
}
