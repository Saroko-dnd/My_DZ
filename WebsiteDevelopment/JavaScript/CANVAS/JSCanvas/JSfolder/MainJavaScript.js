

window.onload = DrawInCanvas;


function DrawInCanvas()
{
    console.log('start');
    var MainCanvas = document.getElementById('MainCanvasID');
    var Context = MainCanvas.getContext("2d");
    //Рисуем дом
    Context.beginPath();
    Context.moveTo(100, 850);
    Context.lineTo(600, 850);
    Context.lineTo(600, 350);
    Context.lineTo(100, 350);
    Context.lineTo(100, 850);
    Context.moveTo(100, 350);
    Context.lineTo(350, 150);
    Context.lineTo(600, 350);
    Context.moveTo(500, 650);
    Context.lineTo(500, 500);
    Context.lineTo(200, 500);
    Context.lineTo(200, 650);
    Context.lineTo(500, 650);
    Context.moveTo(500, 575);
    Context.lineTo(200, 575);
    Context.moveTo(350, 650);
    Context.lineTo(350, 500);
    Context.strokeStyle = '#FF0000';
    Context.stroke();
    //Рисуем радугу
    DrawRainbow(Context);
}

function DrawRainbow(ContextForDrawing)
{
    var YForMoveCommand = 10;
    var XForBézierControlPoint = 800;
    var YForBézierControlPoint = 100;
    var XForCurveEndingPoint = 800;
    var RGBcolorString = new String();
    var RGBcolorObject;
    var HueDegrees = 0;
    for (var CounterOfColors = 0; CounterOfColors < 10; ++CounterOfColors)
    {
        ContextForDrawing.beginPath();
        ContextForDrawing.moveTo(350, YForMoveCommand);
        ContextForDrawing.quadraticCurveTo(XForBézierControlPoint, YForBézierControlPoint, XForCurveEndingPoint, 500);
        RGBcolorObject = HSVtoRGB(HueDegrees/360, 1, 1);
        ContextForDrawing.strokeStyle = "rgb(" + RGBcolorObject.r.toString() + ", " + RGBcolorObject.g.toString() + ", " + RGBcolorObject.b.toString() + ")";
        ContextForDrawing.lineWidth = 10;
        ContextForDrawing.stroke();
        YForMoveCommand += 10;
        XForBézierControlPoint -= 10;
        YForBézierControlPoint += 10;
        XForCurveEndingPoint -= 10;
        HueDegrees += 30;
    }
}
//Example for style.color "rgb(155, 102, 102)";

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