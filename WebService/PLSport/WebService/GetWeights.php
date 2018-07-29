<?php

include_once("Info.php");

$query = $connection->query("SELECT ID, Date, Value FROM weight WHERE user_ID = '" . $_GET["UserID"] . "'");

$data = array();
while ($r = mysqli_fetch_assoc($query))
{
	$data[] = array("ID" => $r["ID"], "Date" => $r["Date"], "Value" => $r["Value"]);
}

header("content-type: application/json");
if (empty($data)) { echo "No result found"; http_response_code(400); } else { echo json_encode($data); }


@mysqli_close($connection);

?>