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
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductNameTextBox" ForeColor="Red" ErrorMessage="You must enter product name" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <div class="Flex FlexEnd FlexRow">
                                <asp:Label runat="server" Text="Name:"></asp:Label>
                                <asp:TextBox runat="server" ID="ProductNameTextBox"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductDescriptionTextBox" ForeColor="Red" ErrorMessage="You must enter product description" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <div class="Flex FlexEnd FlexRow">
                                <asp:Label runat="server" Text="Description:"></asp:Label>
                                <asp:TextBox runat="server" ID="ProductDescriptionTextBox"></asp:TextBox>
                            </div>
                            <asp:RangeValidator runat="server" ControlToValidate="ProductPriceTextBox" MaximumValue="2147483647" MinimumValue="1" Type="Integer" 
                                ErrorMessage="Illegal value for product price (max value 2147483647, min value 1)" Display="Dynamic" ForeColor="Red">
                            </asp:RangeValidator>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="You must enter an unsigned integer for product price" ValidationExpression="^[1-9][0-9]*$"
                                 ControlToValidate="ProductPriceTextBox" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductPriceTextBox" ForeColor="Red" ErrorMessage="You must enter product price" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <div class="Flex FlexEnd FlexRow">
                                <asp:Label runat="server" Text="Price:"></asp:Label>
                                <asp:TextBox runat="server" ID="ProductPriceTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="Flex FlexCenter FlexColumn">
                            <asp:Button runat="server" ID="AddNewProductButton" OnClick="AddNewProductButtonOnClick" Text="Add new product"/>
                        </div>
                    </div>
                    <asp:RegularExpressionValidator 
                        id="RegularExpressionValidatorForUploadingImages" runat="server"
                        ErrorMessage="You can only load images (png, gif, jpg)"  
                        ValidationExpression="(.*\.(jpg|png|gif)$)" 
                        ControlToValidate="ProductImageFileUpload" 
                        ForeColor="Red"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                     <div class="Flex FlexCenter FlexRow">
                        <asp:Label runat="server" Text="Image:"></asp:Label>                  
                        <asp:FileUpload runat="server" ID="ProductImageFileUpload" accept="image/jpeg, image/png, image/gif"/>
                     </div>
                </div>
                <CustomControls:ControlForListOfProducts runat="server" ID="CurrentListOfProducts"/>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
