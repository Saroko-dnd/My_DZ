<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivisionOnline.aspx.cs" Inherits="DivisionOnline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="FlexAlignmentFlexStart">
<head runat="server">
    <title></title>
    <link href="CSSFolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="FlexColumn">
        <div class="FlexAlignmentFlexStart">
            <asp:TextBox runat="server" ID="TextBoxForFirstNumber" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ErrorMessage="Field is not filled!" ControlToValidate="TextBoxForFirstNumber"></asp:RequiredFieldValidator>
        </div>
        <div class="FlexAlignmentFlexStart">
            <asp:TextBox runat="server" ID="TextBoxForSecondNumber" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ErrorMessage="Field is not filled!" ControlToValidate="TextBoxForSecondNumber"></asp:RequiredFieldValidator>
        </div>
        <div class="FlexRow FlexAlignmentFlexStart">
            <asp:Button runat="server" Text="divide" ID="ButtonForDivision" OnClick="ButtonForDivision_OnClick"/>
            <asp:Label runat="server" Text="Test text" ID="LabelForResultOfDivision"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
