
var SystemForUpdatingInfoAboutRacer = (function () {

    var PublicMembers = {};

    var UrlToOdataController;

    var InputForRacerId;
    var InputForRacerFirstName;
    var InputForRacerSecondName;
    var InputForRacerBiography;
    var InputForRacerCarName;
    var InputForRacerCarSpeed;
    var InputForRacerColor;

    var ParagraphForSuccessMessage;
    var ParagraphForErrorMessage;

    var ButtonForSavingChanges;
    var ImgWithGifForWaiting;

    PublicMembers.GetInfoAboutDomElements = function()
    {
        UrlToOdataController = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/RaceApi/Racers";
        InputForRacerId = $("[name='RacerID']");
        InputForRacerFirstName = $("[name='FirstName']");
        InputForRacerSecondName = $("[name='SecondName']");
        InputForRacerBiography = $("[name='Biography']");
        InputForRacerCarName = $("[name='CarName']");
        InputForRacerCarSpeed = $("[name='CarSpeedKph']");
        InputForRacerColor = $("[name='ColorCode']");

        ParagraphForSuccessMessage = $("#PForSaveChangesButtonSuccessMessage");
        ParagraphForErrorMessage = $("#PForSaveChangesButtonFailMessage");
        ButtonForSavingChanges = $("#SaveChangesInRacerButton");
        ImgWithGifForWaiting = $("#GifForWaiting");
        $(ParagraphForSuccessMessage).css("color", "green");
        $(ParagraphForErrorMessage).css("color", "red");
    }

    PublicMembers.PostChangedRacerToOdataController = function()
    {
        $(ParagraphForSuccessMessage).css("display", "none");
        $(ParagraphForErrorMessage).css("display", "none");
        $(ButtonForSavingChanges).prop("disabled", true);
        $(ImgWithGifForWaiting).css("display", "block");

        $.ajax({
            url: UrlToOdataController,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                RacerID: $(InputForRacerId).val(),
                FirstName: $(InputForRacerFirstName).val(),
                SecondName: $(InputForRacerSecondName).val(),
                Biography: $(InputForRacerBiography).val(),
                CarName: $(InputForRacerCarName).val(),
                CarSpeedKph: $(InputForRacerCarSpeed).val(),
                ColorCode: $(InputForRacerColor).val().replace("#", ""),
                DistanceCoveredInKm: "0"
            }),
            success: function () {
                $(ParagraphForSuccessMessage).css("display", "block");
                $(ButtonForSavingChanges).prop("disabled", false);
                $(ImgWithGifForWaiting).css("display", "none");
            },
            error: function () {
                $(ParagraphForErrorMessage).css("display", "block");
                $(ButtonForSavingChanges).prop("disabled", false);
                $(ImgWithGifForWaiting).css("display", "none");
            }
        })
    }

    return PublicMembers;
}());

$(document).ready(function () {
    SystemForUpdatingInfoAboutRacer.GetInfoAboutDomElements();
});