<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoriesOfProducts.aspx.cs" Inherits="CategoriesOfProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSSfolder/CategoriesOfProductsPage.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="Label_NameOfTheOnlineStore" CssClass="HorizontalAlingmentCenter"></asp:Label>
        <asp:Repeater runat="server" ID="Repeater_CategoriesOfProducts">
            <ItemTemplate>
                <asp:Button runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName") %>' CssClass="HorizontalAlingmentCenter" OnClick="ButtonForSelectionOfProductCategory_OnClick"/>
            </ItemTemplate>
        </asp:Repeater>  
    </div>
    </form>
</body>
</html>
