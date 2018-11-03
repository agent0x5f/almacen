<!DOCTYPE html>
<html lang="es">
<head>
	<meta charset="utf-8">
	<title>Bienvenido a la página web de El equipo Herrera</title>
	<?php require 'php/php.php'; ?>
	<link rel="stylesheet" type="text/css" href="css/css.css">
	<script type="text/javascript" src="js/js.js"></script>
</head>

<body>

	<header>
		<p id="titulo">El equipo Herrera</p> 
		<img id="img1" src="imagenes/h.png">
	</header>

	<nav>
		<ul class="menu">
			<li><a href="./index.php">Inicio </a></li>
  			<li><a href="./quienes_somos.html"> Quienes somos </a></li>
  			<li><a href="./contacto.html"> Contacto </a></li>
  			<li><a href="./servicios.html"> Servicios </a></li>
  			<li><a href="./contratar.php"> Contratar </a></li>
     		<li><a href="./chat.php"> Chat online </a></li> 
  			<li class="derecha"><a href="./registrarse.php"> Registrarse </a></li> 	
  			<li class="derecha"><a href="./login.php"> Login </a></li>		
		</ul>
	</nav>

	<section>
		<form action="./conectar.php" method="post">
			<fieldset>
				<legend>Login</legend>
				<br>
				<label>Nombre:</label>
				<input type="text" name="nombre" maxlength="50">
				<br>
				<label>Contraseña:</label>
				<input type="password" name="contra" maxlength="25">
				<br>
				<input type="submit" value="Enviar">
				<br>
				<br>
			</fieldset>

		</form>

	</section>

	<footer>
		<br>
		<br>
		<br>
		Hecho por Miguel Mateo Hernández Vargas
	</footer>

	<script type="text/javascript">
	var slideIndex = 0;
    carro();
    </script>

</body>
</html>