<?php

function eliminar(){
$link = mysqli_connect("localhost", "mateo", "123", "datos");
 // Check connection
if($link === false){
    die("ERROR: Could not connect. " . mysqli_connect_error());
}
// Escape user inputs for security
$nombre = mysqli_real_escape_string($link, $_REQUEST['nombre']);

//comprueba que el usuario no este registrado
$sql 	= "SELECT nombre FROM usuarios where nombre='$nombre'";
$query 	= mysqli_query($link, $sql);
$row = mysqli_fetch_array($query);

//EL ELSE NO FUNCIONA !!!
if ($row[0]!=$nombre)
{		
	print("Error-Usuario no existe");
}
if ($row[0]==$nombre)
{
	$sql2 = "DELETE FROM usuarios WHERE nombre='$nombre'";
	$query2 = mysqli_query($link, $sql2);
	print("Cuenta Eliminada");
	header("Location: panel.php");
	exit();
}

mysqli_close($link);
}


eliminar();
?>