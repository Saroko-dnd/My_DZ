<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

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
        <asp:FileUpload ID="FileUploadControl_ForImages" runat="server" CssClass="HorizontalAlignmentCenter" AllowMultiple="true" accept="image/png, image/jpeg, image/gif"/>
        <asp:Button runat="server" ID="Button_SendImagesOnServer" CssClass="HorizontalAlignmentCenter" Text="Upload images to server" OnClick="ButtonSendImagesOnServer_OnClick"/>
        <asp:Button runat="server" ID="Button_ViewAllImagesOnServer" CssClass="HorizontalAlignmentCenter" OnClick="ButtonViewAllImagesOnServer_OnClick" Text="View all images on server"/>
        <asp:RegularExpressionValidator 
            id="RegularExpressionValidatorForUploadingImages" runat="server" 
            ErrorMessage="<%$ Resources:Texts, ValidationErrorMessageWrongTypeOfFile %>" 
            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.png|.gif)$" 
            ControlToValidate="FileUploadControl_ForImages"></asp:RegularExpressionValidator>
    </div>
    </form>
</body>
</html>
