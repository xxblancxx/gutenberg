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
        <a><b>Get books mentioning city:</b></a>
        <div>
            <asp:TextBox runat="server" placeholder="City name"></asp:TextBox>
            <asp:Button runat="server" Text="Send"></asp:Button>
        </div>
        <div id="BookTable">
            <asp:Table BackColor="White" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid" runat="server">
                <asp:TableHeaderRow HorizontalAlign="Left" runat="server">
                    <asp:TableHeaderCell runat="server">Title</asp:TableHeaderCell>
                    <asp:TableHeaderCell runat="server">Author</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">How to fart in secrecy</asp:TableCell>
                    <asp:TableCell runat="server">Sir Augustus the second</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        
        <div>
            <a><b>Plot cities mentioned in book:</b></a><br/>
            <asp:TextBox runat="server" placeholder="Book title"></asp:TextBox>
            <asp:Button runat="server" Text="Send"></asp:Button>
        </div>
        <div>
            <asp:Image CssClass="BookCitiesImage" runat="server"></asp:Image>
        </div>

    </div>
    </form>
</body>
</html>
