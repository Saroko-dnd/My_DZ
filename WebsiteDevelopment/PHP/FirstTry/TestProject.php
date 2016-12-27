
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
  <label for="TestCheckBox">Секретно</label>
  <input type="checkbox" name="TestCheckBox" value="Checkbox value">
 </form>

 <form action="">
     <p>Enter manufacturer name to get info about all his products:</p>
     <input type="text" name="SelectedProductManufacturer">      
     <input type="submit" value="Найти">
 </form>

 <?php
 //SELECT EXAMPLE with binding
  if(isset($_GET['SelectedProductManufacturer'])) {
        CreateDbIfNotExist();
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "firsttrydb";

        $conn = new mysqli($servername, $username, $password, $dbname);  

        $stmt = $conn->prepare("SELECT * FROM products WHERE Manufacturer=?");
        $stmt->bind_param('s', $SelectedProductManufacturer);
        $SelectedProductManufacturer = $_GET['SelectedProductManufacturer'];

        $stmt->execute();
        $stmt->bind_result($ProductID, $ProductName, $ManufacturerOfProduct, $ProductCost);
        while ($stmt->fetch()) {
            printf("ID: %d Name: %s Manufacturer: %s Cost: %d<br>", $ProductID, $ProductName, $ManufacturerOfProduct, $ProductCost);
        }
        $stmt->close();
        $conn->close();
    }
 ?>

<h1>Addition of new products to database</h1>
<form action="">
    <p>Name of new product</p>
    <input type="text" name="NewProductName"> 
    <p>Manufacturer of new product</p>
    <input type="text" name="NewProductManufacturer"> 
    <p>Cost of new product</p>
    <input type="number" name="NewProductCost"> 
    <input type="submit" value="Add new product to database">
</form>

<?php
 //print_r($_GET);
//INSERT EXAMPLE with binding
 if(isset($_GET['NewProductName']) && isset($_GET['NewProductManufacturer']) && isset($_GET['NewProductCost'])) {
        CreateDbIfNotExist();
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "firsttrydb";
        
        $conn = new mysqli($servername, $username, $password, $dbname);  

        $stmt = $conn->prepare("INSERT INTO products (Name, Manufacturer, Cost) VALUES (?, ?, ?)");
        $stmt->bind_param('ssd', $NewProductName, $NewProductManufacturer, $NewProductCost);
        $NewProductName = $_GET['NewProductName'];
        $NewProductManufacturer = $_GET['NewProductManufacturer'];        
        $NewProductCost = $_GET['NewProductCost'];

        $stmt->execute();
        $stmt->close();

        $conn->close();
    }

    function CreateDbIfNotExist()
    {
        //phpinfo();
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "";
        //$dbname = "firsttrydb";
        $conn = new mysqli($servername, $username, $password, $dbname);
        
        if(!$conn->select_db("firsttrydb"))
        {
            $SqlQuery = 'CREATE DATABASE firsttrydb';
            $conn->query($SqlQuery);
            $conn->close();  
            $conn = new mysqli($servername, $username, $password, "firsttrydb");
            $SqlQuery = "CREATE TABLE products (
                ID INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY, 
                Name VARCHAR(50) NOT NULL,
                Manufacturer VARCHAR(50) NOT NULL,
                Cost INT UNSIGNED
                )";
            $conn->query($SqlQuery);       
        }
        $conn->close();  
    }

        //Test of classes in php
    class TestClass
    {
        public $TestVar = "Blablabla";
    }

    $TestObject = new TestClass();
    echo $TestObject->TestVar;
?>

<hr>

<h1>AJAX and PHP test!</h1>
<p>Start type somesthing:</p>     
<input type="text" id="InputForAjaxRequest" onkeyup="AskForHintFromPHP(this.value)">
<p>String length from php:<span id="SpanForSuggestions"></span></p>

<script>
    var InputElement;
    var ElementForSuggestions;
    window.onload = GetInfoAbotElements;

    function GetInfoAbotElements()
    {
        ElementForSuggestions = document.getElementById("SpanForSuggestions");
        InputElement = document.getElementById("InputForAjaxRequest");
    }

    function AskForHintFromPHP(UserString)
    {
        if (InputElement.value == "")
        {
            ElementForSuggestions.innerHTML = "";
        }
        else
        {
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function() {
                if (this.readyState == 4 && this.status == 200) {
                    ElementForSuggestions.innerHTML = this.responseText;
                }
            };
            xmlhttp.open("GET", "AjaxTest.php?UserString=" + UserString, true);
            xmlhttp.send();
        }
    }
</script>

<hr>
<h1>Cookies and Session PHP test! (Session will last only one minute!)</h1>
<form> 
    Enter yor username (for session):<input type="text" name="UserNameForSession">
    <input type="submit">
</form>     

<?php
    if(isset($_COOKIE['counter']))
    {
        ++$_COOKIE['counter'];
        setcookie("counter",$_COOKIE['counter']);
        echo "You have visited this page " . $_COOKIE['counter'] . " times (from Cookies)","<br/>";
    }
    else
    {
        setcookie("counter",1);
    }


    session_start();
    if (!isset($_SESSION['CurrentUserName']))
    {
        if (isset($_GET['UserNameForSession']))
        {
            $_SESSION['CurrentUserName'] = $_GET['UserNameForSession'];  
            $_SESSION['StartTime'] = time();  
        }
    }
    if (isset($_SESSION['CurrentUserName']))
    {
        if ($_SESSION['StartTime'] + 60 < time()) 
        {
            session_unset();           
            session_destroy();
            /*session_write_close();
            setcookie(session_name(),'',0,'/');
            session_regenerate_id(true);*/
            echo "Your username is unknown because session was destroyed!";
        }
        else
        {
            echo "Your username is " . $_SESSION['CurrentUserName'];
        }
    }  
?>
