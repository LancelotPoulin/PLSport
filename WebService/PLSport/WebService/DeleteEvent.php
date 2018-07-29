<?php

include_once("Info.php");

$POST = json_decode(file_get_contents("php://input"), true);

$query = "DELETE FROM user_has_event WHERE event_ID = '" . $_GET["Event_ID"] . "';";

header("content-type: application/json");

if ($connection->query($query))
{
	$connection->query("DELETE FROM event WHERE ID = '" . $_GET["Event_ID"] . "';");
	echo "Delete event OK";
}
else 
{
	echo "Error delete event"; http_response_code(400);
}

@mysqli_close($connection);

?>