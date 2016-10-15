<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddingOfNewProducts.aspx.cs" Inherits="AddingOfNewProducts" %>
<%@ Register TagPrefix="CustomControls" TagName="ControlForListOfProducts" Src="~/CustomControls/GridViewForListOfProducts.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <link href="CSSfolder/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="CurrentScriptManager">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="Flex FlexCenter FlexColumn">
                    <div class="Flex FlexCenter FlexRow">
                        <div class="Flex FlexCenter FlexColumn">
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductNameTextBox" ValidationGroup="ValidatorsForNewProduct" CssClass="MarginLeft" ForeColor="Red" ErrorMessage="You must enter product name!" 
                                Display="Dynamic" Width="300px">
                            </asp:RequiredFieldValidator>
                            <div class="Flex FlexEnd FlexRow">
                                <asp:Label runat="server" Text="Name:"></asp:Label>
                                <asp:TextBox runat="server" ID="ProductNameTextBox" Width="300px" ValidationGroup="ValidatorsForNewProduct"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductDescriptionTextBox" CssClass="MarginLeft" ForeColor="Red" ErrorMessage="You must enter product description!" 
                                Display="Dynamic" Width="300px" ValidationGroup="ValidatorsForNewProduct"></asp:RequiredFieldValidator>
                            <div class="Flex FlexEnd FlexRow">
                                <asp:Label style="margin-bottom:0px" runat="server" Text="Description:"></asp:Label>
                                <asp:TextBox runat="server" ID="ProductDescriptionTextBox" Width="300px" Height="150px" TextMode="MultiLine" ValidationGroup="ValidatorsForNewProduct"></asp:TextBox>
                            </div>
                            <asp:RangeValidator runat="server" ControlToValidate="ProductPriceTextBox" CssClass="MarginLeft" MaximumValue="2147483647" MinimumValue="1" Type="Integer" 
                                ErrorMessage="Illegal value for product price (max value 2147483647, min value 1)!" Display="Dynamic" ForeColor="Red" Width="300px" 
                                ValidationGroup="ValidatorsForNewProduct"></asp:RangeValidator>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="You must enter an unsigned integer for product price!" ValidationExpression="^[1-9][0-9]*$"
                                 ControlToValidate="ProductPriceTextBox" CssClass="MarginLeft" Display="Dynamic" ForeColor="Red" Width="300px" ValidationGroup="ValidatorsForNewProduct"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductPriceTextBox" CssClass="MarginLeft" ForeColor="Red" ErrorMessage="You must enter product price!" 
                                Display="Dynamic" Width="300px" ValidationGroup="ValidatorsForNewProduct"></asp:RequiredFieldValidator>
                            <div class="Flex FlexEnd FlexRow">
                                <asp:Label runat="server" Text="Price:"></asp:Label>
                                <asp:TextBox runat="server" ID="ProductPriceTextBox" Width="300px" ValidationGroup="ValidatorsForNewProduct"></asp:TextBox>
                            </div>
                        </div>
                        <div class="Flex FlexCenter FlexColumn">
                            <asp:Button runat="server" ID="AddNewProductButton" OnClick="AddNewProductButtonOnClick" Text="Add new product" ValidationGroup="ValidatorsForNewProduct"/>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator ControlToValidate="ProductImageFileUpload" ForeColor="Red" runat="server" CssClass="MarginCenter" Display="Dynamic" 
                        ErrorMessage="You must select an image for product!" ValidationGroup="ValidatorsForNewProduct"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="MarginCenter" id="RegularExpressionValidatorForUploadingImages" runat="server" ErrorMessage="You can only load images (png, gif, jpg)!" 
                        ValidationExpression="(.*\.(jpg|png|gif)$)" ControlToValidate="ProductImageFileUpload" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidatorsForNewProduct"></asp:RegularExpressionValidator>
                     <div class="Flex FlexCenter FlexRow">
                        <asp:Label runat="server" Text="Image:"></asp:Label>                  
                        <asp:FileUpload runat="server" ID="ProductImageFileUpload" accept="image/jpeg, image/png, image/gif" ValidationGroup="ValidatorsForNewProduct"/>
                     </div>
                </div>
                <div style="margin-top:10px; margin-bottom:10px">
                    <CustomControls:ControlForListOfProducts runat="server" ID="CurrentListOfProducts"/>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="AddNewProductButton"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
