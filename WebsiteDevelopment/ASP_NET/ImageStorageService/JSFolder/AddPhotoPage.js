

function ValidatePage()
{
    var FileNameIsValid = Page_ClientValidate();
    if (FileNameIsValid)
    {
        document.getElementById('Button_SendImagesOnServer').disabled = false;
    }
    else
    {
        document.getElementById('Button_SendImagesOnServer').disabled = true;
    }
}