<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForQuestionWithRadioButtons.ascx.cs" Inherits="ControlForQuestionWithRadioButtons" %>

<asp:Label runat="server" ID="LabelForTextOfQuestion"></asp:Label>
<asp:Repeater runat="server" ID="RepeaterForAnswers">
    <ItemTemplate>
        <div>
            <asp:RadioButton ID="RadioButtonForAnswer" runat="server" GroupName="RadioButtonsForAnswers"/>
            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Text") %>'></asp:Label>
            <asp:HiddenField ID="HiddenField_IsAnswerCorrect" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CorrectAnswer").ToString()%>' />
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:HiddenField runat="server" ID="HiddenFieldForScore"/>
