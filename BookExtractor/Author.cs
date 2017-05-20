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
            if (name.Contains(','))
            {
                var split = name.Split(',');
                string rname = split[1] + " " + split[0];
                author_name = rname.Trim();
            }
            else
            {
                author_name = name.Trim();
            }
        }

        public override string ToString()
        {
            return author_name;
        }
    }
}
