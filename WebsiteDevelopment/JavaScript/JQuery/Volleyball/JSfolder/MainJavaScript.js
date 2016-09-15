
var GameBallPositionsOnPlayerHit = { OnLeftCorner: 0, OnTop: 1, OnRightCorner: 2 };
var XCenterOfRightArea;
var SpeedForOpacityAnimation = 1000;
var GameFieldDrawingFrequency = 10;
var PlayerWalkSpeed = 1;
var AiDecisions = { MoveRight: 1, MoveLeft: 2, Jump: 3, Stand: 4, ChangeNothing: 0 };
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
var AiOff = true;
var GamePauseOff = true;
var PauseButtonText = 'пауза';
var PauseButtonContinueText = 'продолжить';
var ScoreInfoParagraphXRotationDegree = 0;
var IntervalForUpdateScoreInfo = null;
var RotateScoreForward = true;
var LeftPlayerScoreParagraph;
var RightPlayerScoreParagraph;
var GameBallImage;
var GameBallRotationAngleDegrees = 0;
var GameBallImageShift;
//Min rotation speed for game ball
var GameBallMinRotationSpeed = 1;
//Max rotation speed for game ball
var GameBallMaxRotationSpeed = 31;
//Параметры ии
var MaxNRandomNumberForAi;
var MinRandomNumberForAi = 0;
var AiWithJumpJumpMin = 0;
var AiWithJumpJumpMax;
var AiWithJumpMoveLeftMin;
var AiWithJumpMoveLeftMax;
var AiWithJumpMoveRightMin;
var AiWithJumpMoveRightMax;
var AiMoveLeftMin = 0;
var AiMoveLeftMax;
var AiMoveRightMin;
var AiMoveRightMax;
var AiTryingToCatchBallByLeftCorner = false;

var RotationChangingPerFrame = 0.01;
var LeftPlayerWalkingAnimationImages = new Array();
var RightPlayerWalkingAnimationImages = new Array();
var LeftPlayerThrowBallImages = new Array();
var RightPlayerThrowBallImages = new Array();
var AmountOfWalkingImages = 11;
var PlayerAnimationCounter = 6;
var PlayerThrowBallImageTimerMax = 12;

window.onkeydown = KeyDownEventHandler;
window.onkeyup = KeyUpEventHandler;

$(document).ready(
    function ()
    {
        LoadImages();
        GameBallImage = new Image();
        GameBallImage.src = 'Images/GameBall.png';
        LeftPlayerScoreParagraph = document.getElementById('PLeftPlayerScore');
        LeftPlayerScoreParagraph.Rotation = false;
        RightPlayerScoreParagraph = document.getElementById('PRightPlayerScore');
        RightPlayerScoreParagraph.Rotation = false;
        $('#PlayerVsPlayerButton').click(function () {
            AiOff = true;
            $('#PGameModeSelection').animate({ opacity: '0.0' }, SpeedForOpacityAnimation);
            $('#PlayerVsPlayerButton').animate({ opacity: '0.0' }, SpeedForOpacityAnimation);
            $('#PlayerVsAiButton').animate({ opacity: '0.0' }, SpeedForOpacityAnimation, HideElementsForeModeSelection);
            StartNewGame();
        });
        $('#PlayerVsAiButton').click(function () {
            AiOff = false;
            $('#PGameModeSelection').animate({ opacity: '0.0' }, SpeedForOpacityAnimation);
            $('#PlayerVsPlayerButton').animate({ opacity: '0.0' }, SpeedForOpacityAnimation);
            $('#PlayerVsAiButton').animate({ opacity: '0.0' }, SpeedForOpacityAnimation, HideElementsForeModeSelection);
            StartNewGame();
        });
        $('#GameFinishButton').click(function () {
            FinishCurrentGame();
            ShowElementsForeModeSelection();
        });
        $('#GamePauseButton').click(function () {
            if (GamePauseOff)
            {
                GamePauseOff = false;
                $(this).val(PauseButtonContinueText);
                clearInterval(IntervalForDrawing);
            }
            else
            {
                GamePauseOff = true;
                $(this).val(PauseButtonText);
                IntervalForDrawing = setInterval(DrawGameField, GameFieldDrawingFrequency);
            }
        });
    });

