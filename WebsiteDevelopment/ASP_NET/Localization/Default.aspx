<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:LinkButton ID="LanguageEnglish" Text="English" runat="server"></asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="LanguageFrench" Text="Français" runat="server"></asp:LinkButton>
    <br /><br />
    <div><asp:Menu ID="Menu1" runat="server" BackColor="#B5C7DE" 
            DataSourceID="SiteMapDataSource1" DynamicHorizontalOffset="2" 
            Orientation="Horizontal" MaximumDynamicDisplayLevels="0" StaticDisplayLevels="2"
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#FFFFFF" 
            StaticSubMenuIndent="10px">
        <StaticSelectedStyle BackColor="#507CD1" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
        <DynamicMenuStyle BackColor="#B5C7DE" ForeColor="White" />
        <DynamicSelectedStyle BackColor="#507CD1" />
        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
            </asp:Menu>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
        <h1><asp:Localize ID="Header" runat="server" meta:resourcekey="HeaderResource1" 
                Text="Localization Page Header"></asp:Localize>
            
        </h1>
        <p><asp:Localize ID="Localize1" runat="server" 
                meta:resourcekey="Localize1Resource1" 
                Text="This is a demo page to show you how to do localization in ASP.NET"></asp:Localize></p>
        <asp:Button ID="Button1" runat="server" Text="Hello" 
            meta:resourcekey="Button1Resource1" onclick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" />
    </div>
    </form>
</body>
</html>
