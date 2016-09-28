<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSSfolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="FlexColumn">
            <div class="FlexRow">
                <div class="FlexColumn">
                    <asp:Label runat="server">Products</asp:Label>
                    <asp:ListBox Width="300px" runat="server" Id="ListBoxProducts" SelectionMode="Multiple"></asp:ListBox>
                </div>
                <div class="FlexColumn">
                    <asp:Label runat="server">Basket</asp:Label>
                    <asp:ListBox Width="300px" runat="server" Id="ListBoxSelectedProducts" SelectionMode="Multiple"></asp:ListBox>
                </div>
            </div>
            <div class="FlexColumn">
                <asp:Button runat="server" Text="Add selected to basket" OnClick="AddSelectedToBasketButtonClick"/>
                <asp:Button runat="server" Text="Remove selected from basket"/>
                <asp:Button runat="server" Text="Add all to basket"/>
                <asp:Button runat="server" Text="Remove all from basket"/>
            </div>
        </div>
    </form>
</body>
</html>
