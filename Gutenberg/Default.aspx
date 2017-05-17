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
            <asp:TextBox runat="server" placeholder="City name"></asp:TextBox>
            <asp:Button runat="server" OnClick="ListBooks" Text="Send"></asp:Button>
        </div>
        <div id="BookTable" runat="server">
            <asp:Table BackColor="White" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid" runat="server">
                <asp:TableHeaderRow HorizontalAlign="Left" runat="server">
                    <asp:TableHeaderCell runat="server">Title</asp:TableHeaderCell>
                    <asp:TableHeaderCell runat="server">Author</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <!-- Plot cities mentioned in book unto a map -->
        <div class="NewMethod">
            <a><b>Plot cities mentioned in book:</b></a><br/>
            <asp:TextBox runat="server" placeholder="Book title"></asp:TextBox>
            <asp:Button runat="server" OnClick="CitiesInBook" Text="Send"></asp:Button>
        </div>
        <!-- Plot cities from all books written by author -->
        <div class="NewMethod">
            <a><b>Plot cities by author:</b></a><br/>
            <asp:TextBox runat="server" placeholder="Author"></asp:TextBox>
            <asp:Button runat="server" OnClick="CitiesByAuthor" Text="Send"></asp:Button>
        </div>
        <!-- Plot cities mentioned in books in vicinity of the given geolocation  -->
        <div class="NewMethod">
            <a><b>Plot cities in neighborhood:</b></a><br/>
            <asp:TextBox CssClass="Coordinate" runat="server" placeholder="Longitude"></asp:TextBox>
            <asp:TextBox CssClass="Coordinate" runat="server" placeholder="Latitude"></asp:TextBox>
            <asp:Button runat="server" OnClick="CitiesByGeolocation" Text="Send"></asp:Button>
        </div>
        <div id="MapContainer" runat="server">
            <asp:Image CssClass="Map" runat="server"></asp:Image>
        </div>
    </div>
    </form>
</body>
</html>
