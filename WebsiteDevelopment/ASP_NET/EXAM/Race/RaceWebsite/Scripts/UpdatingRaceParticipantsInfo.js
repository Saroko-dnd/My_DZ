﻿
var UpdatingRaceParticipantsInfo = (function () {

    var PublicMembers = {};

    var CurrentRaceFinishDistance;
    var UrlToOdataController;
    var UrlForReloadingUsualListOfRacers;
    var UrlForCheckingRaceExistence;
    var UrlForGettingCurrentRaceFinishDistance;

    var IntervalObjectForUpdatingInfoAboutRacers;
    var IntervalForUpdatingInfoAboutRacersWasCleared;

    var IntervalForCheckingRaceExistence;
    var IntervalForCheckingRaceExistenceWasCleared;
    var TimeForIntervalForCheckingRaceExistence = 1500;

    var StartNewRaceButton;
    var InputForNewFinishDistance;
    var ClassOnlyVisibleDuringRace = "ShowOnlyWhenRaceAlreadyStarted";
    var ClassForColorVisualizationOfDistanceCoveredByRacer = "ColorVisualizationOfDistanceCoveredByRacer";
    var DivForUpdateAfterRaceIsEnded;
    var ThisIsAdminPage;

    PublicMembers.GetInfoNeededForUpdatingRaceParticipantsInfo = function () {
        var TestOfResourcesObject = Resources;
        UrlToOdataController = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/RaceApi/Racers?$select=RacerID,DistanceCoveredInKm";
        UrlForCheckingRaceExistence = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/Home/RaceAlreadyExist";
        UrlForGettingCurrentRaceFinishDistance = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/Home/CurrentRaceFinishDistance";
        DivForUpdateAfterRaceIsEnded = $("#DivWithRaceParticipants");
        ThisIsAdminPage = $("#HiddenFieldThisIsAdminPage").val();
        if (ThisIsAdminPage == "True")
        {
            UrlForReloadingUsualListOfRacers = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/Admin/Admin/RaceEnded";
            StartNewRaceButton = $("#StartNewRaceButton");
            InputForNewFinishDistance = $("#InputForNewFinishDistance");
            CurrentRaceFinishDistance = Number($(InputForNewFinishDistance).val());
            if ($("#HiddenFieldNewRaceCanBeCreated").val() == "True") {
                EnableAbilityToCreateNewRace();
                IntervalForCheckingRaceExistenceWasCleared = false;
                IntervalForCheckingRaceExistence = setInterval(CheckRaceExistence, TimeForIntervalForCheckingRaceExistence);
            }
            else {
                DisableAbilityToCreateNewRace();
                PublicMembers.StartUpdate();
            }
        }
        else
        {
            CurrentRaceFinishDistance = Number($("#HiddenFieldCurrentFinishDistance").val());
            UrlForReloadingUsualListOfRacers = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/Home/RaceEnded";
            if ($("#HiddenFieldNewRaceCanBeCreated").val() == "False") {
                PublicMembers.StartUpdate();
            }
            else
            {
                IntervalForCheckingRaceExistenceWasCleared = false;
                IntervalForCheckingRaceExistence = setInterval(CheckRaceExistence, TimeForIntervalForCheckingRaceExistence);
            }
        }
    }

    PublicMembers.StartUpdate = function () {
        if (ThisIsAdminPage == "True")
        {
            DisableAbilityToCreateNewRace();
        }
        $("." + ClassForColorVisualizationOfDistanceCoveredByRacer).css("border", "2px solid black");
        $("." + ClassForColorVisualizationOfDistanceCoveredByRacer + " div").css("height", "25");
        IntervalForUpdatingInfoAboutRacersWasCleared = false;
        IntervalObjectForUpdatingInfoAboutRacers = setInterval(GetLatestInfoAboutRacers, 1500);
    }

    PublicMembers.StopUpdate = function () {
        clearInterval(IntervalObjectForUpdatingInfoAboutRacers);
        IntervalForCheckingRaceExistenceWasCleared = false;
        IntervalForCheckingRaceExistence = setInterval(CheckRaceExistence, TimeForIntervalForCheckingRaceExistence);
    }

    PublicMembers.PreparePageForUpdating = function () {
        DisableAbilityToCreateNewRace();
    }

    function ReloadListOfRacers(StartUpdatingProcessOnSuccess)
    {
        $.ajax({
            url: UrlForReloadingUsualListOfRacers,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            async: true,
            success: function (data) {
                $(DivForUpdateAfterRaceIsEnded).empty().append(data);
                if (ThisIsAdminPage == "True")
                {
                    EnableAbilityToCreateNewRace();
                }
                if (StartUpdatingProcessOnSuccess == true)
                {
                    GetCurrentRaceFinishDistanceAndStartUpdate();                    
                }
            },
            error: function (xhr, textStatus, errorMessage) {
                alert(Resources.ErrorMessageForReloadingRacersData);
                if (ThisIsAdminPage == "True")
                {
                    EnableAbilityToCreateNewRace();
                }
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
                alert(Resources.ErrorMessageForGettingLatestInfoAboutRacers);
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
            if (Number(LatestInfoAboutRacers.value[CounterOfRacers].DistanceCoveredInKm) >= CurrentRaceFinishDistance && !IntervalForUpdatingInfoAboutRacersWasCleared)
            {
                PublicMembers.StopUpdate();
                IntervalForUpdatingInfoAboutRacersWasCleared = true;
            }
            $("#RacerID_" + LatestInfoAboutRacers.value[CounterOfRacers].RacerID + " .SpanWithRacerDistanceCoveredValue").empty().append(LatestInfoAboutRacers.value[CounterOfRacers].DistanceCoveredInKm);
            WidthOfColorVisualization = GetWidthOfColorVisualization(LatestInfoAboutRacers.value[CounterOfRacers].DistanceCoveredInKm);
            $("#RacerID_" + LatestInfoAboutRacers.value[CounterOfRacers].RacerID + " ." + ClassForColorVisualizationOfDistanceCoveredByRacer + " div").css("width", WidthOfColorVisualization);
        }
        if (IntervalForUpdatingInfoAboutRacersWasCleared)
        {
            setTimeout(ReloadListOfRacers, 3000);
        }
    }

    function CheckRaceExistence()
    {
        $.ajax({
            url: UrlForCheckingRaceExistence,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (RaceStarted) {
                if (RaceStarted && !IntervalForCheckingRaceExistenceWasCleared)
                {
                    IntervalForCheckingRaceExistenceWasCleared = true;
                    clearInterval(IntervalForCheckingRaceExistence);
                    ReloadListOfRacers(true);
                }
            },
            error: function (xhr, textStatus, errorMessage) {
                alert(Resources.ErrorMessageForCheckingRaceExistence);
            }
        })
    }

    function GetCurrentRaceFinishDistanceAndStartUpdate() {
        $.ajax({
            url: UrlForGettingCurrentRaceFinishDistance,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (NewFinishDistanceFromServer) {
                CurrentRaceFinishDistance = Number(NewFinishDistanceFromServer);
                PublicMembers.StartUpdate();
            },
            error: function (xhr, textStatus, errorMessage) {
                alert(Resources.ErrorMessageForGettingCurrentRaceFinishDistance);
            }
        })
    }

    function DisableAbilityToCreateNewRace()
    {
        $(StartNewRaceButton).prop("disabled", true);
        $(InputForNewFinishDistance).prop("disabled", true);
        $("." + ClassOnlyVisibleDuringRace).css("display", "block");
    }

    function EnableAbilityToCreateNewRace()
    {
        $(StartNewRaceButton).prop("disabled", false);
        $(InputForNewFinishDistance).prop("disabled", false);
        $("." + ClassOnlyVisibleDuringRace).css("display", "none");
    }

    return PublicMembers;
}());

$(document).ready(function () {
    UpdatingRaceParticipantsInfo.GetInfoNeededForUpdatingRaceParticipantsInfo();   
})