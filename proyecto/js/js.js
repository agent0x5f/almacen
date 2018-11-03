function Login(){
var opcion = prompt("Desea logearse S/N?");

    if (opcion == 'S') {
        var persona = prompt("Nombre");
        var salario = prompt("Password");
        alert("Bienvenido");
    } 
    if (opcion == 'N') {
        alert("No esta conectado a su cuenta, para realizar compras o usar el chat, por favor, registrese o ingrese a su cuenta");
    } 
}

    function carro() {
    var i;
    var x = document.getElementsByClassName("galeria");
    for (i = 0; i < x.length; i++) {  //oculta las imagens
      x[i].style.display = "none";
    }

    slideIndex+=3;
    if (slideIndex+2 > x.length) {slideIndex = 0}
    x[slideIndex].style.display = "inline-block";
    x[slideIndex+1].style.display = "inline-block";
    x[slideIndex+2].style.display = "inline-block";
    setTimeout(carro, 1000); // Change image every second
    }