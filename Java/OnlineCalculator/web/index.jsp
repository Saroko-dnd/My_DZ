<%--
  Created by IntelliJ IDEA.
  User: master
  Date: 2017/03/02
  Time: 3:31
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Calculator</title>
    <link href="/resources/css/TestCss.css" rel="stylesheet" type="text/css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="/resources/js/CallServer.js"></script>
</head>
<body>
    <h1>Math Expression Calculator</h1>
    <input type="text" id="ExpressionInput">
    <p>
        Result: <span id="ResultSpan"></span>
    </p>
    <input type="button" value="Evaluate" id="EvaluateButton">
</body>
</html>
