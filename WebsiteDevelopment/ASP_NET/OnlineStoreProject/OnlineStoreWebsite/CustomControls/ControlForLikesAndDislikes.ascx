<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForLikesAndDislikes.ascx.cs" Inherits="ControlForLikesAndDislikes" %>

<div style="display:flex; align-content:center; justify-content:center; text-align:center">
    <div style="display:flex; flex-direction:column; align-content:center; justify-content:center; margin-right:5px;">
        <asp:Label ID="LabelForLikes" runat="server" ForeColor="Green" Text="0"></asp:Label>
        <asp:ImageButton ID="LikeButton" runat="server" style="margin-left:auto; margin-right:auto" Width="25px" Height="25px" ImageUrl="~/ImagesForControls/thumbs-up.png" OnClick="LikeButton_OnClick"/>
    </div>
    <div style="display:flex; flex-direction:column; align-content:center; justify-content:center; margin-left:5px;">
        <asp:Label ID="LabelForDislikes" runat="server" ForeColor="Red" Text="0"></asp:Label>
        <asp:ImageButton ID="DislikeButton" runat="server" style="margin-left:auto; margin-right:auto" Width="25px" Height="25px" ImageUrl="~/ImagesForControls/thumbs-down.png" OnClick="DislikeButton_OnClick"/>
    </div>
</div>
