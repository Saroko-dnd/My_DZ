

var SearchNewsSystem = (function () {

    var PublicMembers = {};

    var AjaxFormForSearchingNews;
    var ParameterlessActionValueForAjaxForm;
    var DropDownListDomObject;
    var CurrentSiteName;
    var DataListForNewsHeaderInput;

    PublicMembers.GetInfoAboutAjaxForm = function ()
    {
        AjaxFormForSearchingNews = $("#AjaxFormForSearchingNews");
        ParameterlessActionValueForAjaxForm = $(AjaxFormForSearchingNews).attr("action");
        DropDownListDomObject = $("#DropDownListForNewsType");
        CurrentSiteName = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
        DataListForNewsHeaderInput = $("#ListOfNewsWithSimilarName");
    }

    PublicMembers.SubmitAjaxFormForSearchNewsByType = function () {
        var SelectedTypeValue = $(DropDownListDomObject).val();
        $(AjaxFormForSearchingNews).attr("action", ParameterlessActionValueForAjaxForm + "?SelectedNewsType=" + SelectedTypeValue).submit();
    }

    PublicMembers.SearchNewsByHeader = function () {
        $.ajax({
            type: "get",
            async: false,
            url: CurrentSiteName + "/NewsOdata/GettingNewsUsingHeaderValue?NewsHeaderForSearch=orror&$select=Header",
            success: function (data) {
                $(DataListForNewsHeaderInput).empty();
                for (var CounterOfValues = 0; CounterOfValues < data.value.length; ++CounterOfValues)
                {
                    $(DataListForNewsHeaderInput).append('<option value="' + data.value[CounterOfValues].Header + '"></option>');
                }

                //alert(data.value[0].Header + data.value.length);
            },
            error: function (xhr, textStatus, errorMessage) {
                alert(errorMessage);
            }
        });
    }

    return PublicMembers;

}());

$(document).ready(function () {
    SearchNewsSystem.GetInfoAboutAjaxForm();
    SearchNewsSystem.SearchNewsByHeader();
});