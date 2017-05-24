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

        protected void ListBooks(List<Book> books, bool showAuthor)
        {
            bookList.Style.Add("display", "inline-block");

            headerCellAuthor.Style.Add("display", showAuthor == false ? "none" : "table-cell");

            foreach (Book t in books)
            {
                TableRow tRow = new TableRow();
                myTable.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = t.Title;
                tRow.Cells.Add(tCell);

                if (showAuthor)
                {
                    TableCell tCell2 = new TableCell();
                    foreach (var author in t.Authors)
                    {
                        tCell2.Text += author.Name + ", ";
                    }
                    tRow.Cells.Add(tCell2);
                }
            }
        }

        protected void GetBooksWithCityMysql(object sender, EventArgs e)
        {
            var books = connection.GetBooksWithCityMysql(titleAuthorWithCityTextBox.Text);
            ListBooks(books, true);
        }

        protected void GetBooksWithCityMongoDB(object sender, EventArgs e)
        {
            var books = connection.GetBooksWithCityMongoDB(titleAuthorWithCityTextBox.Text);
            ListBooks(books, true);
        }

        protected void GetCitiesInTitleMysql(object sender, EventArgs e)
        {
            var cities = connection.GetCitiesInTitleMysql(mentionedInBookTextbox.Text);
            SetMapImage(cities);
        }

        protected void GetCitiesInTitleMongoDB(object sender, EventArgs e)
        {
            var cities = connection.GetCitiesInTitleMongoDB(mentionedInBookTextbox.Text);
            SetMapImage(cities);
        }

        protected void GetCitiesWithAuthorMysql(object sender, EventArgs e)
        {
            var cities = connection.GetCitiesWithAuthorMysql(citiesWithAuthorTextBox.Text);
            SetMapImage(cities);
        }

        protected void GetCitiesWithAuthorMongoDB(object sender, EventArgs e)
        {
            var cities = connection.GetCitiesWithAuthorMongoDB(citiesWithAuthorTextBox.Text);
            SetMapImage(cities);
        }

        protected void GetBooksMentionedInAreaMysql(object sender, EventArgs e)
        {
            var books = connection.GetBooksMentionedInAreaMysql(Convert.ToDouble(mentionedInAreaLatitudeBox.Text), Convert.ToDouble(mentionedInAreaLongitudeBox.Text));
            ListBooks(books, false);
        }

        protected void GetBooksMentionedInAreaMongoDB(object sender, EventArgs e)
        {
            var books = connection.GetBooksMentionedInAreaMongoDB(Convert.ToDouble(mentionedInAreaLatitudeBox.Text), Convert.ToDouble(mentionedInAreaLongitudeBox.Text));
            ListBooks(books, false);
        }
    }
}