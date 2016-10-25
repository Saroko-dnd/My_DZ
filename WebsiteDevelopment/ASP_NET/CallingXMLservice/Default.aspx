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
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanelForCurrencyInfo">            
            <ContentTemplate>
                <asp:GridView runat="server" ID="GridViewForCurrency" AutoGenerateColumns="true" CssClass="MarginCenter">

                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RefreshCurrencyTimer"/>
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelForCurrencyInfo">
            <ProgressTemplate>
                <div>

                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Timer runat="server" ID="RefreshCurrencyTimer" Interval="60000" OnTick="RefreshCurrencyDataOnTimerThick"></asp:Timer>
    </div>
    </form>
</body>
</html>
