

var SearchNewsSystem = (function () {

    var PublicMembers = {};

    var AjaxFormForSearchingNews;
    var ParameterlessActionValueForAjaxForm;
    var DropDownListDomObject;

    PublicMembers.GetInfoAboutAjaxForm = function ()
    {
        AjaxFormForSearchingNews = $("#AjaxFormForSearchingNews");
        ParameterlessActionValueForAjaxForm = $(AjaxFormForSearchingNews).attr("action");
        DropDownListDomObject = $("#DropDownListForNewsType");
    }

    PublicMembers.SubmitAjaxFormForSearchNewsByType = function () {
        var SelectedTypeValue = $(DropDownListDomObject).val();
        $(AjaxFormForSearchingNews).attr("action", ParameterlessActionValueForAjaxForm + "?SelectedNewsType=" + SelectedTypeValue).submit();
    }

    return PublicMembers;

}());

$(document).ready(function () {
    SearchNewsSystem.GetInfoAboutAjaxForm();
});