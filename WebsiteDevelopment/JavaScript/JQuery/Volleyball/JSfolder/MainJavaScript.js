
var GameCanvas;
var GameFieldContext;
var LeftPlayerRect;
var RightPlayerRect;
var Grid;
var GameBall;
var BorderXforLeftPlayer = 375;
var BorderXForRightPlayer = 525;
var BorderMaxXForRightPlayer = 900;
var IntervalForDrawing;
var BallRadius = 50;
var BallWasThrownByLeftPlayer = false;
var BallWasThrownByRightPlayer = false;
var BallHitLeftWall = false;
var BallHitRightWall = false;
var BallHitRoof = false;
var BallMoveDistance;

window.onkeydown = KeyDownEventHandler;
window.onkeyup = KeyUpEventHandler;

$(document).ready(
    function ()
    {
        GameCanvas = document.getElementById("MainCanvasForGame");
        GameBall = { x: 500, y: 52, radius: 50, YSpeed: 2, XSpeed: GetRandomSpeedForBall(-1, 1), color:'red' };
        LeftPlayerRect = { x: 0, y: 600, width: 100, height: 200, color: 'black', speed: 0, score: 0 };
        RightPlayerRect = { x: 900, y: 600, width: 100, height: 200, color: 'blue', speed: 0, score: 0 };
        Grid = { x: 475, y: 400, width: 50, height: 400, color: 'green', speed: 0 };
        BallMoveDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed));
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
    if (GameBall.x <= GameBall.radius && !BallHitLeftWall)
    {
        BallHitLeftWall = true;
        BallHitRightWall = false;
        BallHitRoof = false;
        GameBall.XSpeed = -GameBall.XSpeed;
    }
    else if (GameBall.y <= GameBall.radius)
    {
        BallHitLeftWall = false;
        BallHitRightWall = false;
        BallHitRoof = true;
        GameBall.YSpeed = -GameBall.YSpeed;
    }
    else if (GameBall.x >= GameCanvas.width - 50 && !BallHitRightWall)
    {
        BallHitLeftWall = false;
        BallHitRightWall = true;
        BallHitRoof = false;
        GameBall.XSpeed = -GameBall.XSpeed;
    }
    else if (GameBall.y >= GameCanvas.height - 50)
    {
        if (GameBall.x < Grid.x) {
            RightPlayerRect.score += 1;
        }
        else {
            LeftPlayerRect.score += 1;
        }
        clearInterval(IntervalForDrawing);
    }
    else
    {
        if (Intersection(LeftPlayerRect) && !BallWasThrownByLeftPlayer)
        {
            BallWasThrownByLeftPlayer = true;
            BallWasThrownByRightPlayer = false;
            BallHitRightWall = false;
            BallHitLeftWall = false;
            BallHitRoof = false;
            console.log(1);
            ChangeSpeedOfGameBall(LeftPlayerRect);
        }
        else if (Intersection(RightPlayerRect) && !BallWasThrownByRightPlayer)
        {
            BallWasThrownByRightPlayer = true;
            BallWasThrownByLeftPlayer = false;
            BallHitLeftWall = false;
            BallHitRightWall = false;
            BallHitRoof = false;
            console.log(2);
            ChangeSpeedOfGameBall(RightPlayerRect);
        }
        else if (Intersection(Grid))
        {
            BallWasThrownByLeftPlayer = false;
            BallWasThrownByRightPlayer = false;
            BallHitLeftWall = false;
            BallHitRightWall = false;
            BallHitRoof = false;
            console.log(3);
            ChangeSpeedOfGameBall(Grid);
        }
    }
    GameBall.x += GameBall.XSpeed;
    GameBall.y += GameBall.YSpeed;
    GameFieldContext.beginPath();
    GameFieldContext.arc(GameBall.x, GameBall.y, GameBall.radius, 0, 2 * Math.PI);
    GameFieldContext.fillStyle = GameBall.color;
    GameFieldContext.fill();
    GameFieldContext.lineWidth = 1;
    GameFieldContext.stroke();

    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = Grid.color;
    GameFieldContext.fillRect(Grid.x, Grid.y, Grid.width, Grid.height);
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
        RightPlayerRect.x += RightPlayerRect.speed;
    }
    else if (RightPlayerRect.x == BorderMaxXForRightPlayer && RightPlayerRect.speed < 0) {
        RightPlayerRect.x += RightPlayerRect.speed;
    }
    else if ((RightPlayerRect.x > BorderXForRightPlayer) && (RightPlayerRect.x < BorderMaxXForRightPlayer)) {
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
    return RandomSpeed * 2;
}


