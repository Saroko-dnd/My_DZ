<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="MyControls" TagName="ThreeParagraphs" Src="~/TestUserControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <MyControls:ThreeParagraphs runat="server"/>
        <asp:DetailsView ID="DetailsViewTestControl" runat="server" AutoGenerateRows="false" HeaderText="Person" AllowPaging="true">
            <Fields>            
                <asp:BoundField  DataField="FirstName" HeaderText="First name" ReadOnly="true"/>
                <asp:BoundField  DataField="SecondName" HeaderText="Second name" ReadOnly="true"/>
            </Fields>         
        </asp:DetailsView>
        <div style="border-color:black; border-width:2px; border-style:solid ">
            <p>MultiView control (example)</p>
            <asp:MultiView runat="server" ID="TestMultiView">
                <asp:View runat="server" ID="ViewForStep_1">
                    <asp:Label runat="server" Text="Step 1"></asp:Label>
                    <asp:Button runat="server" ID="Step_1_Button" Text="Next step" OnClick="MultiViewNextStepButton_OnClick"/>
                </asp:View>
                <asp:View runat="server" ID="ViewForStep_2">
                    <asp:Label runat="server" Text="Step 2"></asp:Label>
                    <asp:Button runat="server" ID="Step_2_Button" Text="Next step" OnClick="MultiViewNextStepButton_OnClick"/>
                </asp:View>
                <asp:View runat="server" ID="ViewForStep_3">
                    <asp:Label runat="server" Text="Step 3"></asp:Label>
                    <asp:Button runat="server" ID="Step_3_Button" Text="Next step" OnClick="MultiViewNextStepButton_OnClick"/>
                </asp:View>
            </asp:MultiView>
        </div>
        <div>
            <asp:GridView runat="server"  DataKeyNames=""></asp:GridView>
            <asp:ListBox runat="server" ></asp:ListBox>
        </div>

    </div>
    </form>
</body>
</html>
