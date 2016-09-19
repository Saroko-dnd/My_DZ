<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSSfolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListItemCollection runat="server" ID="AspListOfGuests"></asp:ListItemCollection>
        <asp:Label runat="server" id="ListOfGuests"></asp:Label>
        <br /><br />     
        <asp:TextBox runat="server" id="TextInput" /> 
        <asp:Button runat="server" id="AddTextFromInputAButton"  OnClick="AddNewGuest" text="Add text from input" />
        <asp:Button runat="server" id="ButtonShowGuests"  OnClick="ShowListOfGuests" text="Show list of guests" />
    </div>
    </form>
</body>
</html>
