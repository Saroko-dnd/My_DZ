
var EditableNews = (function () {

    var PublicMembers = {};

    var AjaxFormForSendingChangesInNews;
    var ParameterlessActionValueForAjaxForm;
    var UpdatedNewsHeaderObject;
    var UpdatedNewsBodyObject;
    var NameOfEditableClassForNewsHeader = "EditableNewsHeader";
    var NameOfEditableClassForNewsBody = "EditableNewsBody";

    PublicMembers.GetInfoAboutAjaxForm = function ()
    {
        AjaxFormForSendingChangesInNews = $("#AjaxFormForUpdatingNewsData");
        ParameterlessActionValueForAjaxForm = $(AjaxFormForSendingChangesInNews).attr("action");
        MakeNewsEditable('.' + NameOfEditableClassForNewsHeader, '.' + NameOfEditableClassForNewsBody);
    }

    PublicMembers.MakeEditableUpdatedNews = function () {
        MakeNewsEditable(UpdatedNewsHeaderObject, UpdatedNewsBodyObject);
        $(AjaxFormForSendingChangesInNews).attr("action", ParameterlessActionValueForAjaxForm);
    }

    PublicMembers.MakeAllNewsEditableAgain = function () {
        MakeNewsEditable('.' + NameOfEditableClassForNewsHeader, '.' + NameOfEditableClassForNewsBody);
    }

    function MakeNewsEditable(NewsHeaderObject, NewsBodyObject) {
        $(NewsHeaderObject).editable(function (value, settings) {
            var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
            var AjaxFormTargetId = $(this).parent().parent().attr("id");
            $(AjaxFormForSendingChangesInNews).attr("data-ajax-update", "#" + AjaxFormTargetId);
            var AjaxFormActionValue = $(AjaxFormForSendingChangesInNews).attr("action");
            SetUpdatedNewsHeaderAndBodyObjects(AjaxFormTargetId);
            $(AjaxFormForSendingChangesInNews).attr("action", AjaxFormActionValue + "?PropertyName=Header&NewValue=" + value + "&SelectedNewsID=" + IdOfCurrentNews).submit();
            return (value);
        }, {
            type: 'textarea',
            submit: 'OK',
        });


        $(NewsBodyObject).editable(function (value, settings) {
            var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
            var AjaxFormTargetId = $(this).parent().parent().attr("id");
            $(AjaxFormForSendingChangesInNews).attr("data-ajax-update", "#" + AjaxFormTargetId);
            var AjaxFormActionValue = $(AjaxFormForSendingChangesInNews).attr("action");
            SetUpdatedNewsHeaderAndBodyObjects(AjaxFormTargetId);
            $(AjaxFormForSendingChangesInNews).attr("action", AjaxFormActionValue + "?PropertyName=Body&NewValue=" + value + "&SelectedNewsID=" + IdOfCurrentNews).submit();
            return (value);
        }, {
            type: 'textarea',
            submit: 'OK',
        });
    }

    function SetUpdatedNewsHeaderAndBodyObjects(IdOfDOMobjectWithUpdatedNews) {
        UpdatedNewsHeaderObject = "#" + IdOfDOMobjectWithUpdatedNews + " ." + NameOfEditableClassForNewsHeader;
        UpdatedNewsBodyObject = "#" + IdOfDOMobjectWithUpdatedNews + " ." + NameOfEditableClassForNewsBody;
    }

    return PublicMembers;
}());

jQuery(document).ready(function () {
    EditableNews.GetInfoAboutAjaxForm();
})

/*var AjaxFormForSendingChangesInNews;
var ParameterlessActionValueForAjaxForm;

var NameOfEditableClassForNewsHeader = "EditableNewsHeader";
var NameOfEditableClassForNewsBody = "EditableNewsBody";

$(document).ready(function () {

    AjaxFormForSendingChangesInNews = $("#AjaxFormForUpdatingNewsData");
    ParameterlessActionValueForAjaxForm = $(AjaxFormForSendingChangesInNews).attr("action");
    MakeNewsEditable('.' + NameOfEditableClassForNewsHeader, '.' + NameOfEditableClassForNewsBody);
})

var UpdatedNewsHeaderObject;
var UpdatedNewsBodyObject;

function MakeEditableUpdatedNews()
{
    MakeNewsEditable(UpdatedNewsHeaderObject, UpdatedNewsBodyObject);
    $(AjaxFormForSendingChangesInNews).attr("action", ParameterlessActionValueForAjaxForm);
}

function SetUpdatedNewsHeaderAndBodyObjects(IdOfDOMobjectWithUpdatedNews)
{
    UpdatedNewsHeaderObject = "#" + IdOfDOMobjectWithUpdatedNews + " ." + NameOfEditableClassForNewsHeader;
    UpdatedNewsBodyObject = "#" + IdOfDOMobjectWithUpdatedNews + " ." + NameOfEditableClassForNewsBody;
}

function MakeNewsEditable(NewsHeaderObject, NewsBodyObject)
{
    $(NewsHeaderObject).editable(function (value, settings) {
        var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
        var AjaxFormTargetId = $(this).parent().parent().attr("id");
        $(AjaxFormForSendingChangesInNews).attr("data-ajax-update", "#" + AjaxFormTargetId);
        var AjaxFormActionValue = $(AjaxFormForSendingChangesInNews).attr("action");
        SetUpdatedNewsHeaderAndBodyObjects(AjaxFormTargetId);
        $(AjaxFormForSendingChangesInNews).attr("action", AjaxFormActionValue + "?PropertyName=Header&NewValue=" + value + "&SelectedNewsID=" + IdOfCurrentNews).submit();
        return (value);
    }, {
        type: 'textarea',
        submit: 'OK',
    });


    $(NewsBodyObject).editable(function (value, settings) {
        var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
        var AjaxFormTargetId = $(this).parent().parent().attr("id");
        $(AjaxFormForSendingChangesInNews).attr("data-ajax-update", "#" + AjaxFormTargetId);
        var AjaxFormActionValue = $(AjaxFormForSendingChangesInNews).attr("action");
        SetUpdatedNewsHeaderAndBodyObjects(AjaxFormTargetId);
        $(AjaxFormForSendingChangesInNews).attr("action", AjaxFormActionValue + "?PropertyName=Body&NewValue=" + value + "&SelectedNewsID=" + IdOfCurrentNews).submit();
        return (value);
    }, {
        type: 'textarea',
        submit: 'OK',
    });
}
*/