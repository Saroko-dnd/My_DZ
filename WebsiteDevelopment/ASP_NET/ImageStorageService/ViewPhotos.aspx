<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPhotos.aspx.cs" Inherits="ViewPhotos" %>

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
        <asp:Repeater runat="server" ID="RepeaterForShowingAllImages">
            <ItemTemplate>
                <asp:Label runat="server" CssClass="HorizontalAlignmentCenter"> <%# DataBinder.Eval(Container.DataItem, "FileNameWithoutExtension") %> </asp:Label>
                <asp:Image ID="Image" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImageUrl") %>' CssClass="HorizontalAlignmentCenter" />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
