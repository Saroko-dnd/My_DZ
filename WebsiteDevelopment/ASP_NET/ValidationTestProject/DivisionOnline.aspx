<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivisionOnline.aspx.cs" Inherits="DivisionOnline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="FlexAlignmentFlexStart">
<head runat="server">
    <title></title>
    <link href="CSSFolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="FlexColumn MarginLeftAndTop">
        <div class="FlexAlignmentFlexStart">
            <asp:TextBox runat="server" ID="TextBoxForFirstNumber" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ErrorMessage="<%$ Resources:ErrorMessagesForValidators, ErrorMessageForRequiredFieldValidator %>" 
                ControlToValidate="TextBoxForFirstNumber" Display="Dynamic" CssClass="MarginLeft"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="TextBoxForFirstNumber" ValidationExpression="^(([-+]?[1-9]\d*)|(0))$" 
                ErrorMessage="<%$ Resources:ErrorMessagesForValidators, ErrorMessageForRegularExpressionValidator %>" ForeColor="Red" Display="Dynamic" CssClass="MarginLeft"></asp:RegularExpressionValidator>
            <asp:RangeValidator runat="server" ControlToValidate="TextBoxForFirstNumber" Display="Dynamic" MinimumValue="-2147483648" MaximumValue="2147483647" Type="Integer" ForeColor="Red" 
                ErrorMessage="<%$ Resources:ErrorMessagesForValidators, ErrorMessageForRangeValidator %>" CssClass="MarginLeft"></asp:RangeValidator>
        </div>
        <div class="FlexAlignmentFlexStart">
            <asp:TextBox runat="server" ID="TextBoxForSecondNumber" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ErrorMessage="<%$ Resources:ErrorMessagesForValidators, ErrorMessageForRequiredFieldValidator %>" 
                ControlToValidate="TextBoxForSecondNumber" Display="Dynamic" CssClass="MarginLeft"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="TextBoxForSecondNumber" ValidationExpression="^[-+]?[1-9]\d*$" 
                ErrorMessage="<%$ Resources:ErrorMessagesForValidators, ErrorMessageForRegularExpressionValidator %>" ForeColor="Red" Display="Dynamic" CssClass="MarginLeft"></asp:RegularExpressionValidator>
            <asp:RangeValidator runat="server" ControlToValidate="TextBoxForSecondNumber" Display="Dynamic" MinimumValue="-2147483648" MaximumValue="2147483647" Type="Integer" ForeColor="Red" 
                ErrorMessage="<%$ Resources:ErrorMessagesForValidators, ErrorMessageForRangeValidator %>" CssClass="MarginLeft"></asp:RangeValidator>
        </div>
        <div class="FlexRow FlexAlignmentFlexStart">
            <asp:Button runat="server" Text="Divide" ID="ButtonForDivision" OnClick="ButtonForDivision_OnClick"/>
            <asp:Label runat="server" ID="LabelForResultOfDivision" CssClass="MarginLeft"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
