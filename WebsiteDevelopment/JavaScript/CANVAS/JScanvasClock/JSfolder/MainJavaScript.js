
window.onload = StartClock;

var BigClock;

function Clock(NewCanvasWidth, NewCanvasHeight, NewContextForDrawing, NewFullTimeInSecondsForClock) {
    this.IntervalForClockFunction = null;
    var CanvasWidth = NewCanvasWidth;
    var CanvasHeight = NewCanvasHeight;
    var ContextOfCanvasWithClock = NewContextForDrawing;
    var HoursArrowAngleDegreesPerMinute = 0.5;
    var MinutesArrowAngleDegreesPerSecond = 0.1;
    var FullTimeInSecondsForClock = NewFullTimeInSecondsForClock;
    this.AnimateClock = function () {
        //Вычисляем время
        ++FullTimeInSecondsForClock;
        if (FullTimeInSecondsForClock > 43200) {
            FullTimeInSecondsForClock -= 43200;
        }

        //Очищаем Canvas
        ContextOfCanvasWithClock.beginPath();
        ContextOfCanvasWithClock.clearRect(0, 0, CanvasWidth, CanvasHeight);
        ContextOfCanvasWithClock.stroke();

        DrawClockBasics();
        //Рисуем стрелки часов:
        DrawHourHand();
        DrawMinuteHand();
        DrawSecondHand();
        DrawCircleInCenter();

        //Перемещаем нулевые координаты в положение по умолчанию (левый верхний угол canvas)
        ContextOfCanvasWithClock.translate(-400, -400);
        //clearInterval(IntervalForClockFunction);
    };

    var DrawHourHand = function () {
        //Часовая стрелка
        ContextOfCanvasWithClock.beginPath();
        ContextOfCanvasWithClock.strokeStyle = 'black';
        var AngleForRotation = Math.floor((FullTimeInSecondsForClock / 60)) * HoursArrowAngleDegreesPerMinute;
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.moveTo(0, 25);
        ContextOfCanvasWithClock.lineTo(0, -280);
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.lineWidth = 15;
        ContextOfCanvasWithClock.stroke();
    };

    var DrawMinuteHand = function () {
        //Минутная стрелка
        ContextOfCanvasWithClock.beginPath();
        var AngleForRotation = FullTimeInSecondsForClock * MinutesArrowAngleDegreesPerSecond;
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.strokeStyle = 'red';
        ContextOfCanvasWithClock.moveTo(0, 25);
        ContextOfCanvasWithClock.lineTo(0, -320);
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.lineWidth = 10;
        ContextOfCanvasWithClock.stroke();
        ContextOfCanvasWithClock.beginPath();
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.moveTo(0, 25);
        ContextOfCanvasWithClock.lineTo(0, -345);
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.lineWidth = 5;
        ContextOfCanvasWithClock.stroke();
    };

    var DrawSecondHand = function () {
        //Секундная стрелка
        ContextOfCanvasWithClock.beginPath();
        var AngleForRotation = (FullTimeInSecondsForClock % 60) * 6;
        ContextOfCanvasWithClock.strokeStyle = 'green';
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.moveTo(0, 25);
        ContextOfCanvasWithClock.lineTo(0, -345);
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.lineWidth = 5;
        ContextOfCanvasWithClock.stroke();
    };

    var DrawCircleInCenter = function () {
        //Рисуем центральный круг
        ContextOfCanvasWithClock.beginPath();
        ContextOfCanvasWithClock.strokeStyle = 'gray';
        ContextOfCanvasWithClock.fillStyle = 'gray';
        ContextOfCanvasWithClock.arc(0, 0, 15, 0, 2 * Math.PI);
        ContextOfCanvasWithClock.fill();
        ContextOfCanvasWithClock.stroke();
    };

    var DrawClockBasics = function () {
        DrawCircle();
        DrawNumbers();
        DrawLinesForSeconds();
    };

    var DrawCircle = function () {
        //Рисуем круг
        ContextOfCanvasWithClock.beginPath();
        ContextOfCanvasWithClock.strokeStyle = 'blue';
        ContextOfCanvasWithClock.fillStyle = 'blue';
        ContextOfCanvasWithClock.arc(400, 400, 350, 1.0 * Math.PI, 3.0 * Math.PI);
        ContextOfCanvasWithClock.font = "30px Arial";
        ContextOfCanvasWithClock.translate(400, 400);
        ContextOfCanvasWithClock.textAlign = "center";
        ContextOfCanvasWithClock.textBaseline = "middle";
        ContextOfCanvasWithClock.lineWidth = 10;
        ContextOfCanvasWithClock.stroke();
    };

    var DrawNumbers = function () {
        var AngleForRotation;
        //Рисуем числа
        ContextOfCanvasWithClock.beginPath();
        for (var ClockHour = 12; ClockHour > 0; --ClockHour) {
            AngleForRotation = (12 - ClockHour) * 30;
            ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
            ContextOfCanvasWithClock.translate(0, -320);
            ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
            ContextOfCanvasWithClock.fillText(ClockHour.toString(), 0, 0);
            ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
            ContextOfCanvasWithClock.translate(0, 320);
            ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        }
        ContextOfCanvasWithClock.stroke();
    };

    var DrawLinesForSeconds = function () {
        var AngleForRotation;
        ContextOfCanvasWithClock.beginPath();
        //Рисуем отметки для секунд
        for (var ClockMinute = 60; ClockMinute > 0; --ClockMinute) {
            AngleForRotation = (60 - ClockMinute) * 6;
            ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
            ContextOfCanvasWithClock.translate(0, -335);
            ContextOfCanvasWithClock.moveTo(0, 0);
            ContextOfCanvasWithClock.lineTo(0, -15);
            ContextOfCanvasWithClock.translate(0, 335);
            ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        }
        ContextOfCanvasWithClock.lineWidth = 2;
        ContextOfCanvasWithClock.stroke();
    };
}

