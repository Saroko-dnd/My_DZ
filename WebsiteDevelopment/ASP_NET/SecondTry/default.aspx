<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="MainWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSSfolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="DivForTestOfTextInputAndOutput">
        <p>Выберите цвет текста:</p>
        <asp:DropDownList runat="server" AutoPostBack="true" ID="DropDownListOfColors" OnSelectedIndexChanged="DropDownListOfColors_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label runat="server" id="MainTestLabel"></asp:Label>
        <asp:TextBox runat="server" ID="MainInputTextBox"></asp:TextBox>
        <asp:Button runat="server" Text="Add text to label" id="AddTextButton" OnClick="AddTextButton_Click"/>
    </div>
    </form>
</body>
</html>
