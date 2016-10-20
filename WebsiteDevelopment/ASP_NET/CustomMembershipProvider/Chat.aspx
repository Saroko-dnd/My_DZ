<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="DefaultPageForUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="MarginCenter FlexColumnCenter">
            <div class="MarginCenter FlexColumnCenter">
                <asp:TextBox runat="server" ID="TextBoxForTypingNewMessage" Width="500px" Height="200px" TextMode="MultiLine"></asp:TextBox>
                <asp:Button runat="server" ID="AddNewMessageToChatButton" CssClass="BigGreenFont" Text="<%$ Resources:Texts, ButtonSendMessage %>" OnClick="AddNewMessageToChatButtonClick"/>
            </div>
            <asp:UpdatePanel runat="server">          
                <ContentTemplate>
                    <div class="FlexColumnCenter">
                        <asp:Repeater runat="server" ID="RepeaterForChatMessages">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CurrentUserName")  %>'></asp:Label>
                                <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CurrentMessage")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:Repeater>       
                    </div>
                    <asp:Timer runat="server" ID="TimerForGettingNewMessages" Interval="5000" OnTick="GetLastChatMessages"></asp:Timer>  
                </ContentTemplate>
            </asp:UpdatePanel>             
        </div>
    </div>
    </form>
</body>
</html>
