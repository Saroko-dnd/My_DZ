
window.onload = CreateListFromDivStructure;

function CreateListFromDivStructure()
{
    var BodyOfDocument = window.document.body;
    BodyOfDocument.appendChild(GetHtmElementAsText(BodyOfDocument));
    var AllLists = document.getElementsByTagName('li');
    for (var Counter = 0; Counter < AllLists.length; ++Counter)
    {
        AllLists[Counter].style.visible = true;
        AllLists[Counter].onclick = ListOnClick;
    }
}

function ListOnClick(event)
{
        if (this.visible)
        {
            for (var Counter = 0; Counter < this.childNodes.length; ++Counter)
            {
                if (this.childNodes[Counter].nodeType == 1)
                {
                    this.childNodes[Counter].style.display = 'none';
                }
            }
            this.visible = false;
        }
        else
        {
            for (var Counter = 0; Counter < this.childNodes.length; ++Counter)
            {
                if (this.childNodes[Counter].nodeType == 1)
                {
                    this.childNodes[Counter].style.display = 'block';
                }
            }
            this.visible = true;
        }
        event.stopPropagation();
}

var FirstUL = true;

function GetHtmElementAsText(CurrentBodyOfDocument)
{
    var CurrentList = document.createElement('UL');
    if (FirstUL)
    {
        FirstUL = false;
    }
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