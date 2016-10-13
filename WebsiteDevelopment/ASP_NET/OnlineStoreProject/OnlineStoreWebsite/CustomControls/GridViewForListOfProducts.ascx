<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewForListOfProducts.ascx.cs" Inherits="GridViewForListOfProducts" %>
<%@ Register TagPrefix="CustomControls" TagName="ControlForLikesAndDislikes" Src="~/CustomControls/ControlForLikesAndDislikes.ascx" %>

        <asp:GridView ID="DataGridForListOfProducts" runat="server" AllowPaging="true" PageSize="8" AutoGenerateColumns="false"   OnPageIndexChanging="DataGridForListOfProducts_PageIndexChanging" 
            CssClass="MarginCenter">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_NameColumn %>"/>
                <asp:BoundField DataField="Description" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_DescriptionColumn %>"/>
                <asp:BoundField DataField="Price" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_PriceColumn %>"/>
                <asp:ImageField DataImageUrlField="ImageURL" HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_PhotoColumn %>" ControlStyle-Height="100px" ControlStyle-Width="100px">
                </asp:ImageField>
                <asp:TemplateField HeaderText="<%$ Resources:Texts, Header_DataGridForListOfProducts_RatingColumn %>">
                    <ItemTemplate>
                        <CustomControls:ControlForLikesAndDislikes CurrentProductID_Int='<%# Eval("ProductID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
