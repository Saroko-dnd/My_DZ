
window.onload = ActivateDynamicGallery;

var DivWithAllImages;
var IndexOfCurrentImage = 5;
var DivForGallery;
var WidthOfImages = 400;
var HeightOfImage = 200;
var AmountOfClicksBeforeDynamicLoading = 5;
var XHRforImages = new XMLHttpRequest();
var PathsToXMLfiles = ['XMLfolder/XMLimages_1.xml', 'XMLfolder/XMLimages_2.xml', 'XMLfolder/XMLimages_3.xml', 'XMLfolder/XMLimages_4.xml'];
var CurrentIndexOfXMLfilePath = 0;

var FirstInterval = null;
var SecondInterval = null;
var ThirdInterval = null;

function ActivateDynamicGallery()
{
    DivWithAllImages = document.getElementById('DivWithAllImages');
    DivForGallery = document.getElementById('GalleryDiv');
    DivWithAllImages.style.width = WidthOfImages.toString() + 'px';
    DivWithAllImages.style.height = HeightOfImage.toString() + 'px';
    document.getElementById('NextImageButton').onclick = NextImageOnClick;
    document.getElementById('ReferenceToOtherImages').href = '#';
    //Меняем позиционирование картинок (для анимации)
    var AllImages = document.getElementsByTagName('IMG');
    for (var Index = 0; Index < AllImages.length; ++Index)
    {
        AllImages[Index].style.position = 'absolute';
        AllImages[Index].style.zIndex = (Index + 1).toString();
    }
}

function NextImageOnClick()
{
    var JQueryObjectForRotation = $('#' + IndexOfCurrentImage.toString() + 'Image');
    if (FirstInterval != null)
    {
        clearInterval(FirstInterval);
        //clearInterval(SecondInterval);
        //clearInterval(ThirdInterval);
    }
    FirstInterval = setInterval(function () {
        StartWidthGrow(JQueryObjectForRotation, FirstInterval);
    }, 100);
    setTimeout(function () { JQueryObjectForRotation.remove(); }, 500);
    --IndexOfCurrentImage;
    if (IndexOfCurrentImage == 0)
    {
        IndexOfCurrentImage = 5;
    }
    /*SecondInterval = setInterval (function(){
        StartHeightGrow(JQueryObjectForRotation, SecondInterval);
    }, 100);
    ThirdInterval = setInterval(function () {
        OpacityDownToZeroForImage(JQueryObjectForRotation, ThirdInterval);
    }, 100);*/
    //JQueryObjectForRotation.animate({ transform: 'rotateY(180deg)' }, 1000);
    /*++IndexOfCurrentImage;
    if (IndexOfCurrentImage > 5)
    {
        IndexOfCurrentImage = 1;
    }*/
    --AmountOfClicksBeforeDynamicLoading;
    if (AmountOfClicksBeforeDynamicLoading > 0)
    {
        //DivWithAllImages.scrollTop += HeightOfImage;
    }
    else
    {
        DynamicLoadingOfImages();
    }
}

function StartWidthGrow(CurrentJQueryObjectForRotation, IntervalForThisFunction)
{
    clearInterval(IntervalForThisFunction);
    CurrentJQueryObjectForRotation.animate({ width: '800px', height: '400px', opacity: '0.0', left: '-200px', top: '-100px', right: '-200px', bottom: '-100px' }, 500);
    $({ deg: 0 }).animate({ deg: 45 }, {
        duration: 2000,
        step: function (now) {
            // in the step-callback (that is fired each step of the animation),
            // you can use the `now` paramter which contains the current
            // animation-position (`0` up to `angle`)
            CurrentJQueryObjectForRotation.css({
                transform: 'rotateY(' + now + 'deg)'
            });
        }
    });
}

function StartHeightGrow(CurrentJQueryObjectForRotation, IntervalForThisFunction)
{
    clearInterval(IntervalForThisFunction);
    CurrentJQueryObjectForRotation.animate({ height: '400px' }, 500);
}

function OpacityDownToZeroForImage(CurrentJQueryObjectForRotation, IntervalForThisFunction)
{
    clearInterval(IntervalForThisFunction);
    CurrentJQueryObjectForRotation.animate({ opacity: '0.0' }, 500);
}

function DynamicLoadingOfImages()
{
    if (CurrentIndexOfXMLfilePath === PathsToXMLfiles.length)
    {
        CurrentIndexOfXMLfilePath = 0;
    }
    AmountOfClicksBeforeDynamicLoading = 5;
    LoadContentOnHTMLPage(PathsToXMLfiles[CurrentIndexOfXMLfilePath]);
    ++CurrentIndexOfXMLfilePath;
}

function LoadContentOnHTMLPage(XMLfilename) {
    //Загружаем контент как таблицу на MainHtmlPage
    var XmlWithContent = loadXMLDoc(XMLfilename);
    var XslFileForContent = loadXMLDoc("XSLTfolder/MainXSLTFile.xslt");
    var xsltProcessor = new XSLTProcessor();
    xsltProcessor.importStylesheet(XslFileForContent);
    var ResultTableWithContent = xsltProcessor.transformToFragment(XmlWithContent, document);
    var AllImagesOnPage = document.getElementsByTagName('IMG');
    for (var Index = 4; Index > -1; --Index)
    {
        DivWithAllImages.removeChild(AllImagesOnPage[Index]);
    }
    DivWithAllImages.appendChild(ResultTableWithContent);
    DivWithAllImages.scrollTop = 0;
}

function loadXMLDoc(filename) {
    if (window.ActiveXObject) {
        xhttp = new ActiveXObject("Msxml2.XMLHTTP");
    }
    else {
        xhttp = new XMLHttpRequest();
    }
    xhttp.open("GET", filename, false);
    try { xhttp.responseType = "msxml-document" } catch (err) { } // Helping IE11
    xhttp.send("");
    return xhttp.responseXML;
}