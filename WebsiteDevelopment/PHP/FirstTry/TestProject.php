
<?php
echo "<h2>PHP is Fun!</h2>";
echo "Hello world!<br>";
echo "I'm about to learn PHP!<br>";
echo "This ", "string ", "was ", "made ", "with multiple parameters.<br>";
$TestHtmlReference = htmlspecialchars("This is reference after htmlspecialchars() <a href='test'>Test reference</a>", ENT_QUOTES);
$PureHtmlReference = "This is usual reference <a href='test'>Test reference</a>";
echo $PureHtmlReference, "<br>";
echo $TestHtmlReference, "<br>";

/*$servername = "localhost";
$username = "root";
$password = "";
$dbname = "dbfortestproject";

$conn = new mysqli($servername, $username, $password, $dbname);

$sql = "INSERT INTO product (Cost, Name, Weight)
VALUES (200, 'TestProduct', 50),(100, 'SecondTestProduct', 10),(1500, 'ThirdTestProduct', 110)";

if ($conn->query($sql)) {
    echo "<p style='color: green'>New record created successfully</p>";
} else {
    echo "<p style='color: red'>", "Error: " . $sql . "<br>" . $conn->error, "</p>";
}

$conn->close();*/

?> 

 <form action="">
  <p><b>Как по вашему мнению расшифровывается аббревиатура &quot;ОС&quot;?</b></p>
  <p><input type="radio" name="answer" value="a1">Офицерский состав<Br>
  <input type="radio" name="answer" value="a2">Операционная система<Br>
  <input type="radio" name="answer" value="a3">Большой полосатый мух</p>
  <label for="TestCheckBox">Секретно</label>
  <input type="checkbox" name="TestCheckBox" value="Checkbox value">
 </form>

<form action="">
    <p> Введите ID продукта:</p>
    <input type='number' name='SelectedProductID' id='InputForProductID'/>
    <input type='submit' value='Найти'/>
</form>
 

<?php

        function GetProductById($SelectedId)
        {
            $servername = "localhost";
            $username = "root";
            $password = "";
            $dbname = "dbfortestproject";
            // Create connection
            $conn = new mysqli($servername, $username, $password, $dbname);
            $sql = "SELECT * FROM products WHERE ProductID='".$SelectedId."'";
            $stmt = $conn->prepare("SELECT * FROM products WHERE ProductID=?");
            $stmt->bind_param("d", $SelectedId);//bind_result after execute
            $stmt->execute();
            $result = $stmt->setFetchMode(PDO::FETCH_ASSOC);
            //$result = $conn->query($sql);
            $ResultText = "";
            if ($result->num_rows > 0) {
    // output data of each row
                while($row = $result->fetch_assoc()) {
                    $ResultText = $ResultText ."<p>id: " .$row["ProductID"] ." Name: " .$row["Name"] . " Cost: " .$row["Cost"] . "</p>";
                }
            } else {
                $ResultText = "0 results";
            }

            $conn->close();

            return $ResultText;
        }
 //print_r($_GET);

 //If value with name 'TestCheckBox' exists then echo
 if(isset($_GET['SelectedProductID'])) {
        echo GetProductById($_GET['SelectedProductID']);
    }
?>


