

var SearchNewsSystem = (function () {

    var PublicMembers = {};

    var AjaxFormForSearchingNewsByType;
    var ParameterlessActionValueForAjaxForm;
    var DropDownListDomObject;
    var CurrentSiteName;
    var DataListForNewsHeaderInput;
    var TextInputForNewsHeader;

    PublicMembers.GetInfoAboutAjaxForm = function ()
    {
        AjaxFormForSearchingNewsByType = $("#AjaxFormForSearchingNewsByType");
        ParameterlessActionValueForAjaxForm = $(AjaxFormForSearchingNewsByType).attr("action");
        DropDownListDomObject = $("#DropDownListForNewsType");
        CurrentSiteName = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
        DataListForNewsHeaderInput = $("#ListOfNewsWithSimilarName");
        TextInputForNewsHeader = $("#InputForNewsHeader");
    }

    PublicMembers.SubmitAjaxFormForSearchNewsByType = function () {
        var SelectedTypeValue = $(DropDownListDomObject).val();
        $(AjaxFormForSearchingNewsByType).attr("action", ParameterlessActionValueForAjaxForm + "?SelectedNewsType=" + SelectedTypeValue).submit();
    }

    PublicMembers.SearchNewsByHeader = function () {
        var CurrentTextOfHeader = $(TextInputForNewsHeader).val();
        if (CurrentTextOfHeader != "")
        {
            $.ajax({
                type: "get",
                async: true,
                url: CurrentSiteName + "/NewsOdata/GettingNewsUsingHeaderValue?NewsHeaderForSearch=" + CurrentTextOfHeader + "&$select=Header&$top=5",
                success: function (data) {
                    $(DataListForNewsHeaderInput).empty();
                    for (var CounterOfValues = 0; CounterOfValues < data.value.length; ++CounterOfValues) {
                        $(DataListForNewsHeaderInput).append('<option value="' + data.value[CounterOfValues].Header + '"></option>');
                    }

                    //alert(data.value[0].Header + data.value.length);
                },
                error: function (xhr, textStatus, errorMessage) {
                    alert(errorMessage);
                }
            });
        }
    }

    return PublicMembers;

}());

$(document).ready(function () {
    SearchNewsSystem.GetInfoAboutAjaxForm();
    SearchNewsSystem.SearchNewsByHeader();
});