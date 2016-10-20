<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login DestinationPageUrl="~/Chat.aspx" runat="server" CssClass="MarginCenter"></asp:Login>
        <asp:CreateUserWizard runat="server" CssClass="MarginCenter" RequireEmail="false"></asp:CreateUserWizard>
    </div>
    </form>
</body>
</html>
