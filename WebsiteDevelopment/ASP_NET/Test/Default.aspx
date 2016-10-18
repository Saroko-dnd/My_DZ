<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="UserControls" TagName="ControlForTest" Src="~/UserControls/ControlForTest.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <UserControls:ControlForTest runat="server" ID="CurrentControlForTest" />
    </div>
    </form>
</body>
</html>
