<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="DefaultPageForUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/MainStyleSheet.css" rel="stylesheet"/>
    <link href="CSS/ChatStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="MarginCenter FlexColumnCenter">
            <div class="MarginCenter FlexColumnCenter">
                <asp:TextBox runat="server" ID="TextBoxForTypingNewMessage" Width="500px" Height="200px" TextMode="MultiLine"></asp:TextBox>
            </div>
            <asp:UpdatePanel runat="server">          
                <ContentTemplate>
                    <div class="MarginCenter">
                        <asp:Button runat="server" ID="AddNewMessageToChatButton" CssClass="BigGreenFont" Text="<%$ Resources:Texts, ButtonSendMessage %>" OnClick="AddNewMessageToChatButtonClick"/>
                    </div>
                    <div class="DivForChatMessages MarginCenter" id="DivWithChat">
                        <asp:Repeater runat="server" ID="RepeaterForChatMessages">
                            <ItemTemplate>
                                <div class="DivWithChatMessage FlexColumnCenter">
                                    <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MessageSource")  %>'></asp:Label>
                                    <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CurrentMessage").ToString().Replace("\n","<br/>")  %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>       
                    </div>
                    <asp:Timer runat="server" ID="TimerForGettingNewMessages" Interval="5000" OnTick="GetLastChatMessages"></asp:Timer>  
                </ContentTemplate>
            </asp:UpdatePanel>             
        </div>
    </div>
    </form>
    <script>
        var ScrollPositionOfChatDiv;

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(SaveStateOfChatScroll);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ResetStateOfChatScroll);

        function SaveStateOfChatScroll(sender, args)
        {
            ScrollPositionOfChatDiv = document.getElementById('DivWithChat').scrollTop;
        }

        function ResetStateOfChatScroll(sender, args) {
            document.getElementById('DivWithChat').scrollTop = ScrollPositionOfChatDiv;
        }

    </script>
</body>
</html>
