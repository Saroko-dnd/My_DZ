
$(document).ready(function (){
    $("#HideButton_1").click(function ()
    {
        $('.FirstHide').hide();
    }
)
}
)


$(document).ready(function () {
    $("#HideButton_2").click(function () {
        $('#UniqueP').hide();
    }
)
}
)

$(document).ready(function () {
    $("#ChangeColorButtonID").click(function () {
        $('div p:nth-of-type(odd)').css('color','blue');
    }
)
}
)

$(document).ready(function () {
    $("#HideMsoNormalButtonID").click(function () {
        $('.MsoNormal:nth-of-type(odd)').hide();
    }
)
}
)

$(document).ready(function () {
    $("#ShowMsoNormalButtonID").click(function () {
        $('.MsoNormal:nth-of-type(odd)').show();
    }
)
}
)

$(document).ready(function () {
    var $images = $("table img");
    $images.mouseenter(function () {
        $(this).hide();
    })
}
)

//FADE TEST CODE*********************************************************************
var IntervalForFirstFadeFunction;
var IntervalForSecondFadeFunction;
var FirstSpaceImage;
var SecondSpaceImage;
var FirstTime = true;

function FirstFadeFunctionForSpaceImage() {
    if (FirstTime) {
        FirstTime = false;
        IntervalForSecondFadeFunction = setInterval(SecondFadeFunctionForSpaceImage, 2000);
    }
    FirstSpaceImage.fadeToggle(2000);
}

function SecondFadeFunctionForSpaceImage() {
    SecondSpaceImage.fadeToggle(2000);
}

$(document).ready(function () {
    FirstSpaceImage = $('#FirstSpaceImageID');
    SecondSpaceImage = $('#SecondSpaceImageID');
}
)

$(document).ready(function () {
    $('#StartFadeTestButtonID').click(function ()
    {
        FirstTime = true;
        FirstFadeFunctionForSpaceImage();
        IntervalForFirstFadeFunction = setInterval(FirstFadeFunctionForSpaceImage, 2000);
    })
}
)

$(document).ready(function () {
    $('#EndFadeTestButtonID').click(function () {
        clearInterval(IntervalForFirstFadeFunction);
        clearInterval(IntervalForSecondFadeFunction);
        FirstSpaceImage.fadeIn();
        SecondSpaceImage.fadeIn();
    })
}
)
//***********************************************************************************
