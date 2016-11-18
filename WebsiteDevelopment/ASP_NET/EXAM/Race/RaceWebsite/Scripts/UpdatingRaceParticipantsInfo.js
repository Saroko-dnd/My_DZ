
var UpdatingRaceParticipantsInfo = (function () {

    var PublicMembers = {};

    var CurrentRaceFinishDistance;
    var UrlToOdataController;
    var UrlForGettingUsualListOfRacers;
    var IntervalObjectForUpdatingInfoAboutRacers;
    var IntervalWasCleared;
    var StartNewRaceButton;
    var InputForNewFinishDistance;
    var ClassOnlyVisibleDuringRace = "ShowOnlyWhenRaceAlreadyStarted";
    var ClassForColorVisualizationOfDistanceCoveredByRacer = "ColorVisualizationOfDistanceCoveredByRacer";
    var DivForUpdateAfterRaceIsEnded;

    PublicMembers.GetInfoNeededForUpdatingRaceParticipantsInfo = function() {
        UrlToOdataController = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/RaceApi/Racers?$select=RacerID,DistanceCoveredInKm";
        UrlForGettingUsualListOfRacers = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/Admin/Admin/RaceEnded";
        DivForUpdateAfterRaceIsEnded = $("#DivWithRaceParticipants");
        StartNewRaceButton = $("#StartNewRaceButton");
        InputForNewFinishDistance = $("#InputForNewFinishDistance");
        if ($("#HiddenFieldNewRaceCanBeCreated").val() == "True") {
            $(StartNewRaceButton).prop("disabled", false);
            $(InputForNewFinishDistance).prop("disabled", false);
            $("." + ClassOnlyVisibleDuringRace).css("display", "none");
        }
        else
        {
            $(StartNewRaceButton).prop("disabled", true);
            $(InputForNewFinishDistance).prop("disabled", true);
            $("." + ClassOnlyVisibleDuringRace).css("display", "block");
            PublicMembers.StartUpdate();
        }
    }

    PublicMembers.StartUpdate = function () {
        $("." + ClassForColorVisualizationOfDistanceCoveredByRacer).css("border", "2px solid black");
        $("." + ClassForColorVisualizationOfDistanceCoveredByRacer + " div").css("height", "25");
        CurrentRaceFinishDistance = Number($("#InputForNewFinishDistance").val());
        IntervalWasCleared = false;
        IntervalObjectForUpdatingInfoAboutRacers = setInterval(GetLatestInfoAboutRacers, 1500);
    }

    PublicMembers.StopUpdate = function () {
        clearInterval(IntervalObjectForUpdatingInfoAboutRacers);
    }

    PublicMembers.PreparePageForUpdating = function () {
        $(StartNewRaceButton).prop("disabled", true);
        $(InputForNewFinishDistance).prop("disabled", true);
        $("." + ClassOnlyVisibleDuringRace).css("display", "block");
    }

    function LoadUsualListOrRacers()
    {
        $.ajax({
            url: UrlForGettingUsualListOfRacers,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            async: true,
            success: function (data) {
                $(DivForUpdateAfterRaceIsEnded).empty().append(data);
                $(StartNewRaceButton).prop("disabled", false);
                $(InputForNewFinishDistance).prop("disabled", false);
                $("." + ClassOnlyVisibleDuringRace).css("display", "none");
            },
            error: function (xhr, textStatus, errorMessage) {
                alert("Error occurred during updating of info about racers!");
                $(StartNewRaceButton).prop("disabled", false);
                $(InputForNewFinishDistance).prop("disabled", false);
                $("." + ClassOnlyVisibleDuringRace).css("display", "none");
            }
        })
    }

    function GetLatestInfoAboutRacers()
    {
        $.ajax({
            url: UrlToOdataController,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (data) {
                UpdateAllInfoAboutRacersOnClientSide(data);
            },
            error: function (xhr, textStatus, errorMessage) {
                alert("Error occurred during updating of info about racers!");
            }
        })
    }

    function GetWidthOfColorVisualization(CurrentDistanceCoveredByRacer)
    {
        var WidthInPercents = (Number(CurrentDistanceCoveredByRacer) / CurrentRaceFinishDistance) * 100;
        return WidthInPercents + "%";
    }

    function UpdateAllInfoAboutRacersOnClientSide(LatestInfoAboutRacers)
    {
        for (var CounterOfRacers = 0; CounterOfRacers < LatestInfoAboutRacers.value.length; ++CounterOfRacers)
        {      
            if (Number(LatestInfoAboutRacers.value[CounterOfRacers].DistanceCoveredInKm) >= CurrentRaceFinishDistance && !IntervalWasCleared)
            {
                PublicMembers.StopUpdate();
                IntervalWasCleared = true;
            }
            $("#RacerID_" + LatestInfoAboutRacers.value[CounterOfRacers].RacerID + " .SpanWithRacerDistanceCoveredValue").empty().append(LatestInfoAboutRacers.value[CounterOfRacers].DistanceCoveredInKm);
            WidthOfColorVisualization = GetWidthOfColorVisualization(LatestInfoAboutRacers.value[CounterOfRacers].DistanceCoveredInKm);
            $("#RacerID_" + LatestInfoAboutRacers.value[CounterOfRacers].RacerID + " ." + ClassForColorVisualizationOfDistanceCoveredByRacer + " div").css("width", WidthOfColorVisualization);
        }
        if (IntervalWasCleared)
        {
            setTimeout(LoadUsualListOrRacers, 3000);
        }
    }

    return PublicMembers;
}());

$(document).ready(function () {
    UpdatingRaceParticipantsInfo.GetInfoNeededForUpdatingRaceParticipantsInfo();   
})