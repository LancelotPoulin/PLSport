<?php

include_once("Info.php");

$POST = json_decode(file_get_contents("php://input"), true);

$query = "INSERT INTO event VALUES (NULL, '" . $POST["Name"] . "', '" . $POST["Description"] . "', '" . $POST["Date"] . "', '" . $POST["Place"] . "', '" . $POST["Type_ID"] . "', '" . $POST["Owner_ID"] . "');";

header("content-type: application/json");

if ($connection->query($query))
{
	$query = $connection->query("SELECT ID FROM event WHERE Name = '" . $POST["Name"] . "' AND Description = '" . $POST["Description"] . "' AND Date = '" . $POST["Date"] . "' AND Place = '" . $POST["Place"] . "' AND Owner_ID = '" . $POST["Owner_ID"] . "' AND Type_ID = '" . $POST["Type_ID"] . "'");
	$data = 0;
	while ($r = mysqli_fetch_assoc($query))
	{
		$data = $r["ID"];
		echo "Insert Event OK|$data";
	}
}
else 
{
	echo "Error insert event"; http_response_code(400);
}

@mysqli_close($connection);

?>