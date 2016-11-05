
//Makes items editable plus submit text on ENTER (url as parameter)
/*$(document).ready(function () {
    $('.editable').editable('/Admin/Admin/TestAction?');
    console.log("JS Works");
});*/

//Editable with callback. Callback function must return string. Usually the edited content(value parameter). This will be displayed on page after editing is done.
$(document).ready(function () {
    $('.EditableNewsHeader').editable(function (value, settings) {
        var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
        var RequiredForm = $("#" + IdOfCurrentNews);
        var FormActionValue = $(RequiredForm).attr("action");
        $(RequiredForm).attr("action", FormActionValue + "?PropertyName=Header&NewValue=" + value).submit();
        /*console.log(FormActionValue + "?Header=" + value);
        console.log(this);
        console.log(value);
        console.log(settings);*/
        return (value);
    }, {
        type: 'textarea',
        submit: 'OK',
    });


    $('.EditableNewsBody').editable(function (value, settings) {
        var IdOfCurrentNews = $(this).siblings("[name=SelectedNewsID]").val();
        var RequiredForm = $("#" + IdOfCurrentNews);
        var FormActionValue = $(RequiredForm).attr("action");
        $(RequiredForm).attr("action", FormActionValue + "?PropertyName=Body&NewValue=" + value).submit();
        /*console.log(IdOfCurrentNews);
        console.log(this);
        console.log(value);
        console.log(settings);*/
        return (value);
    }, {
        type: 'textarea',
        submit: 'OK',
    });

})
