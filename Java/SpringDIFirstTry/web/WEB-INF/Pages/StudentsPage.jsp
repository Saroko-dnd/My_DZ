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
<form action="/createNewObject" name="Student" method="post">
    <input type="number" name="Age">
    <input type="text" name="Name">
    <input type="text" name="SecondName">
    <input type="submit" value="CreateNewStudent" />
</form>
${Students}
<script>



    function TestAjaxFunction(){
        $.get( "/temperature", function( data ) {
            $( "#temperatureParagraph" ).html( data );
        });
    }

    setTimeout(TestAjaxFunction, 5000);

</script>
</body>
</html>