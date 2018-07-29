<?php

include_once("Info.php");

$POST = json_decode(file_get_contents("php://input"), true);

$query = "INSERT INTO weight VALUES (NULL, '" . date('Y-m-d', time()) . "', '" . $POST["Value"] . "', '" . $POST["User_ID"] . "');";

header("content-type: application/json");

if ($connection->query($query))
{
	echo "Insert weight OK";
}
else 
{
	echo "Error insert weight"; http_response_code(400);
}

@mysqli_close($connection);

?>