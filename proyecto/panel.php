<!DOCTYPE html>
<html lang="es">
<head>
	<meta charset="utf-8">
	<title>Panel de control de cuentas</title>
	<?php require 'php/php.php'; ?>
	<link rel="stylesheet" type="text/css" href="css/css.css">
	<script type="text/javascript" src="js/js.js"></script>
</head>

<body>

	<header>
		<a href="index.php"><img id="img1" src="imagenes/titulo_web.png"></a>
	</header>

	<section>
		<form action="./insertar.php" method="post">
			<fieldset>
				<legend>Crear cuenta</legend>
				<br>
				<label>Nombre:</label>
				<input type="text" name="nombre" maxlength="50" required>
				<br>
				<label>Contraseña:</label>
				<input type="password" name="contra" maxlength="25" required>
				<br>
				<label>Tipo:</label>
				<input type="text" name="tipo" maxlength="25" required>
				<br>
				<input type="submit" value="Enviar">
				<br>
				<br>
			</fieldset>
		</form>
	</section>

	<section>
		<form action="./remover.php" method="post">
			<fieldset>
				<legend>Remover cuenta</legend>
				<br>
				<label>Nombre:</label>
				<input type="text" name="nombre" maxlength="50" required>
				<br>
				<input type="submit" value="Enviar">
				<br>
				<br>
			</fieldset>
		</form>
	</section>

	<section>
		<form action="./modificar.php" method="post">
			<fieldset>
				<legend>Modificar cuenta</legend>
				<br>
				<label>Nombre:</label>
				<input type="text" name="nombre" maxlength="50" required>
				<br>
				<label>Contraseña:</label>
				<input type="password" name="contra" maxlength="25" required>
				<br>
				<label>Tipo:</label>
				<input type="text" name="tipo" maxlength="25" required>
				<br>
				<input type="submit" value="Enviar">
				<br>
				<br>
			</fieldset>
		</form>
	</section>

		<section>
		<form action="./mostrar.php" method="post">
			<fieldset>
				<legend>Datos en la DB</legend>
				<br>				
				<input type="submit" value="Mostrar">
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