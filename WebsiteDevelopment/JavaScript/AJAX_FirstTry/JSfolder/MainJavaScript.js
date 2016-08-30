
window.onload = EstablishHandersForEvent;

var AmountOfImagesForDynamicLoading = 5;
var AmountOfScrollClicksBeforeLoadNewImages = 3;
var HeightOfImages = 200;
var DivForGallery;
var DivWithImages;
var XHRobjectForFood = new XMLHttpRequest();
var XHRobjectForWater = new XMLHttpRequest();
var XHRobjectForImage = new XMLHttpRequest();
var ArrayOfImagesForDynamicLoading = ['Images/Image_6.jpg', 'Images/Image_7.jpg', 'Images/Image_8.jpg', 'Images/Image_9.jpg', 'Images/Image_10.jpg'];
var ParagraphForAJAXresult;

function EstablishHandersForEvent()
{
    window.URL = window.URL || window.webkitURL;
    DivWithImages = document.getElementById('DivWithAllImages');
    DivForGallery = document.getElementById('DivForGallery');
    DivForGallery.style.width = '400px';
    DivForGallery.style.height = HeightOfImages.toString() + 'px';
    var ReferenceToOthersImages = document.getElementById('AReferenceToOtherImages');
    ReferenceToOthersImages.href = '#';
    document.getElementById('FoodButton').onclick = MenuButtonClick(XHRobjectForFood, 'Food.txt');
    document.getElementById('WaterButton').onclick = MenuButtonClick(XHRobjectForWater, 'Water.txt');
    document.getElementById('ButtonForImageGallery').onclick = NextImageOnClick;
    ParagraphForAJAXresult = document.getElementById('PForAnswerFromServer');
    XHRobjectForFood.onreadystatechange = XHRReadyStateChanged(XHRobjectForFood);
    XHRobjectForWater.onreadystatechange = XHRReadyStateChanged(XHRobjectForWater);
    XHRobjectForImage.onload = AddNewImageToGallery;
    XHRobjectForImage.responseType = 'blob';
}

function NextImageOnClick()
{
    DivForGallery.scrollTop += HeightOfImages;
    if (DivForGallery.scrollTop >= HeightOfImages * AmountOfScrollClicksBeforeLoadNewImages)
    {
        RequestForOneImageToGallery();
    }
}

function AddNewImageToGallery()
{
    if (XHRobjectForImage.status === 200) {
        var ImageBlobObject = XHRobjectForImage.response;
        var NewImage = document.createElement('IMG');
        NewImage.src = window.URL.createObjectURL(ImageBlobObject);
        DivWithImages.appendChild(NewImage);
        if (AmountOfImagesForDynamicLoading > 0) {
            RequestForOneImageToGallery();
        }
    }
    else {
        alert('Error was occured during loading of image!');
    }  
}

function RequestForOneImageToGallery()
{
    if (AmountOfImagesForDynamicLoading > 0 && (XHRobjectForImage.readyState === 0 || XHRobjectForImage.readyState === 4))
    {
        XHRobjectForImage.open('GET', ArrayOfImagesForDynamicLoading[AmountOfImagesForDynamicLoading - 1], true);
        XHRobjectForImage.send();
        --AmountOfImagesForDynamicLoading;
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