
var NumberOfAnumation = 1;
var AllImages;
var MainDiv;

/*window.onresize = ResizeBodyHeight;

function ResizeBodyHeight()
{
    document.body.style.height = (MainDiv.height + 500).toString() + 'px';
}*/

$(document).ready(function () {
    MainDiv = $('MainDiv');
    var WindowOffset = $(window).scrollTop();
    AllImages = $("img");
    jQuery.each(AllImages, function (Index, Element) {
        if (WindowOffset >= (Element.offsetTop - ($(window).height() / 2))) {
            $(Element).animate({ opacity: '1' }, 1000);
        }
    });
    $(window).scroll(function () {
        var currentoffset = $(this).scrollTop();
        console.log(currentoffset);
        jQuery.each(AllImages, function (Index, Element) {
            if (currentoffset >= (Element.offsetTop - ($(window).height()/2)))
            {
                $(Element).animate({ opacity: '1' }, 1000);
            }
        });
    })
});
