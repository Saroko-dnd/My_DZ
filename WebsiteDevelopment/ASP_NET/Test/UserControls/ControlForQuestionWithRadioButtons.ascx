<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForQuestionWithRadioButtons.ascx.cs" Inherits="ControlForQuestionWithRadioButtons" %>
<%@ Register Assembly="CustomControlForRadioButton"  Namespace="CustomControlForRadioButton"  TagPrefix="MyCustomControls" %>

<asp:Label runat="server" ID="LabelForTextOfQuestion"></asp:Label>
<asp:Repeater runat="server" ID="RepeaterForAnswers">
    <ItemTemplate>
        <div>
            <MyCustomControls:RadioButtonForRepeater ID="RadioButtonForAnswer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Text") %>'/>
            <asp:HiddenField ID="HiddenField_IsAnswerCorrect" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CorrectAnswer").ToString()%>' />
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:HiddenField runat="server" ID="HiddenFieldForScore"/>
