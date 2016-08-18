
var ArrayOfDownImagesID = ['BlackSquare_1_1', 'BlackSquare_3_1', 'BlackSquare_5_1', 'BlackSquare_7_1', 'BlackSquare_2_2', 'BlackSquare_4_2', 'BlackSquare_6_2', 'BlackSquare_8_2', 'BlackSquare_1_3', 'BlackSquare_3_3', 'BlackSquare_5_3', 'BlackSquare_7_3'];
var ArrayOfTopImagesID = ['BlackSquare_2_8', 'BlackSquare_4_8', 'BlackSquare_6_8', 'BlackSquare_8_8', 'BlackSquare_1_7', 'BlackSquare_3_7', 'BlackSquare_5_7', 'BlackSquare_7_7', 'BlackSquare_2_6', 'BlackSquare_4_6', 'BlackSquare_6_6', 'BlackSquare_8_6'];

var GameFieldArrays = new Array(8);

var BlackCheckerType = 'BlackChecker';
var BrownCheckerType = 'BrownChecker';
var EmptySquare = 'Nothing';
var FirstPartOfID = 'BlackSquare_';

function DrowCheckers()
{
    ArrayOfDownImagesID.forEach(DrawBrownChecker);
    ArrayOfTopImagesID.forEach(DrawBlackChecker);
    CreateGameFieldArrays();
}

function CreateGameFieldArrays()
{
    for (var IndexOfMainArray = 0; IndexOfMainArray <=  7; ++IndexOfMainArray)
    {
        if ((IndexOfMainArray % 2) != 0)
        {
            GameFieldArrays[IndexOfMainArray] = CreateRowOfGameField(true, IndexOfMainArray + 1);
        }
        else
        {
            GameFieldArrays[IndexOfMainArray] = CreateRowOfGameField(false, IndexOfMainArray + 1);
        }
    }
}

function CreateRowOfGameField(BoolBlackSquareEven, IntYAxisValue)
{

    if (BoolBlackSquareEven)
    {
        return FillGameRowWithObjects(1, 7, IntYAxisValue);
    }
    else
    {
        return FillGameRowWithObjects(0, 6, IntYAxisValue);
    }
}

function FillGameRowWithObjects(IntFirstIndex, IntLastIndex, IntYAxisValue )
{
    var RowOfGameField = new Array(8);
    for (var IndexOfRowArray = IntFirstIndex; IndexOfRowArray <= IntLastIndex; IndexOfRowArray += 2)
    {
        var GameSquareImage = document.getElementById(FirstPartOfID + (IndexOfRowArray + 1) + '_' + IntYAxisValue);
        RowOfGameField[IndexOfRowArray] = GameSquareImage;
        alert(GameSquareImage.src);
    }
    return RowOfGameField;
}

function DrawBrownChecker(ID, IndexOfID, ArrayWithID)
{
    document.getElementById(ID).src = 'Images/BrownChecker.png';
}

function DrawBlackChecker(ID, IndexOfID, ArrayWithID)
{
    document.getElementById(ID).src = 'Images/BlackChecker.png';
}