function StartClock()
{
    var CurrentDateTime = new Date();
    var HoursForClock = CurrentDateTime.getHours() % 12;
    var MinutesForClock = CurrentDateTime.getMinutes();
    var SecondsForClock = CurrentDateTime.getSeconds();
    var CurrentFullTimeInSecondsForClock = SecondsForClock + (MinutesForClock * 60) + (HoursForClock * 3600);
    var CanvasForClock = document.getElementById('CanvasForClock');
    BigClock = new Clock(CanvasForClock.width, CanvasForClock.height, CanvasForClock.getContext("2d"), CurrentFullTimeInSecondsForClock);
    BigClock.IntervalForClockFunction = setInterval(function () { BigClock.AnimateClock() }, 1000);
}

/*function AnimateClock()
{
    //Вычисляем время
    ++FullTimeInSecondsForClock;
    if (FullTimeInSecondsForClock > 43200) {
        FullTimeInSecondsForClock -= 43200;
    }

    //Очищаем Canvas
    ContextOfCanvasWithClock.beginPath();
    ContextOfCanvasWithClock.clearRect(0, 0, CanvasWidth, CanvasHeight);
    ContextOfCanvasWithClock.stroke();

    DrawClockBasics();
    //Рисуем стрелки часов:
    DrawHourHand();
    DrawMinuteHand();
    DrawSecondHand();
    DrawCircleInCenter();

    //Перемещаем нулевые координаты в положение по умолчанию (левый верхний угол canvas)
    ContextOfCanvasWithClock.translate(-400, -400);
    //clearInterval(IntervalForClockFunction);
}

function DrawHourHand()
{
    //Часовая стрелка
    ContextOfCanvasWithClock.beginPath();
    ContextOfCanvasWithClock.strokeStyle = 'black';
    var AngleForRotation = Math.floor((FullTimeInSecondsForClock / 60)) * HoursArrowAngleDegreesPerMinute;
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.moveTo(0, 25);
    ContextOfCanvasWithClock.lineTo(0, -280);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 15;
    ContextOfCanvasWithClock.stroke();
}

function DrawMinuteHand()
{
    //Минутная стрелка
    ContextOfCanvasWithClock.beginPath();
    var AngleForRotation = FullTimeInSecondsForClock * MinutesArrowAngleDegreesPerSecond;
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.strokeStyle = 'red';
    ContextOfCanvasWithClock.moveTo(0, 25);
    ContextOfCanvasWithClock.lineTo(0, -320);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 10;
    ContextOfCanvasWithClock.stroke();
    ContextOfCanvasWithClock.beginPath();
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.moveTo(0, 25);
    ContextOfCanvasWithClock.lineTo(0, -345);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 5;
    ContextOfCanvasWithClock.stroke();
}

function DrawSecondHand()
{
    //Секундная стрелка
    ContextOfCanvasWithClock.beginPath();
    var AngleForRotation = (FullTimeInSecondsForClock % 60) * 6;
    ContextOfCanvasWithClock.strokeStyle = 'green';
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.moveTo(0, 25);
    ContextOfCanvasWithClock.lineTo(0, -345);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 5;
    ContextOfCanvasWithClock.stroke();
}

function DrawCircleInCenter()
{
    //Рисуем центральный круг
    ContextOfCanvasWithClock.beginPath();
    ContextOfCanvasWithClock.strokeStyle = 'gray';
    ContextOfCanvasWithClock.fillStyle = 'gray';
    ContextOfCanvasWithClock.arc(0, 0, 15, 0, 2 * Math.PI);
    ContextOfCanvasWithClock.fill();
    ContextOfCanvasWithClock.stroke();
}

function DrawClockBasics()
{
    DrawCircle();
    DrawNumbers();
    DrawLinesForSeconds();
}

function DrawCircle()
{
    //Рисуем круг
    ContextOfCanvasWithClock.beginPath();
    ContextOfCanvasWithClock.strokeStyle = 'blue';
    ContextOfCanvasWithClock.fillStyle = 'blue';
    ContextOfCanvasWithClock.arc(400, 400, 350, 1.0 * Math.PI, 3.0 * Math.PI);
    ContextOfCanvasWithClock.font = "30px Arial";
    ContextOfCanvasWithClock.translate(400, 400);
    ContextOfCanvasWithClock.textAlign = "center";
    ContextOfCanvasWithClock.textBaseline = "middle";
    ContextOfCanvasWithClock.lineWidth = 10;
    ContextOfCanvasWithClock.stroke();
}

function DrawNumbers()
{
    var AngleForRotation;
    //Рисуем числа
    ContextOfCanvasWithClock.beginPath();
    for (var ClockHour = 12; ClockHour > 0; --ClockHour) {
        AngleForRotation = (12 - ClockHour) * 30;
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.translate(0, -320);
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.fillText(ClockHour.toString(), 0, 0);
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.translate(0, 320);
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    }
    ContextOfCanvasWithClock.stroke();
}

function DrawLinesForSeconds()
{
    var AngleForRotation;
    ContextOfCanvasWithClock.beginPath();
    //Рисуем отметки для секунд
    for (var ClockMinute = 60; ClockMinute > 0; --ClockMinute) {
        AngleForRotation = (60 - ClockMinute) * 6;
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.translate(0, -335);
        ContextOfCanvasWithClock.moveTo(0, 0);
        ContextOfCanvasWithClock.lineTo(0, -15);
        ContextOfCanvasWithClock.translate(0, 335);
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    }
    ContextOfCanvasWithClock.lineWidth = 2;
    ContextOfCanvasWithClock.stroke();
}*/