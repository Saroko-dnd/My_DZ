<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListOfProductsForSelectedCategory.aspx.cs" Inherits="ListOfProductsForSelectedCategory" %>
<%@ Register TagPrefix="CustomControls" TagName="ControlForListOfProducts" Src="~/CustomControls/GridViewForListOfProducts.ascx" %>

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
        <CustomControls:ControlForListOfProducts ID="CurrentListOfProducts" runat="server"/>
    </div>
    </form>
</body>
</html>
