<?php

include_once("Info.php");

$query = $connection->query("SELECT ID, Surname, Name, Mail, Password, Gender, Height, Birthday, Status_ID FROM user WHERE Mail = '" . $_GET["Mail"] . "' AND Password = '" . $_GET["Password"] . "'");

$data = array();
while ($r = mysqli_fetch_assoc($query))
{
	$data[] = array("ID" => $r["ID"], "Surname" => $r["Surname"], "Name" => $r["Name"], "Mail" => $r["Mail"], "Password" => $r["Password"], "Gender" => $r["Gender"], "Height" => $r["Height"], "Birthday" => $r["Birthday"], "Status_ID" => $r["Status_ID"]);
}

header("content-type: application/json");
if (empty($data)) { echo "No result found"; http_response_code(400); } else { echo json_encode($data); }


@mysqli_close($connection);

?>