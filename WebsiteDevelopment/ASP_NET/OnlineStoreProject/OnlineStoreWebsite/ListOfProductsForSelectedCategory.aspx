<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListOfProductsForSelectedCategory.aspx.cs" Inherits="ListOfProductsForSelectedCategory" %>
<%@ Register TagPrefix="CustomControls" TagName="ControlForLikesAndDislikes" Src="~/CustomControls/ControlForLikesAndDislikes.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSSfolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="DataGridForListOfProducts" runat="server" AllowPaging="true" PageSize="8" AutoGenerateColumns="false"   OnPageIndexChanging="DataGridForListOfProducts_PageIndexChanging" 
            CssClass="MarginCenter">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_NameColumn %>"/>
                <asp:BoundField DataField="Description" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_DescriptionColumn %>"/>
                <asp:BoundField DataField="Price" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_PriceColumn %>"/>
                <asp:ImageField DataImageUrlField="ImageURL" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_PhotoColumn %>" ControlStyle-Height="100px" ControlStyle-Width="100px"></asp:ImageField>
                <asp:TemplateField HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_RatingColumn %>">
                    <ItemTemplate>
                        <CustomControls:ControlForLikesAndDislikes CurrentProductID_Int='<%# Eval("ProductID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
