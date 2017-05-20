using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class City
    {
        public int city_id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<string> AlternativeNames { get; set; }


        public City()
        {

        }

        public City(int id, string name, double latitude, double longitude)
        {
            Name = name;
            city_id = id;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
