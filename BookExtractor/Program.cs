using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch timer = new Stopwatch();
            var ex = new Extractor();
            timer.Start();
            ex.CheckBooks();
            ex.InsertBooks();

            timer.Stop();
            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.WriteLine("Finished after " + timer.Elapsed.Minutes + " Min. " + timer.Elapsed.Seconds +" Sec.");
            Console.Read();
        }
    }
}
