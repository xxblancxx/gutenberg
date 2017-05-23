<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gutenberg.Default" %>
<link href="Style/Default.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Gutenberg project</h1>
        <!-- Get book mentioning city and list title + author -->
        <a><b>Get books mentioning city:</b></a>
        <div>
            <asp:TextBox runat="server" ID="titleAuthorWithCityTextBox" placeholder="City name"></asp:TextBox>
            <asp:Button runat="server" OnClick="GetBooksWithCityMysql" Text="Mysql"></asp:Button>
            <asp:Button runat="server" OnClick="GetBooksWithCityMongoDB" Text="MongoDB"></asp:Button>
        </div>
        <div id="BookTable" runat="server">
            <asp:Table ID="myTable" BackColor="White" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid" runat="server">
                <asp:TableHeaderRow HorizontalAlign="Left" runat="server">
                    <asp:TableHeaderCell ID="headerCellTitle" runat="server">Title</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="headerCellAuthor" runat="server">Author</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <!-- Plot cities mentioned in book unto a map -->
        <div class="NewMethod">
            <a><b>Plot cities mentioned in book:</b></a><br/>
            <asp:TextBox ID="mentionedInBookTextbox" runat="server" placeholder="Book title"></asp:TextBox>
            <asp:Button runat="server" OnClick="GetCitiesInTitleMysql" Text="Mysql"></asp:Button>
            <asp:Button runat="server" OnClick="GetCitiesInTitleMongoDB" Text="MongoDB"></asp:Button>
        </div>
        <!-- Plot cities from all books written by author -->
        <div class="NewMethod">
            <a><b>Plot cities by author:</b></a><br/>
            <asp:TextBox ID="citiesWithAuthorTextBox" runat="server" placeholder="Author"></asp:TextBox>
            <asp:Button runat="server" OnClick="GetCitiesWithAuthorMysql" Text="Mysql"></asp:Button>
            <asp:Button runat="server" OnClick="GetCitiesWithAuthorMongoDB" Text="MongoDB"></asp:Button>
        </div>
        <!-- Plot cities mentioned in books in vicinity of the given geolocation  -->
        <div class="NewMethod">
            <a><b>Plot cities in neighborhood:</b></a><br/>
            <asp:TextBox CssClass="Coordinate" ID="mentionedInAreaLatitudeBox" runat="server" placeholder="Longitude"></asp:TextBox>
            <asp:TextBox CssClass="Coordinate" ID="mentionedInAreaLongitudeBox" runat="server" placeholder="Latitude"></asp:TextBox>
            <asp:Button runat="server" OnClick="GetBooksMentionedInAreaMysql" Text="Mysql"></asp:Button>
            <asp:Button runat="server" OnClick="GetBooksMentionedInAreaMongoDB" Text="MongoDB"></asp:Button>
        </div>
        <div id="MapContainer" runat="server" style=" overflow: hidden">
            <img id="img" runat="server" alt=""/>
        </div>
    </div>
    </form>
</body>
</html>
