
var GameCanvas;
var GameFieldArray = new Array(4);
var ContextOfGameCanvas;
var XMousePosition;
var YMousePosition;
var MouseDownInsideCanvas = false;
var SelectedFigure = { Xposition: -1, Yposition: -1, CurrentGameNumber: -1, CurrentColor: '' };
var FirstIndexOfEmptyPlace = -1;
var SecondIndexOfEmptyPlace = -1;
var CopyOfFirstIndexForSelectedFigure = -1;
var CopyOfSecondIndexForSelectedFigure = -1;
var FigureWasSelected = false;
var Victory;
var MouseJustClicked = true;

window.onload = StartTrackMouse;

function StartTrackMouse()
{
    GameCanvas = document.getElementById('CanvasForSliderPuzzle');
    GameCanvas.onmousedown = TrackMouseDown;
    GameCanvas.onmouseup = TrackMouseUp;
    GameCanvas.onmousemove = TrackMouse;
    ContextOfGameCanvas = GameCanvas.getContext("2d");
    ContextOfGameCanvas.font = '40px Arial';
    ContextOfGameCanvas.textAlign = "center";
    ContextOfGameCanvas.textBaseline = "middle";
    GenerateGameField();
    DrawGameField();
    //setInterval(ChangingPositionOfFiguresOnDragAndDrop, 50);
}

function GenerateGameField()
{
    CreateGameFieldBasics();
    GenerateGameNumbers();
    FindIndexesOfEmptySpace();
}

function CreateGameFieldBasics()
{
    //Создаем массив
    for (var Index = 0; Index < 4; ++Index) {
        GameFieldArray[Index] = new Array(4);
    }
    //Заполняем массив фигурами
    var DegreeForColor = 0;
    var SForColor = 1.0;
    var VForColor = 1.0;
    var CurrentColorRGB;
    for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
        for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
            GameFieldArray[FirstIndex][SecondIndex] = new Object();
            GameFieldArray[FirstIndex][SecondIndex].Xposition = (SecondIndex * 100) + 200;
            GameFieldArray[FirstIndex][SecondIndex].Yposition = (FirstIndex * 100) + 200;
            GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber = -1;
            CurrentColorRGB = HSVtoRGB(DegreeForColor / 360, SForColor, VForColor);
            GameFieldArray[FirstIndex][SecondIndex].CurrentColor = "rgb(" + CurrentColorRGB.r.toString() + ", " + CurrentColorRGB.g.toString() + ", " + CurrentColorRGB.b.toString() + ")";
            DegreeForColor += 60;
            if (DegreeForColor >= 360) {
                DegreeForColor -= 360;
                SForColor -= 0.3;
                VForColor -= 0.3;
            }
        }
    }
}

function GenerateGameNumbers()
{
    //Расставляем числа на игровом поле случайным образом
    do {
        var RandomNumberForShiftCycle;
        var NumberOfCycle = 0;
        for (var GameNumber = 1; GameNumber < 16; ++GameNumber) {
            RandomNumberForShiftCycle = GetRandomInt(1, 16);
            while (NumberOfCycle != RandomNumberForShiftCycle) {
                for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
                    for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
                        if (GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber === -1) {
                            ++NumberOfCycle;
                            if (NumberOfCycle === RandomNumberForShiftCycle) {
                                GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber = GameNumber;
                                break;
                            }
                        }
                    }
                    if (NumberOfCycle === RandomNumberForShiftCycle) {
                        break;
                    }
                }
            }
            NumberOfCycle = 0;
        }
        Victory = false;
        CheckWin();
        if (Victory) {
            for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
                for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
                    GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber = -1;
                }
            }
        }
    } while (Victory)
}

function FindIndexesOfEmptySpace()
{
    for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
        for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
            if (GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber === -1) {
                FirstIndexOfEmptyPlace = FirstIndex;
                SecondIndexOfEmptyPlace = SecondIndex;
                break;
            }
        }
        if (FirstIndexOfEmptyPlace > -1) {
            break;
        }
    }
}

