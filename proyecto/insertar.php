<?php

function registrar(){
$link = mysqli_connect("localhost", "mateo", "123", "datos");
 // Check connection
if($link === false){
    die("ERROR: Could not connect. " . mysqli_connect_error());
}
// Escape user inputs for security
$nombre = mysqli_real_escape_string($link, $_REQUEST['nombre']);
$contra = mysqli_real_escape_string($link, $_REQUEST['contra']);

//comprueba que el usuario no este registrado
$sql 	= "SELECT nombre FROM usuarios where nombre='$nombre'";
$query 	= mysqli_query($link, $sql);
$row = mysqli_fetch_array($query);

if ($row[0]==$nombre)
{
	print("Error-Ya existe un usuario con ese nombre");
}
//EL ELSE NO FUNCIONA !!!
if ($row[0]!=$nombre)
	{
		$sql2 = "INSERT INTO usuarios (nombre,pass) VALUES ('$nombre', '$contra')";
		$query2 = mysqli_query($link, $sql2);
		print("Usuario creado.");
	}
	 
mysqli_close($link);
}


function busca_tabla($user){
$con = @mysqli_connect('localhost', 'mateo', '123', 'datos'); /*ip,user,pass,db*/

if (!$con) {
    echo "Error: " . mysqli_connect_error();
	exit();
}
else
{
	$sql 	= "SELECT nombre,pass FROM usuarios where nombre='$user'";
	$query 	= mysqli_query($con, $sql);
	
		if ($query)
		print("Si esta");
		else
		print("No esta");
}
// Close connection
mysqli_close ($con);
}

registrar();
?>