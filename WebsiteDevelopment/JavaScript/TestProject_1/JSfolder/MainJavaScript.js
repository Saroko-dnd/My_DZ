
function GetUserDataFunction() {
    var PersonName = prompt("Please enter your first name:", "waiting for input");
    var PersonLastName = prompt("Please enter your second name:", "waiting for input");
    var PersonPatronymic = prompt("Please enter your patronymic:", "waiting for input");
    var PersonAge = prompt("Please enter your age:", "waiting for input");
    var PersonEmail = prompt("Please enter your email:", "waiting for input");
    document.getElementById('P_hesderOfSurveyResult').textContent = 'RESULT OF SURVEY';
    document.getElementById('P_forUserName').textContent = 'Name - ' + PersonName;
    document.getElementById('P_forUserLastName').textContent = 'LastName - ' + PersonLastName;
    document.getElementById('P_forUserPatronymic').textContent = 'Patronymic - ' + PersonPatronymic;
    document.getElementById('P_forUserAge').textContent = 'Age - ' + PersonAge;
    document.getElementById('P_forUserEmail').textContent = 'Email - ' + PersonEmail;
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