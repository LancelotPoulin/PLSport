<?php

// POULIN Lancelot - 18/01/2018 - PLSport

$MySqlAdress= 'localhost';
$MySqlID = 'root';
$MySqlPW = '';
$MySqlDBName = 'plsport';

$connection = new mysqli($MySqlAdress, $MySqlID, $MySqlPW, $MySqlDBName) or die(mysqli_error())
	
?>
