<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSSFolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="DivWithCenterElements">
        <asp:XmlDataSource ID="XmlTestDataSource" runat="server" DataFile="XMLFolder/Cities.xml" XPath="/Cities/City"></asp:XmlDataSource>
        <asp:Label runat="server" Text="ListBox with data from XmlDataSource control"></asp:Label>
        <asp:ListBox runat="server" DataSourceID="XmlTestDataSource" DataTextField="Name"></asp:ListBox>
        <asp:Label runat="server" Text="GridView with data from XmlDataSource control"></asp:Label>
        <asp:GridView runat="server" DataSourceID="XmlTestDataSource" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField  HeaderText="City name" DataField="Name"/>
            </Columns>
        </asp:GridView>
        <asp:Label runat="server" Text="TreeView with data from XmlDataSource control"></asp:Label>
        <asp:TreeView runat="server" DataSourceID="XmlTestDataSource">
            <DataBindings>
                <asp:TreeNodeBinding TextField="Name"/>
            </DataBindings>
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
