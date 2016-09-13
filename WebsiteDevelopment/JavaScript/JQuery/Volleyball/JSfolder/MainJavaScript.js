
var Walls = {LeftWall: 1, RightWall: 2, Grid: 3};
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
var BallHitLeftWall = false;
var BallHitRightWall = false;
var BallHitRoof = false;
var BallWasThrownByLeftPlayer = false;
var BallWasThrownByRightPlayer = false;
var BallMoveDistance;
var ScoreInfoParagraph;
var GameBallYStartSpeed;
var BallStartSpeedFactor = 2;
var AccelerationOfGravity = 0.1;
var AccelerationOfGravityForBall = 0.010;
var PlayerJumpSpeed = -4.1;
var LeftPlayerJumpingWhenJumpKeyPressed = false;
var RightPlayerJumpingWhenJumpKeyPressed = false;
var SlowingBallOnHit = 0.8;

window.onkeydown = KeyDownEventHandler;
window.onkeyup = KeyUpEventHandler;

$(document).ready(
    function ()
    {
        GameCanvas = document.getElementById("MainCanvasForGame");
        GameBall = { x: 500, y: 52, radius: 50, YSpeed: 2, XSpeed: GetRandomSpeedForBall(BallStartSpeedFactor), color: 'red' };
        LeftPlayerRect = { x: 0, y: 600, width: 100, height: 200, color: 'black', XSpeed: 0, YSpeed: 0, score: 0, CanMoveLeft: true, CanMoveUp: true, CanMoveRight: true, JumpKeyDown: false, Jumping: false, CanJump: false };
        RightPlayerRect = { x: 900, y: 600, width: 100, height: 200, color: 'blue', XSpeed: 0, YSpeed: 0, score: 0, CanMoveLeft: true, CanMoveUp: true, CanMoveRight: true, JumpKeyDown: false, Jumping: false, CanJump: false };
        Grid = { x: 475, y: 400, width: 50, height: 400, color: 'green', XSpeed: 0, YSpeed: 0};
        BallMoveDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed));
        GameBallYStartSpeed = GameBall.YSpeed;
        ScoreInfoParagraph = document.getElementById('ScoreInfoParagraph');
        ScoreInfoParagraph.innerText = LeftPlayerRect.score.toString() + " : " + RightPlayerRect.score.toString();
        GameFieldContext = GameCanvas.getContext('2d');
        DrawGameField();
        IntervalForDrawing = setInterval(DrawGameField, 10);
    });

function KeyDownEventHandler(event)
{
    if (event.keyCode == 68)
    {
        if (LeftPlayerRect.CanMoveRight)
        {
            console.log('black left');
            LeftPlayerRect.XSpeed = 1;
        }
        else
        {
            console.log('black left stop');
            LeftPlayerRect.XSpeed = 0;
        }
    }
    else if(event.keyCode == 65)
    {
        if (LeftPlayerRect.CanMoveLeft) {
            LeftPlayerRect.XSpeed = -1;
        }
        else {
            LeftPlayerRect.XSpeed = 0;
        }
    }
    else if (event.keyCode == 39)
    {
        if (RightPlayerRect.CanMoveRight) {
            RightPlayerRect.XSpeed = 1;
        }
        else {
            RightPlayerRect.XSpeed = 0;
        }
    }
    else if (event.keyCode == 37)
    {
        if (RightPlayerRect.CanMoveLeft) {
            RightPlayerRect.XSpeed = -1;
        }
        else {
            RightPlayerRect.XSpeed = 0;
        }
    }
    else if (event.keyCode == 87 && !LeftPlayerRect.JumpKeyDown && (LeftPlayerRect.y < (GameCanvas.height - LeftPlayerRect.height)))
    {
        LeftPlayerRect.JumpKeyDown = true;
        LeftPlayerJumpingWhenJumpKeyPressed = true;
    }
    else if ((event.keyCode == 87 && !LeftPlayerRect.JumpKeyDown) && (LeftPlayerRect.y == (GameCanvas.height - LeftPlayerRect.height)) && !LeftPlayerJumpingWhenJumpKeyPressed) {
        LeftPlayerRect.YSpeed = PlayerJumpSpeed;
        LeftPlayerRect.JumpKeyDown = true;
        LeftPlayerRect.CanJump = true;
    }
    else if (event.keyCode == 38 && !RightPlayerRect.JumpKeyDown && (RightPlayerRect.y < (GameCanvas.height - RightPlayerRect.height))) {
        RightPlayerRect.JumpKeyDown = true;
        RightPlayerJumpingWhenJumpKeyPressed = true;
    }
    else if ((event.keyCode == 38 && !RightPlayerRect.JumpKeyDown) && (RightPlayerRect.y == (GameCanvas.height - RightPlayerRect.height)) && !RightPlayerJumpingWhenJumpKeyPressed) {
        RightPlayerRect.YSpeed = PlayerJumpSpeed;
        RightPlayerRect.JumpKeyDown = true;
        RightPlayerRect.CanJump = true;
    }
}

