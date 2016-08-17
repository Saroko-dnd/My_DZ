
var ArrayOfDownCanvasID = ['BlackSquare_1_1', 'BlackSquare_3_1', 'BlackSquare_5_1', 'BlackSquare_7_1', 'BlackSquare_2_2', 'BlackSquare_4_2', 'BlackSquare_6_2', 'BlackSquare_8_2', 'BlackSquare_1_3', 'BlackSquare_3_3', 'BlackSquare_5_3', 'BlackSquare_7_3'];
var ArrayOfTopCanvasID = ['BlackSquare_2_8', 'BlackSquare_4_8', 'BlackSquare_6_8', 'BlackSquare_8_8', 'BlackSquare_1_7', 'BlackSquare_3_7', 'BlackSquare_5_7', 'BlackSquare_7_7', 'BlackSquare_2_6', 'BlackSquare_4_6', 'BlackSquare_6_6', 'BlackSquare_8_6'];

function DrowCheckers()
{
    for (var Counter = 0; Counter < 12; ++Counter)
    {
        DrowBrownChecker(ArrayOfDownCanvasID[Counter]);
        var CurrentCanvas = document.getElementById('BlackSquare_1_1');
        var Context = CurrentCanvas.getContext("2d");
        var CheckerImage = new Image(50, 50);
        CheckerImage.src = '../Images/BrownChecker.png';
        Context.drawImage(CheckerImage, 50, 50);
    }
}



function DrowBrownChecker(ID)
{
   
    //alert(ID);
}