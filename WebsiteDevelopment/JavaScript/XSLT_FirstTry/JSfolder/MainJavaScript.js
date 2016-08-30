
window.onload = LoadContentOnHTMLPage;

function LoadContentOnHTMLPage()
{
    //Загружаем контент как таблицу на MainHtmlPage
    var XmlWithContent = loadXMLDoc("XMLfolder/XMLFile.xml");
    var XslFileForContent = loadXMLDoc("XSLTfolder/MainXSLTFile.xslt");
    var xsltProcessor = new XSLTProcessor();
    xsltProcessor.importStylesheet(XslFileForContent);
    var ResultTableWithContent = xsltProcessor.transformToFragment(XmlWithContent, document);
    document.body.appendChild(ResultTableWithContent);
    document.getElementById("OnlySmallPriceButton").onclick = LoadContentWithSmallPrices("XSLTfolder/OnlySmallPriceXSLTFile.xslt");
    document.getElementById("OnlyPriceLessThanAverageButton").onclick = LoadContentWithSmallPrices("XSLTfolder/AvgXSLTFile.xslt");
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

function LoadContentWithSmallPrices(XSLTfileName)
{
    //убираем предыдущую таблицу
    return function()
    {
        var CurrentTitileHeader = document.getElementsByTagName('h2')[0];
        CurrentTitileHeader.parentElement.removeChild(CurrentTitileHeader);
        var CurrentTableWithContent = document.getElementsByTagName('table')[0];
        CurrentTableWithContent.parentElement.removeChild(CurrentTableWithContent);
        var XmlWithContent = loadXMLDoc("XMLfolder/XMLFile.xml");
        var XslFileForContent = loadXMLDoc(XSLTfileName);
        var xsltProcessor = new XSLTProcessor();
        xsltProcessor.importStylesheet(XslFileForContent);
        var ResultTableWithContent = xsltProcessor.transformToFragment(XmlWithContent, document);
        document.body.appendChild(ResultTableWithContent);
    }
}