function DrawGameField()
{
    //Рисуем игровое поле
    ContextOfGameCanvas.beginPath();
    ContextOfGameCanvas.lineWidth = 10;
    ContextOfGameCanvas.clearRect(0, 0, 800, 800);
    ContextOfGameCanvas.strokeRect(200, 200, 400, 400);
    for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
        for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
            ContextOfGameCanvas.strokeRect((SecondIndex * 100) + 200, (FirstIndex * 100) + 200, 100, 100);         
        }
    }
    ContextOfGameCanvas.stroke();
    //Рисуем цветные фигуры с числами
    for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
        for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
            if (GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber != -1)
            {
                if (!(FirstIndex === CopyOfFirstIndexForSelectedFigure && SecondIndex === CopyOfSecondIndexForSelectedFigure))
                {
                    ContextOfGameCanvas.beginPath();
                    ContextOfGameCanvas.fillStyle = GameFieldArray[FirstIndex][SecondIndex].CurrentColor;
                    ContextOfGameCanvas.fillRect(GameFieldArray[FirstIndex][SecondIndex].Xposition, GameFieldArray[FirstIndex][SecondIndex].Yposition, 100, 100);
                    ContextOfGameCanvas.stroke();
                    ContextOfGameCanvas.beginPath();
                    ContextOfGameCanvas.fillStyle = "black";
                    ContextOfGameCanvas.fillText(GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber.toString(), GameFieldArray[FirstIndex][SecondIndex].Xposition + 50,
                        GameFieldArray[FirstIndex][SecondIndex].Yposition + 50);
                    ContextOfGameCanvas.stroke();
                }
            }
        }
    }
    //Последней рисуем фигуру передвигаемую игроком
    if (CopyOfFirstIndexForSelectedFigure != -1)
    {
        ContextOfGameCanvas.beginPath();
        ContextOfGameCanvas.fillStyle = GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].CurrentColor;
        ContextOfGameCanvas.fillRect(GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].Xposition,
            GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].Yposition, 100, 100);
        ContextOfGameCanvas.stroke();
        ContextOfGameCanvas.beginPath();
        ContextOfGameCanvas.fillStyle = "black";
        ContextOfGameCanvas.fillText(GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].CurrentGameNumber.toString(),
            GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].Xposition + 50, GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].Yposition + 50);
        ContextOfGameCanvas.stroke();
    }
}

function CheckWin()
{
    var EndCycle = false;
    var NoVictory = false;
    var PreviousGameNumber = 0;
    for (var FirstIndex = 0; FirstIndex < 4; ++FirstIndex) {
        for (var SecondIndex = 0; SecondIndex < 4; ++SecondIndex) {
            if (GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber - PreviousGameNumber != 1) {
                NoVictory = true;
                break;
            }
            else
            {
                if (GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber === 15)
                {
                    EndCycle = true;
                    break;
                }
                else
                {
                    PreviousGameNumber = GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber;
                }
            }
        }
        if (NoVictory || EndCycle)
        {
            break;
        }
    }
    if (!NoVictory)
    {
        Victory = true;
        document.getElementById('ParagraphForWin').appendChild(document.createTextNode('Victory!'));
    }
}

function ChangingPositionOfFiguresOnDragAndDrop()
{
    if (MouseDownInsideCanvas && !Victory)
    {
        HandlerForMouseDownInsideCanvas();
    }
    else if (!Victory)
    {
        HandlerForMouseMoveInsideCanvas();
    }
}

function HandlerForMouseMoveInsideCanvas()
{
    if (YMousePosition >= 200 && YMousePosition <= 600 && XMousePosition >= 200 && XMousePosition <= 600) {
        var FirstIndex = Math.floor((YMousePosition - 200) / 100);
        var SecondIndex = Math.floor((XMousePosition - 200) / 100);
        var ThisFigureCanMove = false;
        if ((FirstIndex + 1) === FirstIndexOfEmptyPlace && SecondIndex === SecondIndexOfEmptyPlace) {
            ThisFigureCanMove = true;
        }
        else if ((FirstIndex - 1) === FirstIndexOfEmptyPlace && SecondIndex === SecondIndexOfEmptyPlace) {
            ThisFigureCanMove = true;
        }
        else if (FirstIndex === FirstIndexOfEmptyPlace && (SecondIndex + 1) === SecondIndexOfEmptyPlace) {
            ThisFigureCanMove = true;
        }
        else if (FirstIndex === FirstIndexOfEmptyPlace && (SecondIndex - 1) === SecondIndexOfEmptyPlace) {
            ThisFigureCanMove = true;
        }
        if (ThisFigureCanMove) {
            GameCanvas.style.cursor = "pointer";
        }
        else {
            GameCanvas.style.cursor = "default";
        }
    }
    else {
        GameCanvas.style.cursor = "default";
    }
}

