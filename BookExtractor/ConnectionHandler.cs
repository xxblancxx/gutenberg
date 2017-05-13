using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExtractor
{
    class ConnectionHandler
    {
        private readonly string connstring = string.Format("Server=192.168.0.111; database={0}; UID=root; password=sushi4life", "gutenberg");

        public List<City> GetAllCities()
        {
            List<City> cities = new List<City>();
            using (var connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT city_id, city_name FROM city";
                var cmdReader = new MySqlCommand(query, connection);
                var reader = cmdReader.ExecuteReader();

                while(reader.Read())
                {
                    int cityid = Int32.Parse(reader.GetString(0));
                    string cityName = reader.GetString(1);
                   // string cityAltNamesCSV = reader.GetString(2);
                    cities.Add(new City(cityid, cityName));
                }
            }

            return cities;
        }

    }
}