function StartNewGame()
{
    GameCanvas = document.getElementById("MainCanvasForGame");
    GameBall = { x: 500, y: 52, radius: 50, YSpeed: 2, XSpeed: GetRandomSpeedForBall(BallStartSpeedFactor), color: 'red', RotateForward: true, RotationSpeed: GameBallMinRotationSpeed };
    LeftPlayerRect = {
        BallOnTheTopImage: null, JumpImage: null, GameBallPosition: -1, StandAnimationBallOnTheRight: null, StandAnimationBallOnTheLeft: null, ThrowBallImageTimer: 0, StandImage: null, WalkFrameNumber: 0,
        AnimationCounter: 0, CurrentAnimationFrame: LeftPlayerWalkingAnimationImages[0], x: 0, y: 600, width: 100, height: 200, color: 'black', XSpeed: 0, YSpeed: 0, score: 0, CanMoveLeft: true, CanMoveUp: true,
        CanMoveRight: true, JumpKeyDown: false, Jumping: false, CanJump: false
    };
    RightPlayerRect = {
        BallOnTheTopImage: null, JumpImage: null, GameBallPosition: -1, StandAnimationBallOnTheRight: null, StandAnimationBallOnTheLeft: null, ThrowBallImageTimer: 0, StandImage: null, WalkFrameNumber: 0,
        AnimationCounter: 0, CurrentAnimationFrame: RightPlayerWalkingAnimationImages[0], x: 900, y: 600, width: 100, height: 200, color: 'blue', XSpeed: 0, YSpeed: 0, score: 0, CanMoveLeft: true, CanMoveUp: true,
        CanMoveRight: true, JumpKeyDown: false, Jumping: false, CanJump: false
    };

    LeftPlayerRect.StandAnimationBallOnTheRight = new Image();
    LeftPlayerRect.StandAnimationBallOnTheRight.src = 'Images/StandAnimations/Black/BallOnTheRight.png';
    LeftPlayerRect.StandAnimationBallOnTheLeft = new Image();
    LeftPlayerRect.StandAnimationBallOnTheLeft.src = 'Images/StandAnimations/Black/BallOnTheLeft.png';
    LeftPlayerRect.JumpImage = new Image();
    LeftPlayerRect.JumpImage.src = 'Images/Jumping/Black/OnJump.png';
    LeftPlayerRect.BallOnTheTopImage = new Image();
    LeftPlayerRect.BallOnTheTopImage.src = 'Images/StandAnimations/Black/BallOnTheTop.png';

    RightPlayerRect.StandAnimationBallOnTheRight = new Image();
    RightPlayerRect.StandAnimationBallOnTheRight.src = 'Images/StandAnimations/Blue/BallOnTheRight.png';
    RightPlayerRect.StandAnimationBallOnTheLeft = new Image();
    RightPlayerRect.StandAnimationBallOnTheLeft.src = 'Images/StandAnimations/Blue/BallOnTheLeft.png';
    RightPlayerRect.JumpImage = new Image();
    RightPlayerRect.JumpImage.src = 'Images/Jumping/Blue/OnJump.png';
    RightPlayerRect.BallOnTheTopImage = new Image();
    RightPlayerRect.BallOnTheTopImage.src = 'Images/StandAnimations/Blue/BallOnTheTop.png';

    Grid = { x: 475, y: 400, width: 50, height: 400, color: 'green', XSpeed: 0, YSpeed: 0 };
    BallMoveDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed));
    GameBallYStartSpeed = GameBall.YSpeed;
    ScoreInfoParagraph = document.getElementById('ScoreInfoParagraph');
    ChangeScore();
    GameFieldContext = GameCanvas.getContext('2d');
    GameBallImageShift = Math.sqrt((GameBall.radius * GameBall.radius) + (GameBall.radius * GameBall.radius));
    MaxNRandomNumberForAi = GameCanvas.width / 2 - Grid.width / 2 - RightPlayerRect.width;
    AiWithJumpJumpMax = MaxNRandomNumberForAi / 100;
    AiWithJumpMoveLeftMin = (MaxNRandomNumberForAi / 100) + 1;
    AiWithJumpMoveLeftMax = ((MaxNRandomNumberForAi / 100) * 2) + 1;
    AiWithJumpMoveRightMin = ((MaxNRandomNumberForAi / 100) * 2) + 2;
    AiWithJumpMoveRightMax = ((MaxNRandomNumberForAi / 100) * 3) + 2;
    AiMoveLeftMax = MaxNRandomNumberForAi / 60;
    AiMoveRightMin = (MaxNRandomNumberForAi / 60) + 1;
    AiMoveRightMax = ((MaxNRandomNumberForAi / 60) * 2) + 1;
    XCenterOfRightArea = Math.floor((GameCanvas.width/2 + Grid.width/2) + ((GameCanvas.width - (GameCanvas.width/2 + Grid.width/2))/2));
    DrawGameField();
    IntervalForDrawing = setInterval(DrawGameField, GameFieldDrawingFrequency);
}

