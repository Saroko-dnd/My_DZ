
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

var FadeMustBe = true;

$(document).ready(function () {
    $('#StartFadingButton').click(function()
    {
        while (FadeMustBe)
        {
            $('#FirstFadeImage').fadeIn(2000);
            $('#SecondFadeImage').fadeOut(2000);
            $('#FirstFadeImage').fadeOut(2000);
            $('#SecondFadeImage').fadeIn(2000);
        }
    })
}
)