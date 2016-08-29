

window.onload = EstablishHandersForEvent;

var ImgForGallery;
var XHRobjectForFood = new XMLHttpRequest();
var XHRobjectForWater = new XMLHttpRequest();
var XHRobjectForImage = new XMLHttpRequest();
var ArrayOfImagePaths = ['Images/Image_1.jpg', 'Images/Image_2.jpg', 'Images/Image_3.jpg', 'Images/Image_4.jpg'];
var CurrentIndexOfArray = 0;
var ParagraphForAJAXresult;

function EstablishHandersForEvent()
{
    ImgForGallery = document.getElementById('ImgForGallery');
    document.getElementById('FoodButton').onclick = MenuButtonClick(XHRobjectForFood, 'Food.txt');
    document.getElementById('WaterButton').onclick = MenuButtonClick(XHRobjectForWater, 'Water.txt');
    document.getElementById('ButtonForImageGallery').onclick = MenuButtonClick(XHRobjectForWater, 'Water.txt');
    ParagraphForAJAXresult = document.getElementById('PForAnswerFromServer');
    XHRobjectForFood.onreadystatechange = XHRReadyStateChanged(XHRobjectForFood);
    XHRobjectForWater.onreadystatechange = XHRReadyStateChanged(XHRobjectForWater);
    XHRobjectForImage.onreadystatechange = XHRReadyStateChanged(XHRobjectForWater);
}

function NextImageOnClick()
{
    if (XHRobjectForImage.readyState === 0 || XHRobjectForImage.readyState === 4)
    {
        XHRobjectForImage.open('GET', ArrayOfImagePaths[CurrentIndexOfArray], true);
        XHRobjectForImage.send();
        ++CurrentIndexOfArray;
        if (CurrentIndexOfArray === ArrayOfImagePaths.length)
        {
            CurrentIndexOfArray = 0;
        }
    }
}

function ChangeImgSrc()
{
    if (XHRobjectForImage.readyState === 4) {
        if (XHRobjectForImage.status === 200) {
            ParagraphForAJAXresult.innerText = XHRobjectForImage.responseText;
        }
        else {
            ParagraphForAJAXresult.innerText = 'Error!';
        }
    }
}

function MenuButtonClick(CurrentXHRObject, FileName)
{
    return function () {
        if (CurrentXHRObject.readyState === 0 || CurrentXHRObject.readyState === 4) {
            CurrentXHRObject.open('GET', FileName, true);
            CurrentXHRObject.send();
        }
    };
}

function XHRReadyStateChanged(CurrentXHRObject)
{
    return function () {
        if (CurrentXHRObject.readyState === 4) {
            if (CurrentXHRObject.status === 200) {
                ParagraphForAJAXresult.innerText = CurrentXHRObject.responseText;
            }
            else {
                ParagraphForAJAXresult.innerText = 'Error!';
            }
        }
    };
}