function FinishCurrentGame()
{
    clearInterval(IntervalForDrawing);
    GameBall.x = GameCanvas.width / 2;
    GameBall.y = GameBall.radius + 2;
    GameBall.YSpeed = GameBallYStartSpeed;
    GameBall.XSpeed = GetRandomSpeedForBall(BallStartSpeedFactor);
    LeftPlayerRect.score = 0;
    LeftPlayerRect.x = 0;
    LeftPlayerRect.y = GameCanvas.height - LeftPlayerRect.height;
    LeftPlayerRect.YSpeed = 0;
    LeftPlayerRect.JumpKeyDown = false;
    LeftPlayerRect.Jumping = false;
    LeftPlayerRect.CanJump = false;
    RightPlayerRect.score = 0;
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
    UpdateScoreInfo();
    DrawGameField();
}

function HideElementsForeModeSelection()
{
    $('#PGameModeSelection').hide();
    $('#PlayerVsPlayerButton').hide();
    $('#PlayerVsAiButton').hide();
    $('#GameFinishButton').css('display', 'inline-block').animate({ opacity: '1.0' }, SpeedForOpacityAnimation);
    $('#GamePauseButton').css('display', 'inline-block').animate({ opacity: '1.0' }, SpeedForOpacityAnimation);
}

function ShowElementsForeModeSelection() {
    $('#GameFinishButton').animate({ opacity: '0.0' }, SpeedForOpacityAnimation);
    $('#GamePauseButton').animate({ opacity: '0.0' }, SpeedForOpacityAnimation, HideFinishGameButton);
}

function HideFinishGameButton()
{
    $('#GamePauseButton').css('display', 'none');
    $('#GameFinishButton').css('display', 'none');
    $('#PGameModeSelection').show().animate({ opacity: '1.0' }, SpeedForOpacityAnimation);
    $('#PlayerVsPlayerButton').show().animate({ opacity: '1.0' }, SpeedForOpacityAnimation);
    $('#PlayerVsAiButton').show().animate({ opacity: '1.0' }, SpeedForOpacityAnimation);
}

