<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="UserControls" TagName="ControlForTest" Src="~/UserControls/ControlForTest.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="SimpleControlForEndOfTest" Src="~/UserControls/SimpleControlForEndOfTest.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel runat="server" ID="PanelForTestControl">

        </asp:Panel>
    </div>
    </form>
</body>
</html>
