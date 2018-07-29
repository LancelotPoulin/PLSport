<?php

include_once("Info.php");

$POST = json_decode(file_get_contents("php://input"), true);

$query = "INSERT INTO user VALUES (NULL, '" . $POST["Mail"] . "', '" . $POST["Password"] . "', '" . $POST["Name"] . "', '" . $POST["Surname"] . "', '" . $POST["Gender"] . "', '" . $POST["Birthday"] . "', '" . $POST["Height"] . "', 1);";

header("content-type: application/json");

if ($connection->query($query))
{
	$query = $connection->query("SELECT ID FROM user WHERE Mail = '" . $POST["Mail"] . "';");
	$r = mysqli_fetch_assoc($query);
	$query = "INSERT INTO weight VALUES (NULL, '" . date('Y-m-d', time()) . "', '" . $POST["Weight"] . "', '" . $r["ID"] . "');";
	$connection->query($query);
	
	echo "Insert User OK";
}
else 
{
	echo "Error insert User"; http_response_code(400);
}

@mysqli_close($connection);

?>