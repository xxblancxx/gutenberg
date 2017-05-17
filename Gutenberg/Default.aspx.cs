using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Gutenberg
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly string connstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", "gutenberg");
        private HtmlGenericControl bookList;
        private HtmlGenericControl map;

        //Casp values
        private string marker = "&markers=color:";
        private string label = "label:";
        private string space = "%7C";
        private string coor = "40.702147,-74.015794";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Hide the Grid for listing
            bookList = BookTable;
            bookList.Style.Add("display", "none");
            //Hide the Map
            map = MapContainer;
            map.Style.Add("display","none");
        }

        private string city1()
        {
            marker += "blue" + space;
            label += "H";

            return marker + label + space + coor;
        }

        public void mapLink()
        {
            string linkStart = "https://maps.googleapis.com/maps/api/staticmap?maptype=terrain&zoom=1&size=1280x1280&scale=2";
            string linkEnd = "&key=AIzaSyAkjegOKY4oRKzYi7N9hI5nwrtTpz8hRRg";
            img.ImageUrl = linkStart + city1() + linkEnd;
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