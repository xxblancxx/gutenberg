using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

namespace BookExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            string input = "";
            while (input != "test" && input != "prod" && input != "exit")
            {
                Console.WriteLine("type 'test' for test db, 'prod' for production db or 'exit' to cancel!");
                input = Console.ReadLine();
            }
            if (input != "exit")
            {
                bool isTest = true;
                if (input == "prod")
                {
                    isTest = false;
                }
                var ex = new Extractor(isTest);
                timer.Start();
                ex.CheckBooks();
                ex.InsertBooks();

                timer.Stop();
                Console.WriteLine();
                Console.WriteLine("Done!");
                Console.WriteLine("Finished after " + timer.Elapsed);
                Console.Read();
            }


        }

    }
}
