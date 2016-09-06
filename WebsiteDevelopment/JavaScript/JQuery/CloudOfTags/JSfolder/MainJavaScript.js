

window.onload = CreateAssociativeArray;

var RequestForTextFile = new XMLHttpRequest();
var ListOfWordsInText;
var ListOfWordsAsObjects = new Array();

function CreateAssociativeArray()
{
    RequestForTextFile.onload = ParseTextFile;
    RequestForTextFile.open("GET", "TextsForTags/FirstText.txt", true);
    RequestForTextFile.send();
}

function ParseTextFile()
{
    ListOfWordsInText = RequestForTextFile.responseText.split(" ");
    for (var Index = 0; Index < ListOfWordsInText.length; ++Index)
    {
        ListOfWordsInText[Index] = ListOfWordsInText[Index].replace(/[^a-zA-Z]/g, "");
        ListOfWordsInText[Index] = ListOfWordsInText[Index].trim();
        if (ListOfWordsInText[Index] != "")
        {
            var CopyOfWord = ListOfWordsInText[Index];
            var NewWordObject = { score: 0, wordItself: ListOfWordsInText[Index] };
            NewWordObject.wordItself = ListOfWordsInText[Index];
            NewWordObject.score = 0;
            for (var SecondIndex = 0; SecondIndex < ListOfWordsInText.length; ++SecondIndex) {
                if (CopyOfWord == ListOfWordsInText[SecondIndex])
                {
                    NewWordObject.score += 1;
                    ListOfWordsInText[SecondIndex] = "";
                }
            }
            ListOfWordsAsObjects.push(NewWordObject);
        }   
    }
    ListOfWordsAsObjects.sort(function (FirstElement, SecondElement) {
        return FirstElement.score < SecondElement.score;
        console.log('sort');
    });
    var WordsForTagCloud = new Array();
    for (var Index = 0; Index < 100; ++Index)
    { 
        WordsForTagCloud.push({ text: ListOfWordsAsObjects[Index].wordItself, weight: Math.log(ListOfWordsAsObjects[Index].score), link: 'http://github.com/mistic100/jQCloud' });
    }
    $('#DivForTagCloud').jQCloud(WordsForTagCloud);
    //ListOfWordsInText.forEach(CountScoreForCurrentWord);
    var fff = 9;
}

