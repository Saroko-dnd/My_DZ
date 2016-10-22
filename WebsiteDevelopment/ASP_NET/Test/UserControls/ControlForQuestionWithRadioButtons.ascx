<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForQuestionWithRadioButtons.ascx.cs" Inherits="ControlForQuestionWithRadioButtons" %>
<%@ Register Assembly="CustomControlForRadioButton"  Namespace="CustomControlForRadioButton"  TagPrefix="MyCustomControls" %>
<%@ Register Assembly="GroupRadioButton"  Namespace="Vladsm.Web.UI.WebControls"  TagPrefix="NotMineCustomControls" %>

<asp:Label runat="server" ID="LabelForTextOfQuestion"></asp:Label>
<asp:Repeater runat="server" ID="RepeaterForAnswers">
    <ItemTemplate>
        <div>
            <NotMineCustomControls:GroupRadioButton runat="server" ID="RadioButtonForAnswer" Text='<%# DataBinder.Eval(Container.DataItem, "Text") %>' GroupName="RadioButtonsForAnswers"/>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:HiddenField runat="server" ID="HiddenFieldForScore"/>
<asp:HiddenField ID="HiddenFieldForQuestionID" runat="server"/>
