<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView runat="server" ID="GridViewForCurrency" AutoGenerateColumns="true">

                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RefreshCurrencyTimer"/>
            </Triggers>
        </asp:UpdatePanel>
        <asp:Timer runat="server" ID="RefreshCurrencyTimer" Interval="5000" OnTick="RefreshCurrencyDataOnTimerThick"></asp:Timer>
    </div>
    </form>
</body>
</html>
