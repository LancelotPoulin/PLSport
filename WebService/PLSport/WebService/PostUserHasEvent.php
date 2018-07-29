<?php

include_once("Info.php");

$POST = json_decode(file_get_contents("php://input"), true);

if ($POST["Type"] == "Insert")
{
	$query = "INSERT INTO user_has_event VALUES ('" . $POST["User_ID"] . "', '" . $POST["Event_ID"] . "');";
}
else
{
	$query = "DELETE FROM user_has_event WHERE user_ID = '" . $POST["User_ID"] . "' AND event_ID = '" . $POST["Event_ID"] . "';";
}

header("content-type: application/json");

if ($connection->query($query))
{	
	$query = $connection->query("SELECT Place FROM event WHERE ID = '" . $POST["Event_ID"] . "'");
	while ($r = mysqli_fetch_assoc($query))
	{
		$place = $r["Place"];
		if ($POST["Type"] == "Insert") { $place--; } else { $place++; }
		$connection->query("UPDATE event SET Place = '" . $place . "' WHERE ID = '" . $POST["Event_ID"] . "'");
		echo "Insert/Delete uhe OK";
	}
}
else 
{
	echo "Error insert/delete uhe"; http_response_code(400);
}

@mysqli_close($connection);

?>