function KeyUpEventHandler(event)
{
    if (event.keyCode == 68 || event.keyCode == 65) {
        LeftPlayerRect.XSpeed = 0;
    }
    else if (event.keyCode == 39 || event.keyCode == 37) {
        RightPlayerRect.XSpeed = 0;
    }
    else if (event.keyCode == 87)
    {
        LeftPlayerRect.JumpKeyDown = false;
        LeftPlayerJumpingWhenJumpKeyPressed = false;
    }
    else if (event.keyCode == 38) {
        RightPlayerRect.JumpKeyDown = false;
        RightPlayerJumpingWhenJumpKeyPressed = false;
    }
}

function DrawGameField()
{
    var IntersectionWithLeftPlayer = false;
    var IntersectionWithRightPlayer = false;
    //Проверяем мешает ли мяч движению игроков
    if ((GameBall.y >= LeftPlayerRect.y - GameBall.radius) || (GameBall.y >= RightPlayerRect.y - GameBall.radius))
    {
        IntersectionWithLeftPlayer = Intersection(LeftPlayerRect);
        IntersectionWithRightPlayer = Intersection(RightPlayerRect);
        if (IntersectionWithLeftPlayer && (GameBall.x < LeftPlayerRect.x || GameBall.x > LeftPlayerRect.x + LeftPlayerRect.width)) {
            if (GameBall.x > LeftPlayerRect.x)
            {
                LeftPlayerRect.CanMoveRight = false;
                LeftPlayerRect.CanMoveLeft = true;
            }
            else if (GameBall.x < LeftPlayerRect.x)
            {
                LeftPlayerRect.CanMoveRight = true;
                LeftPlayerRect.CanMoveLeft = false;
            }
            if ((GameBall.x < LeftPlayerRect.x) && ((LeftPlayerRect.x - GameBall.x) < GameBall.radius))
            {
                LeftPlayerRect.CanMoveUp = false;
            }
            else if ((GameBall.x > LeftPlayerRect.x) && ((GameBall.x - LeftPlayerRect.x) < (GameBall.radius + LeftPlayerRect.width))) {
                LeftPlayerRect.CanMoveUp = false;
            }
            else
            {
                LeftPlayerRect.CanMoveUp = true;
            }
        }
        else
        {
            LeftPlayerRect.CanMoveLeft = true;
            LeftPlayerRect.CanMoveRight = true;
            if (IntersectionWithLeftPlayer)
            {
                LeftPlayerRect.CanMoveUp = false;
            }
            else
            {
                LeftPlayerRect.CanMoveUp = true;
            }
        }
        if (IntersectionWithRightPlayer && (GameBall.x < RightPlayerRect.x || GameBall.x > RightPlayerRect.x + RightPlayerRect.width)) {
            if (GameBall.x > RightPlayerRect.x) {
                RightPlayerRect.CanMoveRight = false;
                RightPlayerRect.CanMoveLeft = true;
            }
            else if (GameBall.x < RightPlayerRect.x) {
                RightPlayerRect.CanMoveRight = true;
                RightPlayerRect.CanMoveLeft = false;
            }
            if ((GameBall.x < RightPlayerRect.x) && ((RightPlayerRect.x - GameBall.x) < GameBall.radius)) {
                RightPlayerRect.CanMoveUp = false;
            }
            else if ((GameBall.x > RightPlayerRect.x) && ((GameBall.x - RightPlayerRect.x) < (GameBall.radius + RightPlayerRect.width))) {
                RightPlayerRect.CanMoveUp = false;
            }
            else {
                RightPlayerRect.CanMoveUp = true;
            }
        }
        else
        {
            RightPlayerRect.CanMoveLeft = true;
            RightPlayerRect.CanMoveRight = true;
            if (IntersectionWithRightPlayer) {
                RightPlayerRect.CanMoveUp = false;
            }
            else
            {
                RightPlayerRect.CanMoveUp = true;
            }
        }
    }
    else
    {
        LeftPlayerRect.CanMoveLeft = true;
        LeftPlayerRect.CanMoveRight = true;
        LeftPlayerRect.CanMoveUp = true;
        RightPlayerRect.CanMoveLeft = true;
        RightPlayerRect.CanMoveRight = true;
        RightPlayerRect.CanMoveUp = true;
    }
    //Очищаем игровое поле
    GameFieldContext.beginPath();
    GameFieldContext.clearRect(0, 0, GameCanvas.width, GameCanvas.height);
    GameFieldContext.stroke();
    //Рисуем мяч 
    if (GameBall.x <= GameBall.radius && !BallHitLeftWall)
    {
        BallWasThrownByLeftPlayer = false;
        BallWasThrownByRightPlayer = false;
        BallHitLeftWall = true;
        BallHitRightWall = false;
        BallHitRoof = false;
        GameBall.XSpeed = -GameBall.XSpeed * SlowingBallOnHit;
    }
    else if (GameBall.y <= GameBall.radius && !BallHitRoof)
    {
        BallWasThrownByLeftPlayer = false;
        BallWasThrownByRightPlayer = false;
        BallHitLeftWall = false;
        BallHitRightWall = false;
        BallHitRoof = true;
        GameBall.YSpeed = -GameBall.YSpeed * SlowingBallOnHit;
    }
    else if ((GameBall.x >= (GameCanvas.width - GameBall.radius)) && !BallHitRightWall)
    {
        BallWasThrownByLeftPlayer = false;
        BallWasThrownByRightPlayer = false;
        BallHitLeftWall = false;
        BallHitRightWall = true;
        BallHitRoof = false;
        GameBall.XSpeed = -GameBall.XSpeed * SlowingBallOnHit;
    }
    else if (GameBall.y >= GameCanvas.height - GameBall.radius)
    {
        if (GameBall.x < Grid.x) {
            RightPlayerRect.score += 1;
        }
        else {
            LeftPlayerRect.score += 1;
        }
        clearInterval(IntervalForDrawing);
        UpdateScoreInfo();
        StartNewRound();
    }
    else
    {
        if (IntersectionWithLeftPlayer && !BallWasThrownByLeftPlayer) {
            BallWasThrownByLeftPlayer = true;
            BallWasThrownByRightPlayer = false;
            BallHitRightWall = false;
            BallHitLeftWall = false;
            BallHitRoof = false;
            ChangeSpeedOfGameBall(LeftPlayerRect);
            if (!LeftPlayerRect.CanMoveRight)
            {
                if (LeftPlayerRect.XSpeed > 0)
                {
                    LeftPlayerRect.XSpeed = 0;
                }
            }
            else if (!LeftPlayerRect.CanMoveLeft)
            {
                if (LeftPlayerRect.XSpeed < 0) {
                    LeftPlayerRect.XSpeed = 0;
                }
            }
        }
        else if (IntersectionWithRightPlayer && !BallWasThrownByRightPlayer) {
            BallWasThrownByLeftPlayer = false;
            BallWasThrownByRightPlayer = true;
            BallHitLeftWall = false;
            BallHitRightWall = false;
            BallHitRoof = false;
            ChangeSpeedOfGameBall(RightPlayerRect);
            if (!RightPlayerRect.CanMoveRight) {
                if (RightPlayerRect.XSpeed > 0) {
                    RightPlayerRect.XSpeed = 0;
                }
            }
            else if (!RightPlayerRect.CanMoveLeft) {
                if (RightPlayerRect.XSpeed < 0) {
                    RightPlayerRect.XSpeed = 0;
                }
            }
        }
        else if (Intersection(Grid)) {
            BallWasThrownByLeftPlayer = false;
            BallWasThrownByRightPlayer = false;
            BallHitLeftWall = false;
            BallHitRightWall = false;
            BallHitRoof = false;
            ChangeSpeedOfGameBall(Grid);
        }
        else
        {
            BallWasThrownByLeftPlayer = false;
            BallWasThrownByRightPlayer = false;
            BallHitLeftWall = false;
            BallHitRightWall = false;
            BallHitRoof = false;
        }
    }
    GameBall.YSpeed += AccelerationOfGravityForBall;
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
    //Обработка движения левого игрока при прыжке
    if (LeftPlayerRect.CanJump)
    {
        if (!LeftPlayerRect.Jumping)
        {
            if (!LeftPlayerRect.CanMoveUp)
            {
                LeftPlayerRect.y = GameCanvas.height - LeftPlayerRect.height;
                LeftPlayerRect.YSpeed = 0;
                LeftPlayerRect.CanJump = false;
            }
            else
            {
                LeftPlayerRect.YSpeed += AccelerationOfGravity;
                LeftPlayerRect.y += LeftPlayerRect.YSpeed;
                LeftPlayerRect.Jumping = true;
            }
        }
        else if (LeftPlayerRect.y >= (GameCanvas.height - LeftPlayerRect.height)) {
            LeftPlayerRect.y = GameCanvas.height - LeftPlayerRect.height;
            LeftPlayerRect.YSpeed = 0;
            LeftPlayerRect.Jumping = false;
            LeftPlayerRect.CanJump = false;
        }
        else
        {
            if (!LeftPlayerRect.CanMoveUp)
            {
                if (LeftPlayerRect.YSpeed < 0)
                {
                    LeftPlayerRect.YSpeed = -LeftPlayerRect.YSpeed;
                }
            }
            LeftPlayerRect.YSpeed += AccelerationOfGravity;
            LeftPlayerRect.y += LeftPlayerRect.YSpeed;
        }
    }
    if (LeftPlayerRect.x == 0 && LeftPlayerRect.XSpeed > 0)
    {
        LeftPlayerRect.x += LeftPlayerRect.XSpeed;
    }
    else if (LeftPlayerRect.x == BorderXforLeftPlayer && LeftPlayerRect.XSpeed < 0)
    {
        LeftPlayerRect.x += LeftPlayerRect.XSpeed;
    }
    else if ((LeftPlayerRect.x < BorderXforLeftPlayer) && LeftPlayerRect.x > 0)
    {
        LeftPlayerRect.x += LeftPlayerRect.XSpeed;
    }
    GameFieldContext.fillRect(LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
    GameFieldContext.stroke();
    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = RightPlayerRect.color;
    //Обработка движения правого игрока при прыжке
    if (RightPlayerRect.CanJump) {
        if (!RightPlayerRect.Jumping) {
            if (!RightPlayerRect.CanMoveUp) {
                RightPlayerRect.y = GameCanvas.height - RightPlayerRect.height;
                RightPlayerRect.YSpeed = 0;
                RightPlayerRect.CanJump = false;
            }
            else {
                RightPlayerRect.YSpeed += AccelerationOfGravity;
                RightPlayerRect.y += RightPlayerRect.YSpeed;
                RightPlayerRect.Jumping = true;
            }
        }
        else if (RightPlayerRect.y >= (GameCanvas.height - RightPlayerRect.height)) {
            RightPlayerRect.y = GameCanvas.height - RightPlayerRect.height;
            RightPlayerRect.YSpeed = 0;
            RightPlayerRect.Jumping = false;
            RightPlayerRect.CanJump = false;
        }
        else {
            if (!RightPlayerRect.CanMoveUp) {
                if (RightPlayerRect.YSpeed < 0) {
                    RightPlayerRect.YSpeed = -RightPlayerRect.YSpeed;
                }
            }
            RightPlayerRect.YSpeed += AccelerationOfGravity;
            RightPlayerRect.y += RightPlayerRect.YSpeed;
        }
    }
    if (RightPlayerRect.x == BorderXForRightPlayer && RightPlayerRect.XSpeed > 0) {
        RightPlayerRect.x += RightPlayerRect.XSpeed;
    }
    else if (RightPlayerRect.x == BorderMaxXForRightPlayer && RightPlayerRect.XSpeed < 0) {
        RightPlayerRect.x += RightPlayerRect.XSpeed;
    }
    else if ((RightPlayerRect.x > BorderXForRightPlayer) && (RightPlayerRect.x < BorderMaxXForRightPlayer)) {
        RightPlayerRect.x += RightPlayerRect.XSpeed;
    }
    GameFieldContext.fillRect(RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
    GameFieldContext.stroke();
}

function GetRandomSpeedForBall(CurrentSpeedFactor)
{
    var RandomSpeed = 0;
    var min = -1;
    var max = 2;
    while (RandomSpeed == 0)
    {
        RandomSpeed = Math.floor(Math.random() * (max - min)) + min;
    }
    return RandomSpeed *= CurrentSpeedFactor;
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
        var FactorForDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed)) / RealDistanceFromRectangleCenter;
        if (CurrentRectangle.XSpeed > 0)
        {
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) + CurrentRectangle.XSpeed;
        }
        else
        {
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
        }
        if (CurrentRectangle.YSpeed < 0)
        {
            GameBall.YSpeed = (YDistanceFromRectangleCenter * FactorForDistance) + Math.abs(CurrentRectangle.YSpeed);
        }
        else
        {
            GameBall.YSpeed = (YDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
        }
        if (GameBall.YSpeed > 0)
        {
            GameBall.YSpeed = -GameBall.YSpeed;
        }
    }
    else if ((GameBall.x < CurrentRectangle.x) && (GameBall.y < CurrentRectangle.y)) {
        var XDistanceFromRectangleCenter = ((CurrentRectangle.width / 2) + CurrentRectangle.x) - GameBall.x;
        var YDistanceFromRectangleCenter = ((CurrentRectangle.height / 2) + CurrentRectangle.y) - GameBall.y;
        var RealDistanceFromRectangleCenter = Math.sqrt((XDistanceFromRectangleCenter * XDistanceFromRectangleCenter) + (YDistanceFromRectangleCenter * YDistanceFromRectangleCenter));
        var FactorForDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed)) / RealDistanceFromRectangleCenter;
        if (CurrentRectangle.XSpeed < 0) {
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) + Math.abs(CurrentRectangle.XSpeed);
        }
        else {
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
        }
        if (CurrentRectangle.YSpeed < 0) {
            GameBall.YSpeed = (YDistanceFromRectangleCenter * FactorForDistance) + Math.abs(CurrentRectangle.YSpeed);
        }
        else {
            GameBall.YSpeed = (YDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
        }
        if (GameBall.XSpeed > 0)
        {
            GameBall.XSpeed = -GameBall.XSpeed;
        }
        if (GameBall.YSpeed > 0)
        {
            GameBall.YSpeed = -GameBall.YSpeed;
        }
    }
    else if (GameBall.y >= CurrentRectangle.y)
    {
        if (GameBall.XSpeed <= 0 && GameBall.x < CurrentRectangle.x)
        {
            var XDistanceFromRectangleCenter = ((CurrentRectangle.width / 2) + CurrentRectangle.x) - GameBall.x;
            var YDistanceFromRectangleCenter = ((CurrentRectangle.height / 2) + CurrentRectangle.y) - GameBall.y;
            var RealDistanceFromRectangleCenter = Math.sqrt((XDistanceFromRectangleCenter * XDistanceFromRectangleCenter) + (YDistanceFromRectangleCenter * YDistanceFromRectangleCenter));
            var FactorForDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed)) / RealDistanceFromRectangleCenter;
            //Защита от бесконечного движения мяча по горизонтали
            if (YDistanceFromRectangleCenter == 0)
            {
                YDistanceFromRectangleCenter = 1;
            }
            if (CurrentRectangle.XSpeed < 0)
            {
                GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) + Math.abs(CurrentRectangle.XSpeed);
            }
            else
            {
                GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
            }
            GameBall.YSpeed = (YDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
            GameBall.XSpeed = -GameBall.XSpeed;
            GameBall.YSpeed = -GameBall.YSpeed;
        }
        else if (GameBall.XSpeed >= 0 && GameBall.x > CurrentRectangle.x)
        {
            var XDistanceFromRectangleCenter = GameBall.x - ((CurrentRectangle.width / 2) + CurrentRectangle.x);
            var YDistanceFromRectangleCenter = ((CurrentRectangle.height / 2) + CurrentRectangle.y) - GameBall.y;
            var RealDistanceFromRectangleCenter = Math.sqrt((XDistanceFromRectangleCenter * XDistanceFromRectangleCenter) + (YDistanceFromRectangleCenter * YDistanceFromRectangleCenter));
            var FactorForDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed)) / RealDistanceFromRectangleCenter;
            //Защита от бесконечного движения мяча по горизонтали
            if (YDistanceFromRectangleCenter == 0)
            {
                YDistanceFromRectangleCenter = 1;
            }
            if (CurrentRectangle.XSpeed > 0) {
                GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) + Math.abs(CurrentRectangle.XSpeed);
            }
            else {
                GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
            }
            GameBall.YSpeed = (YDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
            GameBall.YSpeed = -GameBall.YSpeed;
        }
        else
        {
            if (CurrentRectangle.XSpeed > 0 && GameBall.x > CurrentRectangle.x) {
                GameBall.XSpeed = Math.abs(GameBall.XSpeed) + Math.abs(CurrentRectangle.XSpeed);
            }
            else if (CurrentRectangle.XSpeed < 0 && GameBall.x < CurrentRectangle.x)
            {
                GameBall.XSpeed = -(Math.abs(GameBall.XSpeed) + Math.abs(CurrentRectangle.XSpeed));
            }
            else {
                GameBall.XSpeed = -GameBall.XSpeed * SlowingBallOnHit;
            }
        }
    }
    else
    {
        if (CurrentRectangle.YSpeed < 0) {
            GameBall.YSpeed = -(Math.abs(GameBall.YSpeed) + Math.abs(CurrentRectangle.YSpeed));
        }
        else {
            GameBall.YSpeed = -GameBall.YSpeed * SlowingBallOnHit;
        }
    }
}

