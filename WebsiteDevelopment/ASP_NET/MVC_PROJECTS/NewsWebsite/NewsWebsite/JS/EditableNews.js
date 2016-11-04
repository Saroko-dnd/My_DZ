
//Makes items editable plus submit text on ENTER (url as parameter)
/*$(document).ready(function () {
    $('.editable').editable('/Admin/Admin/TestAction?');
    console.log("JS Works");
});*/

//Editable with callback. Callback function must return string. Usually the edited content(value parameter). This will be displayed on page after editing is done.
$(document).ready(function () {
    $('.editable').editable(function (value, settings) {
        console.log(this);
        console.log(value);
        console.log(settings);
        return (value);
    }, {
        type: 'textarea',
        submit: 'OK',
    });
})
