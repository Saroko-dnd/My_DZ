<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlForTest.ascx.cs" Inherits="ControlForTest" %>
<%@ Register TagPrefix="UserControls" TagName="ControlForQuestionWithRadioButtons" Src="~/UserControls/ControlForQuestionWithRadioButtons.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="SimpleControlForEndOfTest" Src="~/UserControls/SimpleControlForEndOfTest.ascx" %>

<asp:Wizard runat="server" ID="CurrentWizardForTest" OnFinishButtonClick="FinishButtonClickEventHandlerForWizard" OnNextButtonClick="NextButtonClickEventHandlerForWizard"></asp:Wizard>
