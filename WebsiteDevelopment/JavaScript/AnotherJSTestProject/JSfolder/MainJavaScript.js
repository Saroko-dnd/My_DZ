

document.onload = AskUser;

function AskUser()
{
    /*var UserAge = prompt("Сколько вам лет?");
    if (UserAge < 18 )
    {
        alert("Жениться нельзя!! ");
    }
    else
    {
        alert("Жениться можно!! ");
    }*/
    document.getElementById("CreateTableButtonID").onclick = CreateTable;
    var AllImages = document.getElementsByTagName('img');
    for (var ImgIndex = 0; ImgIndex < AllImages.length; ++ImgIndex)
    {
        AllImages[ImgIndex].onclick = ChangeImageSizeOnClick;
        AllImages[ImgIndex].big = false;
    }
}

function ChangeImageSizeOnClick()
{
    console.log(this.id + "ID");
    console.log(this.style.width);
    console.log(this.big);
    if (this.big == false)
    {
        this.big = true;
        this.style.height = '300px';
        this.style.width = '300px';
    }
    else {
        this.big = false;
        this.style.width = '100px';
        this.style.height = '100px';
    }
}

function CreateTable()
{
    var AmountOfRows = document.getElementById("NumberOfRowsInputID").value;
    var AmountOfColumns = document.getElementById("NumberOfColumnsInputID").value;
    var Rows = [];
    for (var RowsCounter = 0; RowsCounter < AmountOfRows; ++RowsCounter)
    {
        var Columns = [];
        for (var ColumnsCounter = 0; ColumnsCounter < AmountOfColumns; ++ColumnsCounter)
        {
            Columns.push(RowsCounter * ColumnsCounter);
        }
        Rows.push(Columns);
    }
    var InnerTable = "<table>";
    for (var RowCounter = 0; RowCounter < Rows.length; ++RowCounter)
    {
        InnerTable += "<tr>"
        for (var ColumnCounter = 0; ColumnCounter < Rows[RowCounter].length; ++ColumnCounter)
        {
            InnerTable += "<td>";
            InnerTable += Rows[RowCounter][ColumnCounter];
            InnerTable += "</td>";
        }
        InnerTable += "</tr>"
    }
    InnerTable += "</table>";
    document.getElementById("DivForTableID").innerHTML = InnerTable;
}