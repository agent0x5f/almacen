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


function busca_tabla(){
$con = @mysqli_connect('localhost', 'mateo', '123', 'datos'); /*ip,user,pass,db*/
$nombre = mysqli_real_escape_string($con, $_REQUEST['nombre']);
$contra = mysqli_real_escape_string($con, $_REQUEST['contra']);

if (!$con) {
    echo "Error: " . mysqli_connect_error();
	exit();
}
else
{
	$sql 	= "SELECT nombre,pass,tipo FROM usuarios where nombre='$nombre' and pass='$contra'";
	$query 	= mysqli_query($con, $sql);	
	$row = mysqli_fetch_array($query);

	if ($row[0]==$nombre and $row[1]==$contra)
		{
		if($row[2]==2) //si es admin
		{
			header("Location: panel.php");
			exit();
		}	
		else
			{
			header("Location: index.php");
			exit();
			}
		}
	else
		print("Error-No esta registrado/Contraseña invalida");
}
// Close connection
mysqli_close ($con);
}

busca_tabla();
?>