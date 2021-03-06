﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPhotos.aspx.cs" Inherits="ViewPhotos" %>

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
        <asp:Button runat="server" ID="Button_GoToPageForUploadingFiles" CssClass="PositionFixedInTopLeftCorner NoticeableFont" Text="Go to uploading page" OnClick="ButtonGoToPageForUploadingFiles_OnClick"/>
        <asp:Repeater runat="server" ID="RepeaterForShowingAllImages">
            <ItemTemplate>
                <div class="ObjectWithBorder HorizontalAlignmentCenter">                 
                    <asp:Label runat="server" CssClass="HorizontalAlignmentCenter NoticeableFont"> <%# DataBinder.Eval(Container.DataItem, "FileNameWithoutExtension") %> </asp:Label>
                    <img id="Image" src='.image?width=300&height=300&url=<%# DataBinder.Eval(Container.DataItem, "ImageUrl") %>' class="HorizontalAlignmentCenter" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
