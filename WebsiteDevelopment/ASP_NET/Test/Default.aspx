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
        <asp:HiddenField runat="server" ID="HiddenFieldForTestHours"/>
        <asp:HiddenField runat="server" ID="HiddenFieldForTestMinutes"/>
        <asp:HiddenField runat="server" ID="HiddenFieldForTestSeconds"/>
        <div id="DivWithTimeControls" class="FlexRow">
            <asp:Label runat="server" ID="UserRunOutOfTimeLabel" Text="Time has expired!" CssClass="BigRedFont HiddenControl"></asp:Label>
            <asp:Label runat="server" ID="LabelForHours"></asp:Label>
            <asp:Label runat="server" Text=":"></asp:Label>
            <asp:Label runat="server" ID="LabelForMinutes"></asp:Label>
            <asp:Label runat="server" Text=":"></asp:Label>
            <asp:Label runat="server" ID="LabelForSeconds"></asp:Label>
        </div>

        <asp:ScriptManager runat="server" ID="CurrentScriptManager"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button runat="server" ID="ButtonForHiddenPostBackWhenTimeRunsOut" CssClass="HiddenControl" OnClick="TimeExpiredEventHandler"/>
                <asp:Panel runat="server" ID="PanelForTestControl" ClientIDMode="Static">

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script>
            var IntervalForTickOfTimer;
            var TimeForTest = { Hours: 0, Minutes: 0, Seconds: 0 };
            var LabelForHours;
            var LabelForMinutes;
            var LabelForSeconds;
            var TimeExpired = false;

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckStateOfTest);

            function CheckStateOfTest(sender, args)
            {             
                if ($("#HiddenFieldForStateOfTestWizard").attr("value") == "Completed")
                {
                    clearInterval(IntervalForTickOfTimer);
                }
            }

            window.onload = SetTimeForTestAndStartTimer();
        
            function SetTimeForTestAndStartTimer()
            {
                TimeForTest.Hours = parseInt($('#<%= HiddenFieldForTestHours.ClientID %>').attr("value"));
                TimeForTest.Minutes = parseInt($('#<%= HiddenFieldForTestMinutes.ClientID %>').attr("value"));
                TimeForTest.Seconds = parseInt($('#<%= HiddenFieldForTestSeconds.ClientID %>').attr("value"));
                LabelForSeconds = document.getElementById('<%= LabelForSeconds.ClientID %>');
                LabelForMinutes = document.getElementById('<%= LabelForMinutes.ClientID %>');
                LabelForHours = document.getElementById('<%= LabelForHours.ClientID %>');
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
                else if (TimeForTest.Hours > 0)
                {
                    TimeForTest.Seconds = 59;
                    TimeForTest.Minutes = 59;
                    TimeForTest.Hours -= 1;
                }
                else
                {
                    clearInterval(IntervalForTickOfTimer);
                    $('#<%= UserRunOutOfTimeLabel.ClientID %>').show();
                    $("#DivWithTimeControls > :not(#<%= UserRunOutOfTimeLabel.ClientID %>)").hide();
                    TimeExpired = true;
                }
                LabelForSeconds.innerText = TimeForTest.Seconds;
                LabelForMinutes.innerText = TimeForTest.Minutes;
                LabelForHours.innerText = TimeForTest.Hours;
                if (TimeExpired)
                {
                    __doPostBack('<%= ButtonForHiddenPostBackWhenTimeRunsOut.ClientID %>', '');
                }
            }
        </script>
    </div>
    </form>
</body>
</html>
