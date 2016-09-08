
var GameCanvas;
var GameFieldContext;
var LeftPlayerRect;
var RightPlayerRect;
var GameBall;
var BorderXforLeftPlayer = 275;
var BorderXForRightPlayer = 525;
var BorderMaxXForRightPlayer = 800;
var IntervalForDrawing;
var BallRadius = 50;

window.onkeydown = KeyDownEventHandler;
window.onkeyup = KeyUpEventHandler;

$(document).ready(
    function ()
    {
        GameCanvas = document.getElementById("MainCanvasForGame");
        GameBall = { x: 500, y: 50, radius: 50, verticalSpeed: 0, horizontalSpeed: GetRandomSpeedForBall(-1, 1) };
        LeftPlayerRect = { x: 0, y: 500, width: 200, height: 300, color: 'black', speed: 0 };
        RightPlayerRect = { x: 800, y: 500, width: 200, height: 300, color: 'blue', speed:0 };
        GameFieldContext = GameCanvas.getContext('2d');
        DrawGameField();
        IntervalForDrawing = setInterval(DrawGameField, 10);
    });

function KeyDownEventHandler(event)
{
    if (event.keyCode == 68)
    {
        console.log(68);
        LeftPlayerRect.speed = 1;
    }
    else if(event.keyCode == 65)
    {
        console.log(65);
        LeftPlayerRect.speed = -1;
    }
    else if (event.keyCode == 39)
    {
        console.log(39);
        RightPlayerRect.speed = 1;
    }
    else if (event.keyCode == 37)
    {
        console.log(37);
        RightPlayerRect.speed = -1;
    }
}

function KeyUpEventHandler(event)
{
    if (event.keyCode == 68 || event.keyCode == 65) {
        LeftPlayerRect.speed = 0;
    }
    else if (event.keyCode == 39 || event.keyCode == 37) {
        RightPlayerRect.speed = 0;
    }
}

function DrawGameField()
{
    //Очищаем игровое поле
    GameFieldContext.beginPath();
    GameFieldContext.clearRect(0, 0, GameCanvas.width, GameCanvas.height);
    GameFieldContext.stroke();
    //Рисуем мяч 
    if (GameBall.x == BallRadius)
    {

    }
    else if (GameBall.y == BallRadius)
    {

    }
    else if (GameBall.x == 750)
    {

    }
    else if (GameBall.y == 950)
    {

    }
    else if (GameBall.x > LeftPlayerRect.x && (GameBall.x < LeftPlayerRect.x + LeftPlayerRect.width))
    {
        if (GameBall.y <= LeftPlayerRect.y + BallRadius)
        {
            GameBall.horizontalSpeed = 1;
        }
    }
    else if (GameBall.x > RightPlayerRect.x && (GameBall.x < RightPlayerRect.x + RightPlayerRect.width)) {
        if (GameBall.y <= RightPlayerRect.y + BallRadius) {
            GameBall.horizontalSpeed = -1;
        }
    }
    else if (GameBall.y > BallRadius && GameBall.y < LeftPlayerRect.height)
    {
        if ((GameBall.x >= LeftPlayerRect.x - BallRadius) && (GameBall.x <= LeftPlayerRect.x + BallRadius)) {
            GameBall.horizontalSpeed = 1;
        }
    }
    else if (GameBall.y > BallRadius && GameBall.y < RightPlayerRect.height) {
        if ((GameBall.x >= RightPlayerRect.x - BallRadius) && (GameBall.x <= RightPlayerRect.x + BallRadius)) {
            GameBall.horizontalSpeed = -1;
        }
    }
    GameFieldContext.beginPath();

    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = 'green';
    GameFieldContext.fillRect(475, 400, 50, 400);
    GameFieldContext.stroke();
    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = LeftPlayerRect.color;
    if (LeftPlayerRect.x == 0 && LeftPlayerRect.speed > 0)
    {
        LeftPlayerRect.x += LeftPlayerRect.speed;
    }
    else if (LeftPlayerRect.x == BorderXforLeftPlayer && LeftPlayerRect.speed < 0)
    {
        LeftPlayerRect.x += LeftPlayerRect.speed;
    }
    else if ((LeftPlayerRect.x < BorderXforLeftPlayer) && LeftPlayerRect.x > 0)
    {
        LeftPlayerRect.x += LeftPlayerRect.speed;
    }
    GameFieldContext.fillRect(LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
    GameFieldContext.stroke();
    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = RightPlayerRect.color;
    if (RightPlayerRect.x == BorderXForRightPlayer && RightPlayerRect.speed > 0) {
        console.log('shift right 1');
        RightPlayerRect.x += RightPlayerRect.speed;
    }
    else if (RightPlayerRect.x == BorderMaxXForRightPlayer && RightPlayerRect.speed < 0) {
        console.log('shift right 2');
        RightPlayerRect.x += RightPlayerRect.speed;
    }
    else if ((RightPlayerRect.x > BorderXForRightPlayer) && (RightPlayerRect.x < BorderMaxXForRightPlayer)) {
        console.log('shift right 3');
        RightPlayerRect.x += RightPlayerRect.speed;
    }
    GameFieldContext.fillRect(RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
    GameFieldContext.stroke();
}

function GetRandomSpeedForBall(min, max)
{
    var RandomSpeed = 0;
    while (RandomSpeed == 0)
    {
        RandomSpeed = Math.floor(Math.random() * ((max + 1) - min)) + min;
    }
    return RandomSpeed;
}