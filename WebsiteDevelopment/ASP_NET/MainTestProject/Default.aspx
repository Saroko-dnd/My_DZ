<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView ID="DetailsViewTestControl" runat="server" AutoGenerateRows="false" HeaderText="Person" AllowPaging="true">
            <Fields>            
                <asp:BoundField  DataField="FirstName" HeaderText="First name" ReadOnly="true"/>
                <asp:BoundField  DataField="SecondName" HeaderText="Second name" ReadOnly="true"/>
            </Fields>         
        </asp:DetailsView>
    </div>
    </form>
</body>
</html>
