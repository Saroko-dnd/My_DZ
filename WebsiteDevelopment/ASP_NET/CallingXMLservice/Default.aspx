<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/MainStyleSheet.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="CurrentScriptManager" OnAsyncPostBackError="HandlerForAsyncPostBackErrors"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanelForCurrencyInfo">            
            <ContentTemplate>
                <asp:GridView runat="server" ID="GridViewForCurrency" AutoGenerateColumns="true" CssClass="MarginCenter">

                </asp:GridView>               
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RefreshCurrencyTimer" EventName="Tick"/>
            </Triggers>
        </asp:UpdatePanel>
        <asp:Label runat="server" ID="LabelToShowOnExceptionDuringLoadData" CssClass="ErrorMessage MarginCenter" Text=""></asp:Label>
        <asp:Panel runat="server" ID="UpdateProgressForRefreshingCurrencyData">
            <ProgressTemplate>
                <div class="FlexRowCenter">
                    <asp:Image runat="server" ImageUrl="~/images/spin.gif" />
                    <asp:Label runat="server" CssClass="FlexItemWithText" Text="<%$ Resources:Texts, Label_RefreshingCurrencyData %>"></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:Panel>
        <asp:Timer runat="server" ID="RefreshCurrencyTimer" Interval="15000" OnTick="RefreshCurrencyDataOnTimerThick"></asp:Timer>
    </div>
    </form>
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        var LabelForExcetionMessages;
        var UpdateProgressForRefreshingCurrencyData;

        function InitializeGlobalVariables()
        {
            LabelForExcetionMessages = document.getElementById('<%= LabelToShowOnExceptionDuringLoadData.ClientID %>');
            UpdateProgressForRefreshingCurrencyData = document.getElementById('<%= UpdateProgressForRefreshingCurrencyData.ClientID %>');
            if (LabelForExcetionMessages.innerText == '')
            {
                LabelForExcetionMessages.style.display = "none";
            }
            else
            {
                LabelForExcetionMessages.style.display = "block";
            }
            UpdateProgressForRefreshingCurrencyData.style.display = "none";
        }

        window.onload = InitializeGlobalVariables;

        function BeginRequestHandler(sender, args) {
            LabelForExcetionMessages.style.display = "none";
            UpdateProgressForRefreshingCurrencyData.style.display = "block";
        }

        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                LabelForExcetionMessages.innerText = args.get_error().message.replace(/^.*?:/g, "");
                LabelForExcetionMessages.style.display = "block";
                args.set_errorHandled(true);
            }
            UpdateProgressForRefreshingCurrencyData.style.display = "none";
        }
    </script>
</body>
</html>
