<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="CustomControlForDaysOfWeek"  Namespace="DaysOfWeek"  TagPrefix="MyCustomControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <MyCustomControls:DaysOfWeekCustomControl runat="server"/>
    </div>
    </form>
</body>
</html>
