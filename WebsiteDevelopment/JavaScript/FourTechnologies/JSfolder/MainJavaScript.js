
window.onload = ActivateDynamicGallery;

var DivWithAllImages;
var DivForGallery;
var WidthOfImages = 400;
var HeightOfImage = 200;
var AmountOfClicksBeforeDynamicLoading = 5;
var XHRforImages = new XMLHttpRequest();
var PathsToXMLfiles = ['XMLfolder/XMLimages_1.xml', 'XMLfolder/XMLimages_2.xml', 'XMLfolder/XMLimages_3.xml', 'XMLfolder/XMLimages_4.xml'];
var CurrentIndexOfXMLfilePath = 0;

function ActivateDynamicGallery()
{
    DivWithAllImages = document.getElementById('DivWithAllImages');
    DivForGallery = document.getElementById('GalleryDiv');
    DivWithAllImages.style.width = WidthOfImages.toString() + 'px';
    DivWithAllImages.style.height = HeightOfImage.toString() + 'px';
    document.getElementById('NextImageButton').onclick = NextImageOnClick;
    document.getElementById('ReferenceToOtherImages').href = '#';
}

function NextImageOnClick()
{
    --AmountOfClicksBeforeDynamicLoading;
    if (AmountOfClicksBeforeDynamicLoading > 0)
    {
        DivWithAllImages.scrollTop += HeightOfImage;
    }
    else
    {
        DynamicLoadingOfImages();
    }
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
    //document.body.appendChild(ResultTableWithContent);
    var AllImagesOnPage = document.getElementsByTagName('IMG');
    for (var Index = 0; Index < AllImagesOnPage.length; ++Index)
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