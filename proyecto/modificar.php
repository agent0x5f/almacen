<?php

function modificar(){
$link = mysqli_connect("localhost", "mateo", "123", "datos");
 // Check connection
if($link === false){
    die("ERROR: Could not connect. " . mysqli_connect_error());
}
// Escape user inputs for security
$nombre = mysqli_real_escape_string($link, $_REQUEST['nombre']);
$contra = mysqli_real_escape_string($link, $_REQUEST['contra']);
$tipo = mysqli_real_escape_string($link, $_REQUEST['tipo']);

//comprueba que el usuario no este registrado
$sql 	= "SELECT nombre FROM usuarios where nombre='$nombre'";
$query 	= mysqli_query($link, $sql);
$row = mysqli_fetch_array($query);

//EL ELSE NO FUNCIONA !!!
if ($row[0]!=$nombre)
	{
		print("Error-Usuario no encontrado");
	}

if ($row[0]==$nombre)
{
$sql 	= "UPDATE usuarios SET pass='$contra', tipo='$tipo' WHERE nombre='$nombre'";
$query 	= mysqli_query($link, $sql);
$row = mysqli_fetch_array($query);
print("Cuenta Modificada");
header("Location: panel.php");
exit();
}
	 
mysqli_close($link);
}


modificar();
?>