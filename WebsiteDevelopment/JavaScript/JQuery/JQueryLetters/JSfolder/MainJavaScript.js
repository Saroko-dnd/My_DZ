 

$(document).ready(function () {
    $('img').draggable(
        {
            drag: function () {
                $(this).css("z-index","11");
            }
        });
})

$(document).ready(function () {
    $('.Department, #DivWithNewLetters').droppable(
        {
            drop: function (event, ui) {
                ui.draggable.css("left", '0px');
                ui.draggable.css("top", '0px');
                ui.draggable.css("position", 'relative');
                $(this).css("background-color", "yellow").append(ui.draggable);
            }
        });
})

$(document).ready(function () {
    $('#MainDivID').droppable(
        {
            drop: function (event, ui) {
                ui.draggable.css("left", '0px');
                ui.draggable.css("top", '0px');
                ui.draggable.css("position", 'relative');
                $(this).children("div.first").append(ui.draggable);
            }
        });
})

$(document).ready(function () {
    $('body').droppable(
        {
            drop: function (event, ui) {
                ui.draggable.css("left", '0px');
                ui.draggable.css("top", '0px');
                ui.draggable.css("position", 'relative');
                $(this).children("div > div.first").append(ui.draggable);
            }
        });
})