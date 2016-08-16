


function GetUserDataFunction()
{
    var RegexForEmail = /^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i;
    var RegexForAge = /[1-9][0-9]*/;
    var RegexForPhoneNumber = /[+]*375[0-9]{9}/;
    var PersonName = '';
    var PersonLastName = '';
    var PersonPatronymic = '';
    var PersonSex = '';
    var PersonAge = '';
    var PersonEmail = '';
    var PersonPhone = '';
    var ResultOfConfirm = false;

    while (!ResultOfConfirm)
    {
        PersonName = prompt("Please enter your first name:", "");
        PersonLastName = prompt("Please enter your second name:", "");
        PersonPatronymic = prompt("Please enter your patronymic:", "");
        PersonSex = prompt("Please enter your sex:", "");
        PersonAge = AskUserForAge("Please enter your age:", RegexForAge);
        PersonEmail = AskUserForInput("Please enter your email:", RegexForEmail);
        PersonPhone = AskUserPhone("Please enter your phone (Belarus only!):", RegexForPhoneNumber);

        ResultOfConfirm = confirm('First name: ' + PersonName + '\r\n' + 'Last name: ' + PersonLastName + '\r\n' + 'Patronymic: ' + PersonPatronymic + '\r\n' + 'Sex: ' + PersonSex + '\r\n' + 'Age: ' + PersonAge
             + '\r\n' + 'Email: ' + PersonEmail + '\r\n' + 'Phone number:' + PersonPhone + "\r\n\n" + "Are you satisfied with this data?");
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
    Paragraph = document.getElementById('P_forUserPhone');
    Paragraph.textContent = 'Phone number - ' + PersonPhone;
    Paragraph.style.display = 'block';
}

function AskUserForInput(TextOfRequest, RegexForValidation)
{
    var ValidationErrorMessage = "Validation error!";
    var ResultOfCheck = false;
    var UserAnswer = "";
    while (!ResultOfCheck)
    {
        UserAnswer = prompt(TextOfRequest, "");
        if (!RegexForValidation.test(UserAnswer))
        {
            alert(ValidationErrorMessage);
        }
        else
        {
            ResultOfCheck = true;
        }
    }
    return UserAnswer;
}

function AskUserForAge(TextOfRequest, RegexForValidation)
{
    var ValidationErrorMessage = "Validation error!";
    var ResultOfCheck = false;
    var UserAnswer = "";
    while (!ResultOfCheck)
    {
        UserAnswer = prompt(TextOfRequest, "");
        if (!RegexForValidation.test(UserAnswer))
        {
            alert(ValidationErrorMessage);
        }
        else
        {
            if (UserAnswer > 100)
            {
                alert(ValidationErrorMessage);
            }
            else if (UserAnswer > 0)
            {
                ResultOfCheck = true;
            }
        }
    }
    return UserAnswer;
}

function AskUserPhone(TextOfRequest, RegexForValidation)
{
    var ValidationErrorMessage = "Validation error!";
    var ResultOfCheck = false;
    var UserAnswer = "";
    while (!ResultOfCheck)
    {
        UserAnswer = prompt(TextOfRequest, "");
        UserAnswer = UserAnswer.replace(" ", "");
        UserAnswer = UserAnswer.replace("-", "");
        if (UserAnswer.length <= 13)
        {
            if (!RegexForValidation.test(UserAnswer))
            {
                alert(ValidationErrorMessage);
            }
            else
            {
                ResultOfCheck = true;  
            }
        }
        else
        {
            alert(ValidationErrorMessage);
        }
    }
    return UserAnswer;
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

function CreateNewArrayWithFactorials()
{
    var ArrayOfNumbers = [5, 3, 6, 9, 4];
    var NewArrayOfNumbers = ArrayOfNumbers.map(Factorial);
    document.getElementById('P_forArrayWithFactorials').textContent = NewArrayOfNumbers.join(" ");
}

function Factorial(number)
{    
    var Result = 1;
    for (var Counter = 2; Counter <= number; ++Counter)
    {
        Result = Result * Counter;
    }
    return Result;
}

function CreateUserObject()
{
    var User = { name: "Name"};
    User.surname = "Surname";
    User.surname = "NewSurName";
    delete User.name;
    return User;
}