<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="UserControls" TagName="ControlForTest" Src="~/UserControls/ControlForTest.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="SimpleControlForEndOfTest" Src="~/UserControls/SimpleControlForEndOfTest.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSSFolder/MainStyleSheet.css" rel="stylesheet"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="FlexRow">
            <asp:Label runat="server" ID="LabelForMinutes"></asp:Label>
            <asp:Label runat="server" Text=":"></asp:Label>
            <asp:Label runat="server" ID="LabelForSeconds"></asp:Label>
        </div>
        <script>
            var IntervalForTickOfTimer;
            var TimeForTest = { Minutes: 15, Seconds: 0 };
            var LabelForMinutes;
            var LabelForSeconds;

            window.onload = SetTimeForTestAndStartTimer();
        
            function SetTimeForTestAndStartTimer()
            {
                LabelForSeconds = document.getElementById('<%= LabelForSeconds.ClientID %>');
                LabelForMinutes = document.getElementById('<%= LabelForMinutes.ClientID %>');
                IntervalForTickOfTimer = setInterval(TickForTimer, 1000);
            }

            function TickForTimer()
            {
                if (TimeForTest.Seconds > 0)
                {
                    TimeForTest.Seconds -= 1;
                }
                else if (TimeForTest.Minutes > 0)
                {
                    TimeForTest.Seconds = 59;
                    TimeForTest.Minutes -= 1;
                }
                else
                {
                    clearInterval(IntervalForTickOfTimer);
                    //End of test + postback
                }
                LabelForSeconds.innerText = TimeForTest.Seconds;
                LabelForMinutes.innerText = TimeForTest.Minutes;
            }
        </script>
        <asp:ScriptManager runat="server" ID="CurrentScriptManager"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="PanelForTestControl" ClientIDMode="Static">

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
