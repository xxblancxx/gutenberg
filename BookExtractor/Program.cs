﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new Extractor();
            ex.CheckBooks();
            ex.InsertBooks();

            Console.Read();
        }
    }
}
