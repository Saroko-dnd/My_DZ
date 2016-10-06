<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListOfProductsForSelectedCategory.aspx.cs" Inherits="ListOfProductsForSelectedCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="DataGridForListOfProducts" runat="server" AllowPaging="true" PageSize="8" AutoGenerateColumns="false"   OnPageIndexChanging="DataGridForListOfProducts_PageIndexChanging"
            AllowSorting="true" OnSorting="DataGridForListOfProducts_SortingByPrice">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name"/>
                <asp:BoundField DataField="Description" HeaderText="Description"/>
                <asp:BoundField DataField="Price" HeaderText="Price"  sortexpression="Price"/>
                <asp:ImageField DataImageUrlField="ImageURL" HeaderText="Photo" ControlStyle-Height="100px" ControlStyle-Width="100px"></asp:ImageField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