//Проверка на столкновение мяча с прямоугольниками (игроками и сеткой)
function Intersection(CurrentRectangle)
{
    var distX = Math.abs(GameBall.x - CurrentRectangle.x - CurrentRectangle.width / 2);
    var distY = Math.abs(GameBall.y - CurrentRectangle.y - CurrentRectangle.height / 2);
    if (distX > (CurrentRectangle.width / 2 + GameBall.radius)) { return false; }
    if (distY > (CurrentRectangle.height / 2 + GameBall.radius)) { return false; }
    if (distX <= (CurrentRectangle.width / 2)) { return true; }
    if (distY <= (CurrentRectangle.height / 2)) { return true; }
    var dx = distX - CurrentRectangle.width / 2;
    var dy = distY - CurrentRectangle.height / 2;
    return (dx * dx + dy * dy <= (GameBall.radius * GameBall.radius));
}

function ChangeSpeedOfGameBall(CurrentRectangle)
{
    if ((GameBall.x > CurrentRectangle.x + CurrentRectangle.width) && (GameBall.y < CurrentRectangle.y))
    {
        var XDistanceFromRectangleCenter = GameBall.x - ((CurrentRectangle.width / 2) + CurrentRectangle.x);
        var YDistanceFromRectangleCenter = ((CurrentRectangle.height / 2) + CurrentRectangle.y) - GameBall.y;
        var RealDistanceFromRectangleCenter = Math.sqrt((XDistanceFromRectangleCenter * XDistanceFromRectangleCenter) + (YDistanceFromRectangleCenter * YDistanceFromRectangleCenter));
        var FactorForDistance = BallMoveDistance / RealDistanceFromRectangleCenter;
        GameBall.XSpeed = XDistanceFromRectangleCenter * FactorForDistance;
        GameBall.YSpeed = YDistanceFromRectangleCenter * FactorForDistance;
        GameBall.XSpeed = -GameBall.XSpeed;
        GameBall.YSpeed = -GameBall.YSpeed;
    }
    else if ((GameBall.x < CurrentRectangle.x) && (GameBall.y < CurrentRectangle.y)) {
        var XDistanceFromRectangleCenter = ((CurrentRectangle.width / 2) + CurrentRectangle.x) - GameBall.x;
        var YDistanceFromRectangleCenter = ((CurrentRectangle.height / 2) + CurrentRectangle.y) - GameBall.y;
        var RealDistanceFromRectangleCenter = Math.sqrt((XDistanceFromRectangleCenter * XDistanceFromRectangleCenter) + (YDistanceFromRectangleCenter * YDistanceFromRectangleCenter));
        var FactorForDistance = BallMoveDistance / RealDistanceFromRectangleCenter;
        GameBall.XSpeed = XDistanceFromRectangleCenter * FactorForDistance;
        GameBall.YSpeed = YDistanceFromRectangleCenter * FactorForDistance;
        GameBall.XSpeed = -GameBall.XSpeed;
        GameBall.YSpeed = -GameBall.YSpeed;
    }
    else if ((GameBall.x >= CurrentRectangle.x) && (GameBall.x <= CurrentRectangle.x + CurrentRectangle.width))
    {
        GameBall.YSpeed = -GameBall.YSpeed;
    }
    else if (GameBall.y >= CurrentRectangle.y)
    {
        if (CurrentRectangle == Grid)
        {
            if (GameBall.x < CurrentRectangle.x) {
                RightPlayerRect.score += 1;
            }
            else {
                LeftPlayerRect.score += 1;
            }
            clearInterval(IntervalForDrawing);
        }
        GameBall.XSpeed = -GameBall.XSpeed;
    }
}