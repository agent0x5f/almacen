<?php
function get_admin()
{
$con = @mysqli_connect('localhost', 'mateo', '123', 'datos'); /*ip,user,pass,db*/

if (!$con) {
    echo "Error: " . mysqli_connect_error();
	exit();
}
else
{
	$sql 	= 'SELECT nombre,pass FROM usuarios where id=1';
	$query 	= mysqli_query($con, $sql);
	while ($row = mysqli_fetch_array($query))
	{
		print($row[0]);
		print("<br>");
		print($row[1]);
	}
}
// Close connection
mysqli_close ($con);
}

?>