function HandlerForMouseDownInsideCanvas()
{
    if (FigureWasSelected) {
        SelectedFigure.Xposition = XMousePosition - 50;
        SelectedFigure.Yposition = YMousePosition - 50;
        DrawGameField();
    }
    else {
        if (MouseJustClicked) {
            if (YMousePosition >= 200 && YMousePosition <= 600 && XMousePosition >= 200 && XMousePosition <= 600) {
                var FirstIndex = Math.floor((YMousePosition - 200) / 100);
                var SecondIndex = Math.floor((XMousePosition - 200) / 100);
                var ThisFigureCanMove = false;
                if ((FirstIndex + 1) === FirstIndexOfEmptyPlace && SecondIndex === SecondIndexOfEmptyPlace) {
                    ThisFigureCanMove = true;
                }
                else if ((FirstIndex - 1) === FirstIndexOfEmptyPlace && SecondIndex === SecondIndexOfEmptyPlace) {
                    ThisFigureCanMove = true;
                }
                else if (FirstIndex === FirstIndexOfEmptyPlace && (SecondIndex + 1) === SecondIndexOfEmptyPlace) {
                    ThisFigureCanMove = true;
                }
                else if (FirstIndex === FirstIndexOfEmptyPlace && (SecondIndex - 1) === SecondIndexOfEmptyPlace) {
                    ThisFigureCanMove = true;
                }
                if (ThisFigureCanMove) {
                    //For IE
                    GameCanvas.style.cursor = "move";
                    //For Google Chrome
                    GameCanvas.style.cursor = "-webkit-grab";
                    //For Firefox
                    GameCanvas.style.cursor = "-moz-grab";
                    SelectedFigure = GameFieldArray[FirstIndex][SecondIndex];
                    CopyOfFirstIndexForSelectedFigure = FirstIndex;
                    CopyOfSecondIndexForSelectedFigure = SecondIndex;
                    FigureWasSelected = true;
                }
            }
            MouseJustClicked = false;         
        }
    }
}

function TrackMouse(event)
{
    if (!Victory)
    {
        XMousePosition = event.offsetX;
        YMousePosition = event.offsetY;
        ChangingPositionOfFiguresOnDragAndDrop();
    }
}

function TrackMouseDown(event)
{
    MouseDownInsideCanvas = true;
    MouseJustClicked = true;
    if (GameCanvas.style.cursor === "pointer")
    {
        //For IE
        GameCanvas.style.cursor = "move";
        //For Google Chrome
        GameCanvas.style.cursor = "-webkit-grab";
        //For Firefox
        GameCanvas.style.cursor = "-moz-grab";
    }
}

function TrackMouseUp(event) {
    MouseDownInsideCanvas = false;
    if (FigureWasSelected && !Victory)
    {
        var FirstIndex = Math.floor((YMousePosition - 200) / 100);
        var SecondIndex = Math.floor((XMousePosition - 200) / 100);
        if (FirstIndex === FirstIndexOfEmptyPlace && SecondIndex === SecondIndexOfEmptyPlace)
        {
            GameFieldArray[FirstIndex][SecondIndex].CurrentGameNumber = SelectedFigure.CurrentGameNumber;
            GameFieldArray[FirstIndex][SecondIndex].CurrentColor = SelectedFigure.CurrentColor;
            GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].CurrentGameNumber = -1;
            FirstIndexOfEmptyPlace = CopyOfFirstIndexForSelectedFigure;
            SecondIndexOfEmptyPlace = CopyOfSecondIndexForSelectedFigure;
        }
        GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].Xposition = (CopyOfSecondIndexForSelectedFigure * 100) + 200;
        GameFieldArray[CopyOfFirstIndexForSelectedFigure][CopyOfSecondIndexForSelectedFigure].Yposition = (CopyOfFirstIndexForSelectedFigure * 100) + 200;
        CopyOfFirstIndexForSelectedFigure = -1;
        CopyOfSecondIndexForSelectedFigure = -1;
        DrawGameField();
        CheckWin();
        FigureWasSelected = false;
        GameCanvas.style.cursor = "default";
    }
}

function GetRandomInt(MinValue, MaxValue)
{
    var RandomInt = MinValue + Math.random() * (MaxValue - MinValue);
    RandomInt = Math.round(RandomInt);
    return RandomInt;
}

function HSVtoRGB(h, s, v) {
    var r, g, b, i, f, p, q, t;
    if (arguments.length === 1) {
        s = h.s, v = h.v, h = h.h;
    }
    i = Math.floor(h * 6);
    f = h * 6 - i;
    p = v * (1 - s);
    q = v * (1 - f * s);
    t = v * (1 - (1 - f) * s);
    switch (i % 6) {
        case 0: r = v, g = t, b = p; break;
        case 1: r = q, g = v, b = p; break;
        case 2: r = p, g = v, b = t; break;
        case 3: r = p, g = q, b = v; break;
        case 4: r = t, g = p, b = v; break;
        case 5: r = v, g = p, b = q; break;
    }
    return {
        r: Math.round(r * 255),
        g: Math.round(g * 255),
        b: Math.round(b * 255)
    };
}