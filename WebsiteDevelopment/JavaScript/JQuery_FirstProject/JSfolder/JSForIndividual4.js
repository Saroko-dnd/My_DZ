

//Array.eq(index) - way to operate with element of array as with JQuery object
$(document).ready(function () {
    $("#StartMoveParagraphsButtonID").click(function () {
        var ArrayOfParagraphs = $('p');
        ArrayOfParagraphs.eq(2).animate({ left: '-=100px' });
        ArrayOfParagraphs.eq(4).animate({ left: '-=100px' });
    }
)
}
)

$(document).ready(function () {
    $("img").click(function () {
        $(this).animate({ top: '+=30px' });
    }
)
}
)