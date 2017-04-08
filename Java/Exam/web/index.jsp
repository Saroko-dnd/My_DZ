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
        <h1>DB WEB interface</h1>
    </article>
    <p id="ParagraphForTextFromServer"></p>
    <script>
        $(document).ready(function() {
            /*$.ajax({url: "/addNewProducerToDb?Name=SuperProducer&Country=SuperCountry&AnnualProfit=2000", method : 'POST',dataType : 'text',
                traditional: true});*/
            /*$.ajax({url: "/addNewProductToDb?Name=SuperProduct&Price=123&Quantity=50&Color=red&ProducerId=1", method : 'POST',dataType : 'text',
                traditional: true});*/
            $.get( "/getAllProducts", function( data ) {
                $("#ParagraphForTextFromServer" ).text( data );
            });
        });
    </script>
</body>
</html>
