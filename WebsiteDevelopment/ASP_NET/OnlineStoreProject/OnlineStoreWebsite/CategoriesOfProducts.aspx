<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoriesOfProducts.aspx.cs" Inherits="CategoriesOfProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSSfolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Flex FlexColumn">
        <asp:Label runat="server" ID="Label_NameOfTheOnlineStore" CssClass="MarginCenter"></asp:Label>
        <asp:Repeater runat="server" ID="Repeater_CategoriesOfProducts">
            <ItemTemplate>
                <asp:Button runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryName") %>' OnClick="ButtonForSelectionOfProductCategory_OnClick" CssClass="MarginCenter"/>
            </ItemTemplate>
        </asp:Repeater>  
    </div>
    </form>
</body>
</html>
