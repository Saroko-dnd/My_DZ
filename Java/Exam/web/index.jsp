<%--
  Created by IntelliJ IDEA.
  User: admin
  Date: 01.04.2017
  Time: 11:14
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Title</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
</head>
<body>
    <article>
        <h1>DB interface</h1>
    </article>
    <script>
        $(document).ready(function() {
            $.ajax({url: "/addNewProducerToDb?Name=TestProduct&Country=TestCountry&AnnualProfit=1500", method : 'POST',dataType : 'text',
                traditional: true});
        });
    </script>
</body>
</html>
