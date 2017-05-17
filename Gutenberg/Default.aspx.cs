using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gutenberg
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly string connstring = string.Format("Server=159.203.164.55; database={0}; UID=root; password=sushi4life", "gutenberg");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListBooks(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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