
window.onload = AskUser;

var GameButtons;
var GameOver = false;

function AskUser() {
    //GAME
    var SizeOfTheGameField = 16;
    var AmountOfMines = 40;
    var ArrayOfHTMLforGameField = [];
    for (var IndexOfGameField = 0; IndexOfGameField < SizeOfTheGameField; ++IndexOfGameField) {
        var GameRow = '<div  class="DivWithSquares">';
        var ButtonID = 'id=';
        var ColumnIndex = 0;
        for (var CounterOfButtons = 0; CounterOfButtons < SizeOfTheGameField; ++CounterOfButtons) {
            GameRow += '<input type="button" class="GameSquare"' + ButtonID + '"' + IndexOfGameField + '_' + ColumnIndex + '"' + '/>';
            ++ColumnIndex;
        }
        GameRow += '</div>';
        ArrayOfHTMLforGameField.push(GameRow);
    }
    var FullHtmlForGameField = '';

    for (var CounterOfRows = 0; CounterOfRows < ArrayOfHTMLforGameField.length; ++CounterOfRows) {
        FullHtmlForGameField += ArrayOfHTMLforGameField[CounterOfRows];
    }

    document.getElementById('GameFieldDivID').innerHTML = FullHtmlForGameField;
    GameButtons = Create2dArray(SizeOfTheGameField, SizeOfTheGameField);
    var GameSquares = document.getElementsByClassName('GameSquare');
    for (var IndexOfButton = 0; IndexOfButton < GameSquares.length; ++IndexOfButton) {
        var FirstIndex = GameSquares[IndexOfButton].id.split("_")[0];
        var SecondIndex = GameSquares[IndexOfButton].id.split("_")[1];
        GameSquares[IndexOfButton].oncontextmenu = GameButtonRightClick;
        GameButtons[FirstIndex][SecondIndex] = GameSquares[IndexOfButton];
    }
    PrepareField(SizeOfTheGameField);
    LayMines(AmountOfMines, SizeOfTheGameField);
    //GAME
}

function PrepareField(CurrentSizeOfGameField)
{
    for (var FirstIndex = 0; FirstIndex < CurrentSizeOfGameField; ++FirstIndex)
    {
        for (var SecondIndex = 0; SecondIndex < CurrentSizeOfGameField; ++SecondIndex)
        {
            GameButtons[FirstIndex][SecondIndex].mine = false;
            GameButtons[FirstIndex][SecondIndex].flag = false;
            GameButtons[FirstIndex][SecondIndex].onclick = GameButtonLeftClick;         
        }
    }
}

function LayMines(CurrentAmountOfMines, CurrentSizeOfGameField)
{
    var RandomFirstIndex;
    var RandomSecondIndex;
    for (var CounterOfMines = 0; CounterOfMines < CurrentAmountOfMines; ++CounterOfMines)
    {
        RandomFirstIndex = GetRandomInt(0, CurrentSizeOfGameField - 1);
        RandomSecondIndex = GetRandomInt(0, CurrentSizeOfGameField - 1);
        if (GameButtons[RandomFirstIndex][RandomSecondIndex].mine == false)
        {
            GameButtons[RandomFirstIndex][RandomSecondIndex].mine = true;
        }
        else
        {
            while (true)
            {
                //console.log(CounterOfMines);
                RandomFirstIndex = GetRandomInt(0, CurrentSizeOfGameField - 1);
                RandomSecondIndex = GetRandomInt(0, CurrentSizeOfGameField - 1);
                if (GameButtons[RandomFirstIndex][RandomSecondIndex].mine == false) {
                    GameButtons[RandomFirstIndex][RandomSecondIndex].mine = true;
                    break;
                }
            }
        }
    }
}

function GetRandomInt(MinValue, MaxValue)
{
    var RandomInt = MinValue + Math.random() * (MaxValue - MinValue);
    RandomInt = Math.round(RandomInt);
    return RandomInt;
}

function Create2dArray(AmountOfRows, AmountOfColumns) {
    var TwoDimensionalArray = new Array(AmountOfRows);
    for (var Index = 0; Index < AmountOfRows; ++Index) {
        TwoDimensionalArray[Index] = new Array(AmountOfColumns);
    }
    return TwoDimensionalArray;
}

function GameButtonLeftClick() {
    if (!GameOver)
    {
        if (this.mine)
        {
            this.style.backgroundImage = "url('Images/SmallMine.png')";
            this.style.backgroundColor = "transparent";
            GameOver = true;
            document.getElementById('PForGameResultID').innerText = "Game over!";
        }
        else
        {

        }
    }
}

function GameButtonRightClick() {
    console.log('rightclick');
    if (this.SafeFlag == false) {
        this.SafeFlag == true;
    }
}

