<?php

include_once("Info.php");

$query = $connection->query("SELECT e.ID, e.Name, e.Description, e.Date, e.Place, e.type_ID, u.Surname, e.Owner_ID FROM event e, user u, user_has_event uhe WHERE e.ID = uhe.event_ID AND u.ID = e.owner_ID AND uhe.user_ID = '" . $_GET["UserID"] . "'");;

$data = array();
while ($r = mysqli_fetch_assoc($query))
{
	$data[] = array("ID" => $r["ID"], "Name" => $r["Name"], "Description" => $r["Description"], "Date" => $r["Date"], "Place" => $r["Place"], "type_ID" => $r["type_ID"], "Surname" => $r["Surname"], "Owner_ID" => $r["Owner_ID"]);
}

header("content-type: application/json");
if (empty($data)) { echo "No result found"; } else { echo json_encode($data); }


@mysqli_close($connection);

?>