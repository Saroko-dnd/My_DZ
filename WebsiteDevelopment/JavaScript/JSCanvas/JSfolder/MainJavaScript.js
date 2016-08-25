

window.onload = DrawInCanvas;


function DrawInCanvas()
{
    console.log('start');
    var MainCanvas = document.getElementById('MainCanvasID');
    var Context = MainCanvas.getContext("2d");
    //Рисуем дом
    Context.beginPath();
    Context.moveTo(100, 800);
    Context.lineTo(600, 800);
    Context.lineTo(600, 300);
    Context.lineTo(100, 300);
    Context.lineTo(100, 800);
    Context.moveTo(100, 300);
    Context.lineTo(350, 100);
    Context.lineTo(600, 300);
    Context.moveTo(500, 600);
    Context.lineTo(500, 450);
    Context.lineTo(200, 450);
    Context.lineTo(200, 600);
    Context.lineTo(500, 600);
    Context.moveTo(500, 525);
    Context.lineTo(200, 525);
    Context.moveTo(350, 600);
    Context.lineTo(350, 450);
    Context.strokeStyle = '#FF0000';
    Context.stroke();
    //Рисуем радугу
    Context.beginPath();
    Context.moveTo(350, 10);
    Context.quadraticCurveTo(800, 100, 800, 500);
    Context.strokeStyle = 'violet';
    Context.lineWidth = 10;
    Context.stroke();
    Context.beginPath();
    Context.moveTo(350, 20);
    Context.quadraticCurveTo(790, 110, 790, 500);
    Context.strokeStyle = 'darkviolet';
    Context.lineWidth = 10;
    Context.stroke();
    Context.beginPath();
    Context.moveTo(350, 30);
    Context.quadraticCurveTo(780, 120, 780, 500);
    Context.strokeStyle = 'blue';
    Context.lineWidth = 10;
    Context.stroke();
    Context.beginPath();
    Context.moveTo(350, 40);
    Context.quadraticCurveTo(770, 130, 770, 500);
    Context.strokeStyle = 'green';
    Context.lineWidth = 10;
    Context.stroke();
    Context.beginPath();
    Context.moveTo(350, 50);
    Context.quadraticCurveTo(760, 140, 760, 500);
    Context.strokeStyle = 'yellow';
    Context.lineWidth = 10;
    Context.stroke();
    Context.beginPath();
    Context.moveTo(350, 60);
    Context.quadraticCurveTo(750, 150, 750, 500);
    Context.strokeStyle = 'orange';
    Context.lineWidth = 10;
    Context.stroke();
    Context.beginPath();
    Context.moveTo(350, 70);
    Context.quadraticCurveTo(750, 160, 740, 500);
    Context.strokeStyle = 'red';
    Context.lineWidth = 10;
    Context.stroke();
    console.log('end');
}

