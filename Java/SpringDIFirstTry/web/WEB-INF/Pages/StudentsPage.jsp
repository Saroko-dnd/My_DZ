<%--
  Created by IntelliJ IDEA.
  User: admin
  Date: 25.02.2017
  Time: 10:50
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Title</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
</head>
<body>
<h1>List of students</h1>
<p id="temperatureParagraph"></p>

<input type="number" name="Age" id="AgeInput">
<input type="text" name="Name" id="NameInput">
<input type="text" name="SecondName" id="SecondNameInput">
<input type="submit" value="CreateNewStudent" id="CreateNewStudentButton"/>

<input type="button" value="Get object from server as json string" id="ButtonForCallingJsonObject">
<input type="button" value="Refresh students info" id="RefreshButton">
<div id="StudentsDataContainer">

</div>
${Students}
<script>


    function TestAjaxFunction(){
        $.get( "/temperature", function( data ) {
            $( "#temperatureParagraph" ).html( data );
        });
    }

    $(document).ready(function() {
        $( "#ButtonForCallingJsonObject" ).click(function() {
            /*$.get( "/getObjectAsJson","SUPER DATA", function( data ) {
                $( "#DivForJsonString" ).html( data );
            });*/

            $.ajax({url: "/addNewStudentToDb", method : 'POST',dataType : 'text',
                data : "SUPER DATA",
                traditional: true,
                contentType : 'text/plain',
                success: function(result){
                    $("#DivForJsonString").html(result);
                }});
        });

        $( "#RefreshButton" ).click(function() {
            GetAllInfoAboutStudents();
        });

        $("#CreateNewStudentButton").click(function() {
            let age = $("#AgeInput").val();
            let name = $("#NameInput").val();
            let SecondName = $("#SecondNameInput").val();
            $.ajax({url: "/addNewStudentToDb?Age=" + age + '&Name=' + name + '&SecondName=' + SecondName,
                method : 'POST'});
        });
    });

    function GetAllInfoAboutStudents(){
        $.get( "/studentsInfo", function( data ) {
            $( "#StudentsDataContainer" ).html( data );
        });
    }

    setTimeout(TestAjaxFunction, 5000);

</script>
</body>
</html>