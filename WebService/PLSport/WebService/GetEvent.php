<?php

include_once("Info.php");

$query = $connection->query("SELECT e.ID, e.Name, e.Description, e.Date, e.Place, e.type_ID, u.Surname, e.owner_ID FROM event e, user u WHERE u.ID = e.owner_ID AND e.Date > '" . date("Y-m-d H:i:s") . "'");

$data = array();
while ($r = mysqli_fetch_assoc($query))
{
	$data[] = array("ID" => $r["ID"], "Name" => $r["Name"], "Description" => $r["Description"], "Date" => $r["Date"], "Place" => $r["Place"], "type_ID" => $r["type_ID"], "Surname" => $r["Surname"], "owner_ID" => $r["owner_ID"]);
}

header("content-type: application/json");
if (empty($data)) { echo "No result found"; } else { echo json_encode($data); }


@mysqli_close($connection);

?>