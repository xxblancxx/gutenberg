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

        protected void Page_Load(object sender, EventArgs e)
        {
            //Hide the Grid for listing
            bookList = BookTable;
            bookList.Style.Add("display", "none");
            //Hide the Map
            map = MapContainer;
            map.Style.Add("display","none");
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