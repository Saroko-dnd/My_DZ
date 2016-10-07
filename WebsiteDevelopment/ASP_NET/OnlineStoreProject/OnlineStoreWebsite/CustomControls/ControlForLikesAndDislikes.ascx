<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForLikesAndDislikes.ascx.cs" Inherits="ControlForLikesAndDislikes" %>

<div style="display:flex; align-content:center; justify-content:center; text-align:center">
    <div style="display:flex; flex-direction:column; align-content:center; justify-content:center">
        <asp:Label runat="server" Text="0"></asp:Label>
        <asp:ImageButton runat="server" Width="50px" Height="50px" ImageUrl="~/ImagesForControls/thumbs-up.png"/>
    </div>
    <div style="display:flex; flex-direction:column; align-content:center; justify-content:center">
        <asp:Label runat="server" Text="0"></asp:Label>
        <asp:ImageButton runat="server" Width="50px" Height="50px" ImageUrl="~/ImagesForControls/thumbs-down.png"/>
    </div>
</div>
