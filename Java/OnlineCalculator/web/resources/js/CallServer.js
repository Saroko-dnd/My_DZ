/**
 * Created by master on 2017/03/02.
 */
$( document ).ready(function() {
    $( "#EvaluateButton" ).click(function() {
        $.ajax({url: "http://localhost:8080/EvaluateExpression", method : 'POST',dataType : 'text',
            data : $("#ExpressionInput").val().replace(/\s/g,''),
            traditional: true,
            contentType : 'text/plain',
            success: function(result){
                $("#ResultSpan").html(result);
            }});
    });
    // Handler for .ready() called.
});