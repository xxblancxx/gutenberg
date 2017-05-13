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

        public City(int id, string name, string commaseparatedAltNames)
        {
            Name = name;
            city_id = id;
            AlternativeNames = new List<string>();

            if (commaseparatedAltNames != null)
            {
                string[] split = commaseparatedAltNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var altname in split)
                {
                    AlternativeNames.Add(altname);
                }
            }
        }
    }
}
