using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new Extractor();
            ex.CheckBooks();
            ex.InsertBooks();
        }
    }
}
