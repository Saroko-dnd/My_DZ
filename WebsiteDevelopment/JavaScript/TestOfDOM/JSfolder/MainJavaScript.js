
window.onload = CreateListFromDivStructure;

function CreateListFromDivStructure()
{
    var BodyOfDocument = window.document.body;
    BodyOfDocument.appendChild(GetHtmElementAsText(BodyOfDocument));
}

function GetHtmElementAsText(CurrentBodyOfDocument)
{
    var CurrentList = document.createElement('UL');
    var NodesForCurrentElement = CurrentBodyOfDocument.childNodes;
    for (var Counter = 0; Counter < NodesForCurrentElement.length; ++Counter )
    {
        if (NodesForCurrentElement[Counter].nodeType == 1)
        {
            var NewElementOfList = document.createElement('LI');
            NewElementOfList.appendChild(document.createTextNode(NodesForCurrentElement[Counter].tagName));
            CurrentList.appendChild(NewElementOfList);
            if (NodesForCurrentElement[Counter].childNodes.length > 0)
            {
                NewElementOfList.appendChild(GetHtmElementAsText(NodesForCurrentElement[Counter]));
            }
        }
    }
    return CurrentList;
}