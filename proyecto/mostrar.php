<table id="t1" border="1" cellspacing=1 cellpadding=2 style="font-size: 8pt"><tr>
<td><font face="verdana"><b>ID</b></font></td>
<td><font face="verdana"><b>Usuario</b></font></td>
<td><font face="verdana"><b>Contraseña</b></font></td>
<td><font face="verdana"><b>Tipo</b></font></td>
</tr>

<?php  
$link = mysqli_connect("localhost", "mateo", "123", "datos");
$sql  = "SELECT id,nombre,pass,tipo FROM usuarios";
$query  = mysqli_query($link, $sql);
$numero = 0;

  while($row = mysqli_fetch_array($query))
  {
    echo "<tr><td width=\"25%\"><font face=\"verdana\">" . 
	    $row[0] . "</font></td>";
    echo "<td width=\"25%\"><font face=\"verdana\">" . 
	    $row[1] . "</font></td>";
    echo "<td width=\"25%\"><font face=\"verdana\">" . 
	    $row[2] . "</font></td>";
    echo "<td width=\"25%\"><font face=\"verdana\">" . 
	    $row[3]. "</font></td></tr>";    
    $numero++;
  }
  echo "<tr><td colspan=\"15\"><font face=\"verdana\"><b>Número de cuentas: " . $numero . 
      "</b></font></td></tr>";
  
  mysqli_free_result($query);
  mysqli_close($link);
?>
</table>
