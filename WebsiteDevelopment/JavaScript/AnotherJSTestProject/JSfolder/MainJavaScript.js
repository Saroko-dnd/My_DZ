

document.onload = AskUser;

function AskUser()
{
    /*var UserAge = prompt("Сколько вам лет?");
    if (UserAge < 18 )
    {
        alert("Жениться нельзя!! ");
    }
    else
    {
        alert("Жениться можно!! ");
    }*/
    document.getElementById("CreateTableButtonID").onclick = CreateTable;
    //GAME
    var SizeOfTheGameField = 10;
    var ArrayOfHTMLforGameField = [];
    for (var IndexOfGameField = 0; IndexOfGameField < SizeOfTheGameField; ++IndexOfGameField) {
        var GameRow = '<div  class="DivWithSquares">';
        var ButtonID = 'id=';
        var ColumnIndex = 0;
        for (var CounterOfButtons = 0; CounterOfButtons < SizeOfTheGameField; ++CounterOfButtons)
        {
            GameRow += '<input type="button" class="GameSquare"' + ButtonID + '"' + IndexOfGameField + '_' + ColumnIndex + '"' + '/>';
            ++ColumnIndex;
        }
        GameRow += '</div>';
        ArrayOfHTMLforGameField.push(GameRow);
    }
    var FullHtmlForGameField = '';

    for (var CounterOfRows = 0; CounterOfRows < ArrayOfHTMLforGameField.length; ++CounterOfRows)
    {
        FullHtmlForGameField += ArrayOfHTMLforGameField[CounterOfRows];
    }

    document.getElementById('GameFieldDivID').innerHTML = FullHtmlForGameField;
    var GameButtons = Create2dArray(SizeOfTheGameField, SizeOfTheGameField);
    var GameSquares = document.getElementsByClassName('GameSquare');
    for (var IndexOfButton = 0; IndexOfButton < GameSquares.length; ++IndexOfButton)
    {
        var FirstIndex = GameSquares[IndexOfButton].id.split("_")[0];
        var SecondIndex = GameSquares[IndexOfButton].id.split("_")[1];
        GameButtons[FirstIndex][SecondIndex] = GameSquares[IndexOfButton];
        GameButtonLeftClick(GameSquares[IndexOfButton]);
        GameSquares[IndexOfButton].oncontextmenu = GameButtonRightClick;
    }
    //GAME
    var AllImages = document.getElementsByTagName('img');
    for (var ImgIndex = 0; ImgIndex < AllImages.length; ++ImgIndex)
    {
        AllImages[ImgIndex].onclick = ChangeImageSizeOnClick;
        AllImages[ImgIndex].big = false;
    }
}

function Create2dArray(AmountOfRows, AmountOfColumns)
{
    var TwoDimensionalArray = new Array(AmountOfRows);
    for (var Index = 0; Index < AmountOfRows; ++Index)
    {
        TwoDimensionalArray[Index] = new Array(AmountOfColumns);
    }
    return TwoDimensionalArray;
}

function GameButtonLeftClick(GameButton)
{
    var RandomNumber;
    RandomNumber = 1 + Math.random() * (2 - 1);
    RandomNumber = Math.round(RandomNumber);
    if (RandomNumber == 1) {         
        GameButton.mine = true;
    }
    else {
        GameButton.mine = false;
    }
    GameButton.SafeFlag = false;
}

function GameButtonRightClick() {
    console.log('rightclick');
    if (this.SafeFlag == false)
    {
        this.SafeFlag == true;
    }
}

function ChangeImageSizeOnClick()
{
    console.log(this.id + "ID");
    console.log(this.style.width);
    console.log(this.big);
    if (this.big == false)
    {
        this.big = true;
        this.style.height = '300px';
        this.style.width = '300px';
    }
    else {
        this.big = false;
        this.style.width = '100px';
        this.style.height = '100px';
    }
}

function CreateTable()
{
    var AmountOfRows = document.getElementById("NumberOfRowsInputID").value;
    var AmountOfColumns = document.getElementById("NumberOfColumnsInputID").value;
    var Rows = [];
    for (var RowsCounter = 0; RowsCounter < AmountOfRows; ++RowsCounter)
    {
        var Columns = [];
        for (var ColumnsCounter = 0; ColumnsCounter < AmountOfColumns; ++ColumnsCounter)
        {
            Columns.push(RowsCounter * ColumnsCounter);
        }
        Rows.push(Columns);
    }
    var InnerTable = "<table>";
    for (var RowCounter = 0; RowCounter < Rows.length; ++RowCounter)
    {
        InnerTable += "<tr>"
        for (var ColumnCounter = 0; ColumnCounter < Rows[RowCounter].length; ++ColumnCounter)
        {
            InnerTable += "<td>";
            InnerTable += Rows[RowCounter][ColumnCounter];
            InnerTable += "</td>";
        }
        InnerTable += "</tr>"
    }
    InnerTable += "</table>";
    document.getElementById("DivForTableID").innerHTML = InnerTable;
}