function UpdateScoreInfo()
{
    ScoreInfoParagraph.innerText = LeftPlayerRect.score.toString() + " : " + RightPlayerRect.score.toString();
}

function StartNewRound()
{
    GameBall.x = GameCanvas.width / 2;
    GameBall.y = GameBall.radius + 2;
    GameBall.YSpeed = GameBallYStartSpeed;
    GameBall.XSpeed = GetRandomSpeedForBall(BallStartSpeedFactor);
    LeftPlayerRect.x = 0;
    LeftPlayerRect.y = GameCanvas.height - LeftPlayerRect.height;
    LeftPlayerRect.YSpeed = 0;
    LeftPlayerRect.JumpKeyDown = false;
    LeftPlayerRect.Jumping = false;
    LeftPlayerRect.CanJump = false;
    RightPlayerRect.x = GameCanvas.width - RightPlayerRect.width;
    RightPlayerRect.y = GameCanvas.height - RightPlayerRect.height;
    RightPlayerRect.YSpeed = 0;
    RightPlayerRect.JumpKeyDown = false;
    RightPlayerRect.Jumping = false;
    RightPlayerRect.CanJump = false;
    BallHitLeftWall = false;
    BallHitRightWall = false;
    BallHitRoof = false;
    BallWasThrownByLeftPlayer = false;
    BallWasThrownByRightPlayer = false;
    LeftPlayerJumpingWhenJumpKeyPressed = false;
    RightPlayerJumpingWhenJumpKeyPressed = false;
    IntervalForDrawing = setInterval(DrawGameField, 10);
    console.log('start');
}