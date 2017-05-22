using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Gutenberg.Common;
using System.Drawing;
using Gutenberg.Model;

namespace Gutenberg
{
    public partial class Default : System.Web.UI.Page
    {
        //private readonly string connstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", "gutenberg");
        private HtmlGenericControl bookList;
        private HtmlGenericControl map;
        private ConnectionFacade connection;

        private List<Model.City> cities = new List<Model.City>();

        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new ConnectionFacade();
            //Hide the Grid for listing
            bookList = BookTable;
            bookList.Style.Add("display", "none");
            //Hide the Map
            map = MapContainer;
        }

       

        ConnectionFacade a = new ConnectionFacade();
        public void SetMapImage(List<City> cities)
        {
            if (cities.Count>0)
            {
                var bytes = connection.GetStaticMap(cities);
                img.Src = "data:image/jpg;base64," + Convert.ToBase64String(bytes);
                img.Style.Add("margin-top", "-290px");
                img.Style.Add("pointer-events", "none");
            }
            else
            {
                img.Src = null;
            }
        }

        protected void ListBooks(object sender, EventArgs e)
        {
            //using (var connection = new MySqlConnection(connstring))
            //{
            //    //connection.Open();
            //    //string query = "truncate gutenberg.author; truncate gutenberg.book; truncate gutenberg.book_author;";
            //    //var cmd = new MySqlCommand(query, connection);
            //    //cmd.ExecuteNonQuery();
            //}
        }

        protected void CitiesInBookMysql(object sender, EventArgs e)
        {
            var cities = connection.GetCitiesInTitleMysql(mentionedInBookTextbox.Text);
            SetMapImage(cities);
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