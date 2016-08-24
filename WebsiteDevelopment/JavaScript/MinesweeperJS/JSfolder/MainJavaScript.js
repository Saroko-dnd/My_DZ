
window.onload = AskUser;

var GameButtons;
var GameOver = false;
var AmountOfMines;
var SizeOfTheGameField;
var AmountOfFlags;
var ParagraphWithAmountOfFlags;

function AskUser() {
    //GAME
    SizeOfTheGameField = 16;
    AmountOfMines = 40;
    AmountOfFlags = 40;
    ParagraphWithAmountOfFlags = document.getElementById('PForAmountOfFlagsID');
    ParagraphWithAmountOfFlags.innerText = " " + AmountOfFlags.toString();
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
        var FirstIndex = Number(GameSquares[IndexOfButton].id.split("_")[0]);
        var SecondIndex = Number(GameSquares[IndexOfButton].id.split("_")[1]);
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
            GameButtons[FirstIndex][SecondIndex].SafePlace = false;
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
    if (!GameOver && !this.SafePlace)
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
            var FirstIndex = Number(this.id.split('_')[0]);
            var SecondIndex = Number(this.id.split('_')[1]);
            CheckButtonsAround(FirstIndex, SecondIndex);
        }
    }
}

function CheckButtonsAround(FirstIndexInGameArray, SecondIndexInGameArray)
{
    if (!GameButtons[FirstIndexInGameArray][SecondIndexInGameArray].SafePlace)
    {
        GameButtons[FirstIndexInGameArray][SecondIndexInGameArray].SafePlace = true;
        GameButtons[FirstIndexInGameArray][SecondIndexInGameArray].style.backgroundColor = "transparent";
        var AmountOfMinesNearTheButton = 0;
        if ((FirstIndexInGameArray - 1) >= 0 && (SecondIndexInGameArray - 1) >= 0)
        {
            if (GameButtons[FirstIndexInGameArray - 1][SecondIndexInGameArray - 1].mine == true)
            {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((FirstIndexInGameArray + 1) < SizeOfTheGameField && (SecondIndexInGameArray + 1) < SizeOfTheGameField) {
            if (GameButtons[FirstIndexInGameArray + 1][SecondIndexInGameArray + 1].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((FirstIndexInGameArray - 1) >= 0) {
            if (GameButtons[FirstIndexInGameArray - 1][SecondIndexInGameArray].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((FirstIndexInGameArray + 1) < SizeOfTheGameField) {
            if (GameButtons[FirstIndexInGameArray + 1][SecondIndexInGameArray].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((SecondIndexInGameArray - 1) >= 0) {
            if (GameButtons[FirstIndexInGameArray][SecondIndexInGameArray - 1].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((SecondIndexInGameArray + 1) < SizeOfTheGameField) {
            if (GameButtons[FirstIndexInGameArray][SecondIndexInGameArray + 1].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((FirstIndexInGameArray - 1) >= 0 && (SecondIndexInGameArray + 1) < SizeOfTheGameField) {
            if (GameButtons[FirstIndexInGameArray - 1][SecondIndexInGameArray + 1].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if ((FirstIndexInGameArray + 1) < SizeOfTheGameField && (SecondIndexInGameArray - 1) >= 0) {
            if (GameButtons[FirstIndexInGameArray + 1][SecondIndexInGameArray - 1].mine == true) {
                AmountOfMinesNearTheButton += 1;
            }
        }
        if (AmountOfMinesNearTheButton > 0) {
            GameButtons[FirstIndexInGameArray][SecondIndexInGameArray].value = AmountOfMinesNearTheButton;
        }
        else
        {
            if ((FirstIndexInGameArray - 1) >= 0 && (SecondIndexInGameArray - 1) >= 0) {
                CheckButtonsAround(FirstIndexInGameArray - 1, SecondIndexInGameArray - 1);
                console.log((FirstIndexInGameArray - 1) + " " + (SecondIndexInGameArray - 1));
            }
            if ((FirstIndexInGameArray + 1) < SizeOfTheGameField && (SecondIndexInGameArray + 1) < SizeOfTheGameField) {
                CheckButtonsAround(FirstIndexInGameArray + 1, SecondIndexInGameArray + 1);
                console.log((FirstIndexInGameArray + 1) + " " + (SecondIndexInGameArray + 1));
            }
            if ((FirstIndexInGameArray - 1) >= 0) {
                CheckButtonsAround(FirstIndexInGameArray - 1, SecondIndexInGameArray);
                console.log((FirstIndexInGameArray - 1) + " " + SecondIndexInGameArray);
            }
            if ((FirstIndexInGameArray + 1) < SizeOfTheGameField) {
                CheckButtonsAround(FirstIndexInGameArray + 1, SecondIndexInGameArray);
                console.log((FirstIndexInGameArray + 1) + " " + SecondIndexInGameArray);
            }
            if ((SecondIndexInGameArray - 1) >= 0) {
                CheckButtonsAround(FirstIndexInGameArray, SecondIndexInGameArray - 1);
                console.log(FirstIndexInGameArray + " " + (SecondIndexInGameArray - 1));
            }
            if ((SecondIndexInGameArray + 1) < SizeOfTheGameField) {
                CheckButtonsAround(FirstIndexInGameArray, SecondIndexInGameArray + 1);
                console.log(FirstIndexInGameArray + " " + (SecondIndexInGameArray + 1));
            }
            if ((FirstIndexInGameArray - 1) >= 0 && (SecondIndexInGameArray + 1) < SizeOfTheGameField) {
                CheckButtonsAround(FirstIndexInGameArray - 1, SecondIndexInGameArray + 1);
                console.log((FirstIndexInGameArray - 1) + " " + (SecondIndexInGameArray + 1));
            }
            if ((FirstIndexInGameArray + 1) < SizeOfTheGameField && (SecondIndexInGameArray - 1) >= 0) {
                CheckButtonsAround(FirstIndexInGameArray + 1, SecondIndexInGameArray - 1);
                console.log((FirstIndexInGameArray + 1) + " " + (SecondIndexInGameArray - 1));
            }
        }
    }
}

function GameButtonRightClick()
{
    if (this.SafePlace == false && AmountOfFlags > 0 && !GameOver)
    {
        this.style.backgroundImage = "url('Images/SmallFlag.png')";
        this.style.backgroundColor = "transparent";
        this.SafePlace = true;
        --AmountOfFlags;
        ParagraphWithAmountOfFlags.innerText = " " + AmountOfFlags.toString();
        if (this.mine)
        {
            --AmountOfMines;
            if (AmountOfMines == 0)
            {
                GameOver = true;
                document.getElementById('PForGameResultID').innerText = "Victory!";
            }
        }
    }
    //Ниже отключаем дальнейшую обработку правого клика (убирает контекстное меню браузера)
    return false;
}

