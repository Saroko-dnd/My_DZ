<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForLikesAndDislikes.ascx.cs" Inherits="ControlForLikesAndDislikes" %>

<div style="display:flex; align-content:center; justify-content:center; text-align:center">
    <div style="display:flex; flex-direction:column; align-content:center; justify-content:center">
        <asp:Label ID="LabelForLikes" runat="server" ForeColor="Green" Text="0"></asp:Label>
        <asp:ImageButton ID="LikeButton" runat="server" Width="50px" Height="50px" ImageUrl="~/ImagesForControls/thumbs-up.png" OnClick="LikeButton_OnClick"/>
    </div>
    <div style="display:flex; flex-direction:column; align-content:center; justify-content:center">
        <asp:Label ID="LabelForDislikes" runat="server" ForeColor="Red" Text="0"></asp:Label>
        <asp:ImageButton ID="DislikeButton" runat="server" Width="50px" Height="50px" ImageUrl="~/ImagesForControls/thumbs-down.png" OnClick="DislikeButton_OnClick"/>
    </div>
</div>
