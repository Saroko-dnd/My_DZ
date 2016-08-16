
function GetUserDataFunction()
{
    var PersonName = '';
    var PersonLastName = '';
    var PersonPatronymic = '';
    var PersonSex = '';
    var PersonAge = '';
    var PersonEmail = '';
    var ResultOfConfirm = false;

    while (!ResultOfConfirm)
    {
        PersonName = prompt("Please enter your first name:", "");
        PersonLastName = prompt("Please enter your second name:", "");
        PersonPatronymic = prompt("Please enter your patronymic:", "");
        PersonSex = prompt("Please enter your sex:", "");
        PersonAge = prompt("Please enter your age:", "");
        PersonEmail = prompt("Please enter your email:", "");
        ResultOfConfirm = confirm('First name: ' + PersonName + '\r\n' + 'Last name: ' + PersonLastName + '\r\n' + 'Patronymic: ' + PersonPatronymic + '\r\n' + 'Sex: ' + PersonSex + '\r\n' + 'Age: ' + PersonAge
             + '\r\n' + 'Email: ' + PersonEmail);
    }
    
    var Paragraph;
    Paragraph = document.getElementById('P_hesderOfSurveyResult');
    Paragraph.textContent = 'RESULT OF SURVEY';
    Paragraph.style.display = 'block';
    Paragraph = document.getElementById('P_forUserName');
    Paragraph.textContent = 'First name - ' + PersonName;
    Paragraph.style.display = 'block';
    Paragraph = document.getElementById('P_forUserLastName');
    Paragraph.textContent = 'Last name - ' + PersonLastName;
    Paragraph.style.display = 'block';
    Paragraph = document.getElementById('P_forUserPatronymic');
    Paragraph.textContent = 'Patronymic - ' + PersonPatronymic;
    Paragraph.style.display = 'block';
    Paragraph = document.getElementById('P_forUserSex');
    Paragraph.textContent = 'Sex - ' + PersonSex;
    Paragraph.style.display = 'block';
    Paragraph = document.getElementById('P_forUserAge');
    Paragraph.textContent = 'Age - ' + PersonAge;
    Paragraph.style.display = 'block';
    Paragraph = document.getElementById('P_forUserEmail');
    Paragraph.textContent = 'Email - ' + PersonEmail;
    Paragraph.style.display = 'block';
}

var NumberOfQuestion = 1;
var AmountOfQuestions = 3;

function GetTestResult()
{
    var UserScore = 0;
    if (document.getElementById('FirstQuestionRadio_1').checked)
    {
        UserScore += 1;
    }
    if (document.getElementById('SecondQuestionRadio_1').checked)
    {
        UserScore += 1;
    }
    if (document.getElementById('ThirdQuestionRadio_1').checked)
    {
        UserScore += 1;
    }
    document.getElementById('PForResultsOfTest').style.display = 'block';
    document.getElementById('PForResultsOfTest').textContent = 'Result of test: ' + UserScore + ' from ' + AmountOfQuestions;
    document.getElementById('NextQuestionButtonID').style.display = 'none';
    document.getElementById('EndTestButtonID').style.display = 'none';
    document.getElementById('DivFirstQuestion').style.display = 'none';
    document.getElementById('DivSecondQuestion').style.display = 'none';
    document.getElementById('DivThirdQuestion').style.display = 'none';
}

function OnPageLoad()
{
    document.getElementById('DivFirstQuestion').style.display = 'block';
}

function NextQuestion()
{
    if (NumberOfQuestion == 1)
    {
        document.getElementById('DivFirstQuestion').style.display = 'none';
        document.getElementById('DivSecondQuestion').style.display = 'block';
    }
    if (NumberOfQuestion == 2)
    {
        document.getElementById('DivSecondQuestion').style.display = 'none';
        document.getElementById('DivThirdQuestion').style.display = 'block';
    }
    if (NumberOfQuestion == 3)
    {
        document.getElementById('DivThirdQuestion').style.display = 'none';
        GetTestResult();
        document.getElementById('NextQuestionButtonID').style.display = 'none';
        document.getElementById('EndTestButtonID').style.display = 'none';
    }
    ++NumberOfQuestion;
}

window.onload = OnPageLoad();