function KeyDownEventHandler(event)
{
    if (event.keyCode == 68)
    {
        if (LeftPlayerRect.CanMoveRight)
        {
            console.log('black left');
            LeftPlayerRect.XSpeed = PlayerWalkSpeed;
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
            LeftPlayerRect.XSpeed = -PlayerWalkSpeed;
        }
        else {
            LeftPlayerRect.XSpeed = 0;
        }
    }
    else if (event.keyCode == 39 && AiOff)
    {
        if (RightPlayerRect.CanMoveRight) {
            RightPlayerRect.XSpeed = PlayerWalkSpeed;
        }
        else {
            RightPlayerRect.XSpeed = 0;
        }
    }
    else if (event.keyCode == 37 && AiOff)
    {
        if (RightPlayerRect.CanMoveLeft) {
            RightPlayerRect.XSpeed = -PlayerWalkSpeed;
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
    else if (event.keyCode == 38 && !RightPlayerRect.JumpKeyDown && (RightPlayerRect.y < (GameCanvas.height - RightPlayerRect.height)) && AiOff) {
        RightPlayerRect.JumpKeyDown = true;
        RightPlayerJumpingWhenJumpKeyPressed = true;
    }
    else if ((event.keyCode == 38 && !RightPlayerRect.JumpKeyDown) && (RightPlayerRect.y == (GameCanvas.height - RightPlayerRect.height)) && !RightPlayerJumpingWhenJumpKeyPressed && AiOff) {
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
    else if ((event.keyCode == 39 || event.keyCode == 37) && AiOff) {
        RightPlayerRect.XSpeed = 0;
    }
    else if (event.keyCode == 87)
    {
        LeftPlayerRect.JumpKeyDown = false;
        LeftPlayerJumpingWhenJumpKeyPressed = false;
    }
    else if (event.keyCode == 38 && AiOff) {
        RightPlayerRect.JumpKeyDown = false;
        RightPlayerJumpingWhenJumpKeyPressed = false;
    }
}

function DrawGameField()
{
    if (LeftPlayerRect.XSpeed != 0)
    {
        ++LeftPlayerRect.AnimationCounter;
        if (LeftPlayerRect.AnimationCounter == PlayerAnimationCounter) {
            LeftPlayerRect.AnimationCounter = 0;
            if (LeftPlayerRect.XSpeed > 0) {
                if (LeftPlayerRect.WalkFrameNumber < LeftPlayerWalkingAnimationImages.length - 1) {
                    LeftPlayerRect.WalkFrameNumber += 1;
                }
                else {
                    LeftPlayerRect.WalkFrameNumber = 0;
                }
            }
            else if (LeftPlayerRect.XSpeed < 0) {
                if (LeftPlayerRect.WalkFrameNumber > 0)
                {
                    LeftPlayerRect.WalkFrameNumber -= 1;
                }
                else
                {
                    LeftPlayerRect.WalkFrameNumber = LeftPlayerWalkingAnimationImages.length - 1;
                }
            }
        }
    }
    else
    {
        LeftPlayerRect.AnimationCounter = 0;
        LeftPlayerRect.WalkFrameNumber = 0;
    }
    if (RightPlayerRect.XSpeed != 0) {
        ++RightPlayerRect.AnimationCounter;
        if (RightPlayerRect.AnimationCounter == PlayerAnimationCounter) {
            RightPlayerRect.AnimationCounter = 0;
            if (RightPlayerRect.XSpeed > 0) {
                if (RightPlayerRect.WalkFrameNumber > 0) {
                    RightPlayerRect.WalkFrameNumber -= 1;
                }
                else {
                    RightPlayerRect.WalkFrameNumber = RightPlayerWalkingAnimationImages.length - 1;
                }
            }
            else if (RightPlayerRect.XSpeed < 0) {
                if (RightPlayerRect.WalkFrameNumber < RightPlayerWalkingAnimationImages.length - 1) {
                    RightPlayerRect.WalkFrameNumber += 1;
                }
                else {
                    RightPlayerRect.WalkFrameNumber = 0;
                }
            }
        }
    }
    else
    {
        RightPlayerRect.AnimationCounter = 0;
        RightPlayerRect.WalkFrameNumber = 0;
    }

    if (RightPlayerRect.AnimationCounter == PlayerAnimationCounter) {
        RightPlayerRect.AnimationCounter = 0;
        if (RightPlayerRect.WalkFrameNumber < RightPlayerWalkingAnimationImages.length - 1) {
            RightPlayerRect.WalkFrameNumber += 1;
        }
        else {
            RightPlayerRect.WalkFrameNumber = 0;
        }
    }
    if (!AiOff)
    {
        AiDecision();
    }
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
        if (GameBall.YSpeed < 0 && GameBall.RotateForward)
        {
            GameBall.RotateForward = false;
        }
        else if(GameBall.YSpeed > 0 && !GameBall.RotateForward)
        {
            GameBall.RotateForward = true;
        }
        GameBall.XSpeed = -GameBall.XSpeed * SlowingBallOnHit;
    }
    else if (GameBall.y <= GameBall.radius && !BallHitRoof)
    {
        BallWasThrownByLeftPlayer = false;
        BallWasThrownByRightPlayer = false;
        BallHitLeftWall = false;
        BallHitRightWall = false;
        BallHitRoof = true;
        if (GameBall.XSpeed > 0 && GameBall.RotateForward) {
            GameBall.RotateForward = false;
        }
        else if(GameBall.XSpeed < 0 && !GameBall.RotateForward)
        {
            GameBall.RotateForward = true;
        }
        GameBall.YSpeed = -GameBall.YSpeed * SlowingBallOnHit;
    }
    else if ((GameBall.x >= (GameCanvas.width - GameBall.radius)) && !BallHitRightWall)
    {
        BallWasThrownByLeftPlayer = false;
        BallWasThrownByRightPlayer = false;
        BallHitLeftWall = false;
        BallHitRightWall = true;
        BallHitRoof = false;
        if (GameBall.YSpeed > 0 && GameBall.RotateForward) {
            GameBall.RotateForward = false;
        }
        else if (GameBall.YSpeed < 0 && !GameBall.RotateForward) {
            GameBall.RotateForward = true;
        }
        GameBall.XSpeed = -GameBall.XSpeed * SlowingBallOnHit;
    }
    else if (GameBall.y >= GameCanvas.height - GameBall.radius)
    {
        if (GameBall.x < Grid.x) {
            RightPlayerRect.score += 1;
            RightPlayerScoreParagraph.Rotation = true;
        }
        else {
            LeftPlayerRect.score += 1;
            LeftPlayerScoreParagraph.Rotation = true;
        }
        clearInterval(IntervalForDrawing);
        IntervalForUpdateScoreInfo = setInterval(UpdateScoreInfo, 12);
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

    // величина являения
    var derivationMagnitude = 0.00001;
    // угол, на который повернём скрость
    var dAlpha = derivationMagnitude * GameBall.RotationSpeed * GameBall.RotationSpeed;

    GameBall.XSpeed = GameBall.XSpeed * Math.cos(dAlpha) + GameBall.YSpeed * Math.sin(dAlpha);
    GameBall.YSpeed = -GameBall.XSpeed * Math.sin(dAlpha) + GameBall.YSpeed * Math.cos(dAlpha);
    //*********************************************************************************************
    
    GameBall.YSpeed += AccelerationOfGravityForBall;
    GameBall.x += GameBall.XSpeed;
    GameBall.y += GameBall.YSpeed;

    //New drawing of ball
    GameFieldContext.beginPath();
    GameFieldContext.translate(GameBall.x, GameBall.y);
    GameFieldContext.rotate(GameBallRotationAngleDegrees * Math.PI / 180);
    DrawGameBallFromImage();
    GameFieldContext.stroke();

    //Old game ball
    /*GameFieldContext.beginPath();
    GameFieldContext.arc(GameBall.x, GameBall.y, GameBall.radius, 0, 2 * Math.PI);
    GameFieldContext.fillStyle = GameBall.color;
    GameFieldContext.fill();
    GameFieldContext.lineWidth = 1;
    GameFieldContext.stroke();*/

    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = Grid.color;
    GameFieldContext.fillRect(Grid.x, Grid.y, Grid.width, Grid.height);
    GameFieldContext.stroke();
    GameFieldContext.beginPath();
    GameFieldContext.fillStyle = LeftPlayerRect.color;

    if (GameBall.RotateForward)
    {
        if (GameBallRotationAngleDegrees < 360) {
            GameBallRotationAngleDegrees += GameBall.RotationSpeed;
        }
        else {
            GameBallRotationAngleDegrees = 0;
        }
    }
    else
    {
        console.log('rotation minus');
        if (GameBallRotationAngleDegrees > -360) {
            GameBallRotationAngleDegrees -= GameBall.RotationSpeed;
        }
        else {
            GameBallRotationAngleDegrees = 0;
        }
    }
    if (GameBall.RotationSpeed > GameBallMinRotationSpeed)
    {
        GameBall.RotationSpeed /= 1.001;
    }
    else
    {
        GameBall.RotationSpeed = GameBallMinRotationSpeed;
    }

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
    //New drawing of Left player
    if (LeftPlayerRect.YSpeed != 0)
    {
        LeftPlayerRect.ThrowBallImageTimer = 0;
        GameFieldContext.drawImage(LeftPlayerRect.JumpImage, LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
    }
    else if (LeftPlayerRect.ThrowBallImageTimer == 0)
    {
        GameFieldContext.drawImage(LeftPlayerWalkingAnimationImages[LeftPlayerRect.WalkFrameNumber], LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
    }
    else
    {
        --LeftPlayerRect.ThrowBallImageTimer;
        if (LeftPlayerRect.GameBallPosition == GameBallPositionsOnPlayerHit.OnLeftCorner)
        {
            GameFieldContext.drawImage(LeftPlayerRect.StandAnimationBallOnTheLeft, LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
        }
        else if (LeftPlayerRect.GameBallPosition == GameBallPositionsOnPlayerHit.OnRightCorner)
        {
            GameFieldContext.drawImage(LeftPlayerRect.StandAnimationBallOnTheRight, LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
        }
        else
        {
            GameFieldContext.drawImage(LeftPlayerRect.BallOnTheTopImage, LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
        }
    }
    //Old drawing of Left player
    //GameFieldContext.fillRect(LeftPlayerRect.x, LeftPlayerRect.y, LeftPlayerRect.width, LeftPlayerRect.height);
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
   
    //New drawing of Right player
    if (RightPlayerRect.YSpeed != 0) {
        RightPlayerRect.ThrowBallImageTimer = 0;
        GameFieldContext.drawImage(RightPlayerRect.JumpImage, RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
    }
    else if (RightPlayerRect.ThrowBallImageTimer == 0) {
        GameFieldContext.drawImage(RightPlayerWalkingAnimationImages[RightPlayerRect.WalkFrameNumber], RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
    }
    else {
        --RightPlayerRect.ThrowBallImageTimer;
        if (RightPlayerRect.GameBallPosition == GameBallPositionsOnPlayerHit.OnLeftCorner) {
            GameFieldContext.drawImage(RightPlayerRect.StandAnimationBallOnTheLeft, RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
        }
        else if (RightPlayerRect.GameBallPosition == GameBallPositionsOnPlayerHit.OnRightCorner) {
            GameFieldContext.drawImage(RightPlayerRect.StandAnimationBallOnTheRight, RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
        }
        else {
            GameFieldContext.drawImage(RightPlayerRect.BallOnTheTopImage, RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
        }
    }
    //Old drawing of Right player
    //GameFieldContext.fillRect(RightPlayerRect.x, RightPlayerRect.y, RightPlayerRect.width, RightPlayerRect.height);
    GameFieldContext.stroke();
}

function DrawGameBallFromImage()
{
    GameFieldContext.drawImage(GameBallImage, -GameBall.radius, -GameBall.radius, GameBall.radius * 2, GameBall.radius * 2);
    GameFieldContext.rotate(-GameBallRotationAngleDegrees * Math.PI / 180);
    GameFieldContext.translate(-GameBall.x, -GameBall.y);
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
        GameBall.RotationSpeed += Math.abs(GameBall.YSpeed) + Math.abs(GameBall.XSpeed);
        if (CurrentRectangle.XSpeed > 0)
        {
            GameBall.RotationSpeed += CurrentRectangle.XSpeed;
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) + CurrentRectangle.XSpeed;
        }
        else
        {
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
        }
        if (CurrentRectangle.YSpeed < 0)
        {
            GameBall.RotationSpeed += Math.abs(CurrentRectangle.YSpeed);
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
        if (GameBall.RotationSpeed > GameBallMaxRotationSpeed)
        {
            GameBall.RotationSpeed = GameBallMaxRotationSpeed;
        }
        if ( CurrentRectangle != Grid)
        {
            if (!AiOff) {
                ChooseRandomCornerForAi();
            }
            CurrentRectangle.ThrowBallImageTimer = PlayerThrowBallImageTimerMax;
            CurrentRectangle.GameBallPosition = GameBallPositionsOnPlayerHit.OnRightCorner;
        }
    }
    else if ((GameBall.x < CurrentRectangle.x) && (GameBall.y < CurrentRectangle.y)) {
        var XDistanceFromRectangleCenter = ((CurrentRectangle.width / 2) + CurrentRectangle.x) - GameBall.x;
        var YDistanceFromRectangleCenter = ((CurrentRectangle.height / 2) + CurrentRectangle.y) - GameBall.y;
        var RealDistanceFromRectangleCenter = Math.sqrt((XDistanceFromRectangleCenter * XDistanceFromRectangleCenter) + (YDistanceFromRectangleCenter * YDistanceFromRectangleCenter));
        var FactorForDistance = Math.sqrt((Math.abs(GameBall.XSpeed) * Math.abs(GameBall.XSpeed)) + (GameBall.YSpeed * GameBall.YSpeed)) / RealDistanceFromRectangleCenter;
        GameBall.RotationSpeed += Math.abs(GameBall.YSpeed) + Math.abs(GameBall.XSpeed);
        if (CurrentRectangle.XSpeed < 0) {
            GameBall.RotationSpeed += Math.abs(CurrentRectangle.XSpeed);
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) + Math.abs(CurrentRectangle.XSpeed);
        }
        else {
            GameBall.XSpeed = (XDistanceFromRectangleCenter * FactorForDistance) * SlowingBallOnHit;
        }
        if (CurrentRectangle.YSpeed < 0) {
            GameBall.RotationSpeed += Math.abs(CurrentRectangle.YSpeed);
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
        if (GameBall.RotationSpeed > GameBallMaxRotationSpeed) {
            GameBall.RotationSpeed = GameBallMaxRotationSpeed;
        }
        if (CurrentRectangle != Grid) {
            if (!AiOff) {
                ChooseRandomCornerForAi();
            }
            CurrentRectangle.ThrowBallImageTimer = PlayerThrowBallImageTimerMax;
            CurrentRectangle.GameBallPosition = GameBallPositionsOnPlayerHit.OnLeftCorner;
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
            if (!AiOff && CurrentRectangle != Grid) {
                ChooseRandomCornerForAi();
            }
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
            if (!AiOff && CurrentRectangle != Grid) {
                ChooseRandomCornerForAi();
            }
        }
        else
        {
            if (GameBall.x < CurrentRectangle.x)
            {
                if (GameBall.YSpeed > 0 && GameBall.RotateForward) {
                    GameBall.RotateForward = false;
                }
                else if (GameBall.YSpeed < 0 && !GameBall.RotateForward) {
                    GameBall.RotateForward = true;
                }
            }
            else
            {
                if (GameBall.YSpeed < 0 && GameBall.RotateForward) {
                    GameBall.RotateForward = false;
                }
                else if (GameBall.YSpeed > 0 && !GameBall.RotateForward) {
                    GameBall.RotateForward = true;
                }
            }
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
            if (!AiOff && CurrentRectangle != Grid) {
                ChooseRandomCornerForAi();
            }
        }
    }
    else
    {
        if (GameBall.XSpeed < 0 && GameBall.RotateForward) {
            GameBall.RotateForward = false;
        }
        else if(GameBall.XSpeed > 0 && !GameBall.RotateForward)
        {
            GameBall.RotateForward = true;
        }
        if (CurrentRectangle.YSpeed < 0) {
            GameBall.YSpeed = -(Math.abs(GameBall.YSpeed) + Math.abs(CurrentRectangle.YSpeed));
        }
        else {
            GameBall.YSpeed = -GameBall.YSpeed * SlowingBallOnHit;
        }
        if (!AiOff && CurrentRectangle != Grid) {
            ChooseRandomCornerForAi();
        }
        if (CurrentRectangle != Grid)
        {
            CurrentRectangle.ThrowBallImageTimer = PlayerThrowBallImageTimerMax;
            CurrentRectangle.GameBallPosition = GameBallPositionsOnPlayerHit.OnTop;
        }
    }
}

function UpdateScoreInfo()
{
    if (ScoreInfoParagraphXRotationDegree < 90 && RotateScoreForward)
    {
        ++ScoreInfoParagraphXRotationDegree;
    }
    else
    {
        RotateScoreForward = false;
        --ScoreInfoParagraphXRotationDegree;
    }
    if (LeftPlayerScoreParagraph.Rotation) {
        $(LeftPlayerScoreParagraph).css('transform', 'rotatex(' + ScoreInfoParagraphXRotationDegree.toString() + 'deg)');
    }
    else
    {
        $(RightPlayerScoreParagraph).css('transform', 'rotatex(' + ScoreInfoParagraphXRotationDegree.toString() + 'deg)');
    }
    if (ScoreInfoParagraphXRotationDegree == 90)
    {
        ChangeScore();
    }
    if (ScoreInfoParagraphXRotationDegree == -1) {
        $('#ScoreInfoParagraph').css('transform', 'rotatex(' + 0 + 'deg)');
        clearInterval(IntervalForUpdateScoreInfo);
        RotateScoreForward = true;
        LeftPlayerScoreParagraph.Rotation = false;
        RightPlayerScoreParagraph.Rotation = false;
        ScoreInfoParagraphXRotationDegree = 0;
    }
}

function ChangeScore()
{
    LeftPlayerScoreParagraph.innerText = LeftPlayerRect.score.toString();
    RightPlayerScoreParagraph.innerText = RightPlayerRect.score.toString();
    ScoreInfoParagraph.innerText = " : ";
}

function StartNewRound()
{
    GameBall.x = GameCanvas.width / 2;
    GameBall.y = GameBall.radius + 2;
    GameBall.YSpeed = GameBallYStartSpeed;
    GameBall.XSpeed = GetRandomSpeedForBall(BallStartSpeedFactor);
    GameBall.RotationSpeed = GameBallMinRotationSpeed;
    LeftPlayerRect.x = 0;
    LeftPlayerRect.y = GameCanvas.height - LeftPlayerRect.height;
    LeftPlayerRect.YSpeed = 0;
    LeftPlayerRect.JumpKeyDown = false;
    LeftPlayerRect.Jumping = false;
    LeftPlayerRect.CanJump = false;
    LeftPlayerRect.ThrowBallImageTimer = 0;
    RightPlayerRect.x = GameCanvas.width - RightPlayerRect.width;
    RightPlayerRect.y = GameCanvas.height - RightPlayerRect.height;
    RightPlayerRect.YSpeed = 0;
    RightPlayerRect.JumpKeyDown = false;
    RightPlayerRect.Jumping = false;
    RightPlayerRect.CanJump = false;
    RightPlayerRect.ThrowBallImageTimer = 0;
    BallHitLeftWall = false;
    BallHitRightWall = false;
    BallHitRoof = false;
    BallWasThrownByLeftPlayer = false;
    BallWasThrownByRightPlayer = false;
    LeftPlayerJumpingWhenJumpKeyPressed = false;
    RightPlayerJumpingWhenJumpKeyPressed = false;
    IntervalForDrawing = setInterval(DrawGameField, GameFieldDrawingFrequency);
    console.log('start');
}

function AiDecision()
{
    var CurrentDecision;
    if (RightPlayerRect.y == (GameCanvas.height - RightPlayerRect.height))
    {
        CurrentDecision = MakeDecisionForAi(true);
        if (CurrentDecision == AiDecisions.Stand) {
            RightPlayerRect.XSpeed = 0;
        }
        else if (CurrentDecision == AiDecisions.Jump)
        {
            RightPlayerRect.YSpeed = PlayerJumpSpeed;
            RightPlayerRect.CanJump = true;
        }
        else if (CurrentDecision == AiDecisions.MoveLeft) {
            if (RightPlayerRect.x == BorderXForRightPlayer)
            {
                RightPlayerRect.XSpeed = PlayerWalkSpeed;
            }
            else
            {
                RightPlayerRect.XSpeed = -PlayerWalkSpeed;
            }
        }
        else if (CurrentDecision == AiDecisions.MoveRight) {
            if (RightPlayerRect.x == BorderMaxXForRightPlayer) {
                RightPlayerRect.XSpeed = -PlayerWalkSpeed;
            }
            else {
                RightPlayerRect.XSpeed = PlayerWalkSpeed;
            }
        }
    }
    else
    {
        CurrentDecision = MakeDecisionForAi(false);
        if (CurrentDecision == AiDecisions.Stand) {
            RightPlayerRect.XSpeed = 0;
        }
        else if (CurrentDecision == AiDecisions.MoveLeft) {
            if (RightPlayerRect.x == BorderXForRightPlayer) {
                RightPlayerRect.XSpeed = PlayerWalkSpeed;
            }
            else {
                RightPlayerRect.XSpeed = -PlayerWalkSpeed;
            }
        }
        else if (CurrentDecision == AiDecisions.MoveRight) {
            if (RightPlayerRect.x == BorderMaxXForRightPlayer) {
                RightPlayerRect.XSpeed = -PlayerWalkSpeed;
            }
            else {
                RightPlayerRect.XSpeed = PlayerWalkSpeed;
            }
        }
    }
}

function MakeDecisionForAi(CanReturnJumpAsAResult)
{
    if (GameBall.x <= GameCanvas.width/2)
    {
        if (RightPlayerRect.x < XCenterOfRightArea - RightPlayerRect.width / 2)
        {
            return AiDecisions.MoveRight;
        }
        else if (RightPlayerRect.x > XCenterOfRightArea - RightPlayerRect.width / 2) {
            return AiDecisions.MoveLeft;
        }
        else
        {
            return AiDecisions.Stand;
        }
    }
    else
    {
        if (GameBall.y > RightPlayerRect.y - RightPlayerRect.height/2 - GameBall.radius)
        {
            var CurrentRandomNumber = Math.floor(Math.random() * (100 - 0)) + 0;
            if (CurrentRandomNumber == 0)
            {
                return AiDecisions.Jump;
            }
            else if (AiTryingToCatchBallByLeftCorner) {
                if (GameBall.x < RightPlayerRect.x - (GameBall.radius / 2))
                {
                    return AiDecisions.MoveLeft;
                }
                else {
                    return AiDecisions.MoveRight;
                }
            }         
            else {
                if (GameBall.x < RightPlayerRect.x + RightPlayerRect.width + (GameBall.radius / 2)) {
                    return AiDecisions.MoveLeft;
                }
                else {
                    return AiDecisions.MoveRight;
                }
            }
        }
        else if (AiTryingToCatchBallByLeftCorner)
            {
                if (GameBall.x < RightPlayerRect.x - GameBall.radius / 2) {
                    return AiDecisions.MoveLeft;
                }
                else {
                    return AiDecisions.MoveRight;
                }
        }
        else
        {
            if (GameBall.x < RightPlayerRect.x + RightPlayerRect.width + (GameBall.radius / 2)) {
                return AiDecisions.MoveLeft;
            }
            else {
                return AiDecisions.MoveRight;
            }
        }

    }
    /*if (CanReturnJumpAsAResult)
    {
        if (CurrentRandomNumber >= AiWithJumpJumpMin && CurrentRandomNumber <= AiWithJumpJumpMax) {
            return AiDecisions.Jump;
        }
        else if (CurrentRandomNumber >= AiWithJumpMoveLeftMin && CurrentRandomNumber <= AiWithJumpMoveLeftMax) {
            return AiDecisions.MoveLeft;
        }
        else if (CurrentRandomNumber >= AiWithJumpMoveRightMin && CurrentRandomNumber <= AiWithJumpMoveRightMax) {
            return AiDecisions.MoveRight;
        }
        else
        {
            return AiDecisions.ChangeNothing;
        }
    }
    else
    {
        if (CurrentRandomNumber >= AiMoveLeftMin && CurrentRandomNumber <= AiMoveLeftMax) {
            return AiDecisions.MoveLeft;
        }
        else if (CurrentRandomNumber >= AiMoveRightMin && CurrentRandomNumber <= AiMoveRightMax) {
            return AiDecisions.MoveRight;
        }
        else {
            return AiDecisions.ChangeNothing;
        }
    }*/
}

function LoadImages()
{
    var SomeImage;
    //Loading of walk images
    for (var FrameNumber = 0; FrameNumber <= AmountOfWalkingImages; ++FrameNumber)
    {
        SomeImage = new Image();
        SomeImage.src = 'Images/Walking/Black/frame_' + FrameNumber.toString() + '_delay-0.06s.png';
        LeftPlayerWalkingAnimationImages.push(SomeImage);
        SomeImage = new Image();
        SomeImage.src = 'Images/Walking/Blue/frame_' + FrameNumber.toString() + '_delay-0.06s.png';
        RightPlayerWalkingAnimationImages.push(SomeImage);
    }
}

function ChooseRandomCornerForAi()
{
    var CurrentRandomNumber = GetRandomNumber(1, 2);
    if (CurrentRandomNumber == 1)
    {
        AiTryingToCatchBallByLeftCorner = true;
    }
    else
    {
        AiTryingToCatchBallByLeftCorner = false;
    }
}

function GetRandomNumber(MinNumber, MaxNumber)
{
    return Math.floor(Math.random() * ((MaxNumber + 1) - MinNumber)) + MinNumber;
}

