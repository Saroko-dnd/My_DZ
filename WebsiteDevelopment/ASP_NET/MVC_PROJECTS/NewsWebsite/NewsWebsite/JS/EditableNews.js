
//Makes items editable plus submit text on ENTER (url as parameter)
/*$(document).ready(function () {
    $('.editable').editable('/Admin/Admin/TestAction?');
    console.log("JS Works");
});*/

var AjaxFormForSendingChangesInNews;

//Editable with callback. Callback function must return string. Usually the edited content(value parameter). This will be displayed on page after editing is done.
$(document).ready(function () {

    AjaxFormForSendingChangesInNews = $("#AjaxFormForUpdatingNewsData");
    MakeNewsEditable('.EditableNewsHeader', '.EditableNewsBody');
})

var UpdatedNewsHeaderObject;
var UpdatedNewsBodyObject;

function MakeEditableUpdatedNews()
{
    MakeNewsEditable(UpdatedNewsHeaderObject, UpdatedNewsBodyObject);
}

function SetUpdatedNewsHeaderAndBodyObjects(IdOfDOMobjectWithUpdatedNews)
{
    UpdatedNewsHeaderObject = "#" + IdOfDOMobjectWithUpdatedNews + " .EditableNewsHeader";
    UpdatedNewsBodyObject = "#" + IdOfDOMobjectWithUpdatedNews + " .EditableNewsBody";
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
        /*var RequiredForm = $("#" + "NewsPartialForm_" + IdOfCurrentNews);
        var FormActionValue = $(RequiredForm).attr("action");
        $(RequiredForm).attr("action", FormActionValue + "?PropertyName=Header&NewValue=" + value).submit();*/
        /*console.log(FormActionValue + "?Header=" + value);
        console.log(this);
        console.log(value);
        console.log(settings);*/
        return (value);
    }, {
        type: 'textarea',
        submit: 'OK',
    });


    $(NewsBodyObject).editable(function (value, settings) {
        /*var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
        var RequiredForm = $("#" + "NewsPartialForm_" + IdOfCurrentNews);
        var FormActionValue = $(RequiredForm).attr("action");
        $(RequiredForm).attr("action", FormActionValue + "?PropertyName=Body&NewValue=" + value).submit();*/
        /*console.log(IdOfCurrentNews);
        console.log(this);
        console.log(value);
        console.log(settings);*/
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
