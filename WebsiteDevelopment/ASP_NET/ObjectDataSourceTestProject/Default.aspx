<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ObjectDataSource runat="server" ID="DataSourceForCityNames" TypeName="CitiesDataSource" SelectMethod="GetListOfCities"></asp:ObjectDataSource>
        <asp:ObjectDataSource runat="server" ID="DataSourceForCityNamesDataSet" TypeName="CitiesDataSource" SelectMethod="GetDataSetOfCities"></asp:ObjectDataSource>
        <asp:Label runat="server" Text="ListBox with data from ObjectDataSource control"></asp:Label>
        <asp:ListBox runat="server" DataSourceID="DataSourceForCityNames" DataTextField="Name"></asp:ListBox>
        <asp:GridView runat="server" DataSourceID="DataSourceForCityNames" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="City name" DataField="Name"/>
            </Columns>
        </asp:GridView>
        <asp:TreeView runat="server" DataSourceID="DataSourceForCityNamesDataSet">
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
