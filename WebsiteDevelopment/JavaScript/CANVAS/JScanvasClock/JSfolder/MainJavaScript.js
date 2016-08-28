
window.onload = StartClock;

var IntervalForClockFunction;
var CanvasWidth;
var CanvasHeight;
var ContextOfCanvasWithClock;
var HoursForClock;
var MinutesForClock;
var SecondsForClock;

function StartClock()
{
    var CurrentDateTime = new Date();
    HoursForClock = CurrentDateTime.getHours() % 12;
    MinutesForClock = CurrentDateTime.getMinutes();
    SecondsForClock = CurrentDateTime.getSeconds();
    var CanvasForClock = document.getElementById('CanvasForClock');
    CanvasWidth = CanvasForClock.width;
    CanvasHeight = CanvasForClock.height;
    ContextOfCanvasWithClock = CanvasForClock.getContext("2d");
    IntervalForClockFunction = setInterval(AnimateClock, 1000);
}

function AnimateClock()
{
    ContextOfCanvasWithClock.beginPath();
    //Очищаем Canvas
    ContextOfCanvasWithClock.clearRect(0, 0, CanvasWidth, CanvasHeight);
    //Рисуем круг
    ContextOfCanvasWithClock.strokeStyle = 'blue';
    ContextOfCanvasWithClock.fillStyle = 'blue';
    ContextOfCanvasWithClock.arc(400, 400, 350, 1.0 * Math.PI, 3.0 * Math.PI);
    ContextOfCanvasWithClock.font = "30px Arial";
    ContextOfCanvasWithClock.translate(400, 400);
    ContextOfCanvasWithClock.textAlign = "center";
    ContextOfCanvasWithClock.textBaseline = "middle";
    var AngleForRotation;
    //Рисуем числа
    for (var ClockHour = 12; ClockHour > 0; --ClockHour)
    {
        AngleForRotation = (12 - ClockHour) * 30;
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.translate(0, -320);
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.fillText(ClockHour.toString(), 0, 0);
        ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
        ContextOfCanvasWithClock.translate(0, 320);
        ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    }
    ContextOfCanvasWithClock.lineWidth = 10;
    ContextOfCanvasWithClock.stroke();

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
        console.log('Cycle');
    }
    ContextOfCanvasWithClock.lineWidth = 2;
    ContextOfCanvasWithClock.stroke();
    //Вычисляем время
    ++SecondsForClock;
    if (SecondsForClock > 60) {
        SecondsForClock = 1;
        ++MinutesForClock;
        if (MinutesForClock > 60) {
            MinutesForClock = 1;
            ++HoursForClock;
            if (HoursForClock > 12) {
                HoursForClock = 1;
            }
        }
    }
    //Рисуем стрелки часов:
    //Часовая стрелка
    ContextOfCanvasWithClock.beginPath();
    AngleForRotation = HoursForClock * 30;
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.moveTo(0, 0);
    ContextOfCanvasWithClock.lineTo(0, -345);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 15;
    ContextOfCanvasWithClock.stroke();
    //Минутная стрелка
    ContextOfCanvasWithClock.beginPath();
    AngleForRotation = MinutesForClock * 6;
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.moveTo(0, 0);
    ContextOfCanvasWithClock.lineTo(0, -345);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 10;
    ContextOfCanvasWithClock.stroke();
    //Секундная стрелка
    ContextOfCanvasWithClock.beginPath();
    AngleForRotation = SecondsForClock * 6;
    ContextOfCanvasWithClock.rotate(AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.moveTo(0, 0);
    ContextOfCanvasWithClock.lineTo(0, -345);
    ContextOfCanvasWithClock.rotate(-AngleForRotation * Math.PI / 180);
    ContextOfCanvasWithClock.lineWidth = 5;
    ContextOfCanvasWithClock.stroke();

    //Перемещаем нулевые координаты в положение по умолчанию (левый верхний угол canvas)
    ContextOfCanvasWithClock.translate(-400, -400);
    //clearInterval(IntervalForClockFunction);
}