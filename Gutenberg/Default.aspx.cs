using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Gutenberg.Common;

namespace Gutenberg
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly string connstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", "gutenberg");
        private HtmlGenericControl bookList;
        private HtmlGenericControl map;


        //Casp values
        private string marker = "&markers=size:tiny|color:";
        private string markerSize = "|";
        private string space = "%7C";
        //private string coor = "40.702147,-74.015794";

        private List<string> cities = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //mapLink();
            //Hide the Grid for listing
            bookList = BookTable;
            bookList.Style.Add("display", "none");
            //Hide the Map
            map = MapContainer;
        }

        protected void showMap(object sender, EventArgs e)
        {
            //mapLink();
            
        }


        private string addCities()
        {   //              latitude,longitude - bredde, længde
            //cities.Add("40.7021470, -74.015794");
            //cities.Add("80.0, 180.0");
            //cities.Add("30.0, 0.0");
            //cities.Add("4.7358300, 45.2036100");
            //cities.Add("1.0833300, 42.5833300");
            //cities.Add("33.4205600, 43.3077800");
            //cities.Add("-54.8000000, -68.3000000");
            //cities.Add("78.2233400, 15.6468900");
            //cities.Add("71.6900200, 128.8646700");
            //cities.Add("34.0734000, 47.9725000");

            //LongLatit LongLatit = new LongLatit();
            //TextArea1.Value += "LongLat " + LongLatit.GetCities()+" ------- " ;

            //double maxLongitude = cities.Max(c => c.First());

            //double maxLatitude = cities.Max(c => c.Last());

            //double minLongitude = cities.Min(c => c.First());

            //double minLatitude = cities.Min(c => c.Last());

            //TextArea1.Value += "Max Values Long " + maxLongitude + ", Latit " + maxLatitude + " -- Min values Long " + minLongitude + ", Latit " + minLatitude; 

            // 0  < 20 =  zoom 5
            // 20 < 34 =  zoom 4
            // 34 < 50 =  Zoom 3
            // 50 < 180 = zoom 2
            //

            // Googles allowed list of colors
            Array colorOptions = "black,brown,green,purple,yellow,blue,orange,red".Split(',').ToArray(); ;
            Random randomGen = new Random();

            string citieslist = "";
            foreach (var item in cities)
            {
                int randomNumber = randomGen.Next(0, colorOptions.Length);
                citieslist += marker+colorOptions.GetValue(randomNumber) +  space + item;
            }
            return citieslist;

            

        }
        ConnectionFacade a = new ConnectionFacade();
        public void mapLink()
        {
            //string linkStart = "https://maps.googleapis.com/maps/api/staticmap?maptype=terrain&zoom=1&size=1280x1280&scale=2";
            //string linkEnd = "&key=AIzaSyAkjegOKY4oRKzYi7N9hI5nwrtTpz8hRRg";
            //img.ImageUrl = linkStart + addCities() + linkEnd;
            img.ImageUrl = a.GetStaticMap();
            //img.Style.Add("margin-top", "-290px");
        }

        protected void ListBooks(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection(connstring))
            {
                //connection.Open();
                //string query = "truncate gutenberg.author; truncate gutenberg.book; truncate gutenberg.book_author;";
                //var cmd = new MySqlCommand(query, connection);
                //cmd.ExecuteNonQuery();
            }
        }

        protected void CitiesInBook(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void CitiesByAuthor(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected void CitiesByGeolocation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}