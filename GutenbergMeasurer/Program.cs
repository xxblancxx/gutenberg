using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Gutenberg.Common;
using Gutenberg.Model;

namespace GutenbergMeasurer
{
    internal class Program
    {
        private static ConnectionFacade connection = new ConnectionFacade();
        private static List<double> mysqlTimes = new List<double>();
        private static List<double> mongoDBTimes = new List<double>();
        static Stopwatch watch = Stopwatch.StartNew();


        private static void Main(string[] args)
        {
            //Measuring on GetBooksWithCityMysql
            Console.WriteLine("GetBooksWithCityMysql started");
            watch.Start();

            MeasureGetBooksWithCityMysql("Zanzibar");
            MeasureGetBooksWithCityMysql("London");
            MeasureGetBooksWithCityMysql("New York City");
            MeasureGetBooksWithCityMysql("Berlin");
            MeasureGetBooksWithCityMysql("Mosul");

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mysqlTimes);
            Median(mysqlTimes);
            Average(mysqlTimes);

            //Measuring on GetBooksWithCityMongoDB
            Console.WriteLine("\nGetBooksWithCityMongoDB started");
            watch.Start();

            MeasureGetBooksWithCityMongoDB("Zanzibar");
            MeasureGetBooksWithCityMongoDB("London");
            MeasureGetBooksWithCityMongoDB("New York City");
            MeasureGetBooksWithCityMongoDB("Berlin");
            MeasureGetBooksWithCityMongoDB("Mosul");

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mongoDBTimes);
            Median(mongoDBTimes);
            Average(mongoDBTimes);

            mysqlTimes = new List<double>();
            mongoDBTimes = new List<double>();

            //Measuring on GetCitiesInTitleMysql
            Console.WriteLine("\nGetCitiesInTitleMysql started");
            watch.Start();

            MeasureGetCitiesInTitleMysql("Jingle Bells");
            MeasureGetCitiesInTitleMysql("The Warriors");
            MeasureGetCitiesInTitleMysql("The Green Flag");
            MeasureGetCitiesInTitleMysql("At Last");
            MeasureGetCitiesInTitleMysql("Olivia in India");

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mysqlTimes);
            Median(mysqlTimes);
            Average(mysqlTimes);

            //Measuring on GetCitiesInTitleMongoDB
            Console.WriteLine("\nGetCitiesInTitleMongoDB started");
            watch.Start();

            MeasureGetCitiesInTitleMongoDB("Jingle Bells");
            MeasureGetCitiesInTitleMongoDB("The Warriors");
            MeasureGetCitiesInTitleMongoDB("The Green Flag");
            MeasureGetCitiesInTitleMongoDB("At Last");
            MeasureGetCitiesInTitleMongoDB("Olivia in India");

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mongoDBTimes);
            Median(mongoDBTimes);
            Average(mongoDBTimes);

            mysqlTimes = new List<double>();
            mongoDBTimes = new List<double>();

            //Measuring on GetCitiesWithAuthorMysql
            Console.WriteLine("\nGetCitiesWithAuthorMysql started");
            watch.Start();

            MeasureGetCitiesWithAuthorMysql("Helen Bannerman");
            MeasureGetCitiesWithAuthorMysql("Jonathan Swift");
            MeasureGetCitiesWithAuthorMysql("Ida Pfeiffer");
            MeasureGetCitiesWithAuthorMysql("Bill Nye");
            MeasureGetCitiesWithAuthorMysql("John Buffa");

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mysqlTimes);
            Median(mysqlTimes);
            Average(mysqlTimes);

            //Measuring on GetCitiesWithAuthorMongoDB
            Console.WriteLine("\nGetCitiesWithAuthorMongoDB started");
            watch.Start();

            MeasureGetCitiesWithAuthorMongoDB("Helen Bannerman");
            MeasureGetCitiesWithAuthorMongoDB("Jonathan Swift");
            MeasureGetCitiesWithAuthorMongoDB("Ida Pfeiffer");
            MeasureGetCitiesWithAuthorMongoDB("Bill Nye");
            MeasureGetCitiesWithAuthorMongoDB("John Buffa");

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mongoDBTimes);
            Median(mongoDBTimes);
            Average(mongoDBTimes);

            mysqlTimes = new List<double>();
            mongoDBTimes = new List<double>();

            //Measuring on GetBooksMentionedInAreaMysql
            Console.WriteLine("\nGetBooksMentionedInAreaMysql started");
            watch.Start();

            MeasureGetBooksMentionedInAreaMysql(36, 43);
            MeasureGetBooksMentionedInAreaMysql(23, -79);
            MeasureGetBooksMentionedInAreaMysql(37, -84);
            MeasureGetBooksMentionedInAreaMysql(40, -74);
            MeasureGetBooksMentionedInAreaMysql(34, -85);

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mysqlTimes);
            Median(mysqlTimes);
            Average(mysqlTimes);

            //Measuring on GetBooksMentionedInAreaMongoDB
            Console.WriteLine("\nGetBooksMentionedInAreaMongoDB started");
            watch.Start();

            MeasureGetBooksMentionedInAreaMongoDB(36, 43);
            MeasureGetBooksMentionedInAreaMongoDB(23, -79);
            MeasureGetBooksMentionedInAreaMongoDB(37, -84);
            MeasureGetBooksMentionedInAreaMongoDB(40, -74);
            MeasureGetBooksMentionedInAreaMongoDB(34, -85);

            watch.Stop();

            Console.WriteLine("Times:");
            ListTimes(mongoDBTimes);
            Median(mongoDBTimes);
            Average(mongoDBTimes);
        }

        public static void MeasureGetBooksWithCityMysql(string cityName)
        {
            connection.GetBooksWithCityMysql(cityName);
            mysqlTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetBooksWithCityMongoDB(string cityName)
        {
            connection.GetBooksWithCityMongoDB(cityName);
            mongoDBTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetCitiesInTitleMysql(string bookTitle)
        {
            connection.GetCitiesInTitleMysql(bookTitle);
            mysqlTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetCitiesInTitleMongoDB(string bookTitle)
        {
            connection.GetCitiesInTitleMongoDB(bookTitle);
            mongoDBTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetCitiesWithAuthorMysql(string author)
        {
            connection.GetCitiesWithAuthorMysql(author);
            mysqlTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetCitiesWithAuthorMongoDB(string author)
        {
            connection.GetCitiesWithAuthorMongoDB(author);
            mongoDBTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetBooksMentionedInAreaMysql(double latitude, double longitude)
        {
            connection.GetBooksMentionedInAreaMysql(latitude, longitude);
            mysqlTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void MeasureGetBooksMentionedInAreaMongoDB(double latitude, double longitude)
        {
            connection.GetBooksMentionedInAreaMongoDB(latitude, longitude);
            mongoDBTimes.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
        }

        public static void ListTimes(List<double> times)
        {
            foreach (double time in times)
            {
                Console.WriteLine(time);
            }
        }

        public static void Median(List<double> times)
        {
            times.Sort((a, b) => a.CompareTo(b));
            Console.WriteLine("Median: " + times[2]);
        }

        public static void Average(List<double> times)
        {
            double seconds = 0.0;
            times.ToList().ForEach(x => seconds += x);
            Console.WriteLine("Average: " + seconds / times.Count());
        }
    }
}
