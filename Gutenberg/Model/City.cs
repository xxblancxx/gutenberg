using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gutenberg.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public City(int id, string name, double lat, double lon)
        {
            Id = id;
            Name = name;
            Latitude = lat;
            Longitude = lon;
        }
    }
}