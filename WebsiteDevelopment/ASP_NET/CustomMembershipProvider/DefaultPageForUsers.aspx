<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultPageForUsers.aspx.cs" Inherits="DefaultPageForUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="MarginCenter">
            <asp:Label runat="server" Text="<%$ Resources:Texts, Label_LoginSuccess %>" CssClass="BigGreenFont"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
