
<?php
echo "<h2>PHP is Fun!</h2>";
echo "Hello world!<br>";
echo "I'm about to learn PHP!<br>";
echo "This ", "string ", "was ", "made ", "with multiple parameters.<br>";
$TestHtmlReference = htmlspecialchars("This is reference after htmlspecialchars() <a href='test'>Test reference</a>", ENT_QUOTES);
$PureHtmlReference = "This is usual reference <a href='test'>Test reference</a>";
echo $PureHtmlReference, "<br>";
echo $TestHtmlReference, "<br>";
?> 

 <form action="">
  <p><b>Как по вашему мнению расшифровывается аббревиатура &quot;ОС&quot;?</b></p>
  <p><input type="radio" name="answer" value="a1">Офицерский состав<Br>
  <input type="radio" name="answer" value="a2">Операционная система<Br>
  <input type="radio" name="answer" value="a3">Большой полосатый мух</p>
  <p><input type="submit"></p>
  <label for="TestCheckBox">Секретно</label>
  <input type="checkbox" name="TestCheckBox" value="Checkbox value">
 </form>

<?php
 print_r($_GET);
 //If value with name 'TestCheckBox' exists then echo
 if(isset($_GET['TestCheckBox'])) {
        echo "Check box was checked";
    }
?>

