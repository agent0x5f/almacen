/*
 * Ensamblador para el lenguaje asm-8
 * Realizado para la clase de programacion de sistemas
 * Miguel Mateo Hernández Vargas
 * Septiembre 2018
 */
#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <cmath>
using namespace std;
extern int m[];
int m[256];
const int tam_datos=100;
string datos[tam_datos];
string tabla[tam_datos][3];
string tabla_s[tam_datos][3];
string nombre;
int contador=0;
//se usan como apuntadores
int cdatos;  //contiene el valor de datos, ej: DATOS 10 --> 10
int ccodigo; //contiene el valor de codigo, ej:CODIGO 25 --> 25

//cdatos y ccodigo constantes
int cdatosf;
int ccodigof;

int pos_datos;
int pos_codigo;

void inicializa_memoria()
{
    for(int x=0;x<256;x++)
    m[x]=0;
}

void muestra_memoria()
{
    cout<<"========================="<<endl;
    cout<<"\t"<<"MEMORIA"<<endl;
    cout<<"========================="<<endl;

    for(int x=0;x<16;x++)
    {
        cout<<setw(3)<<x*16<<": ";
        for(int y=0;y<16;y++)
        {
            if(y % 4 == 0)
                cout<<" ";
            cout<<m[x+y];
        }
        cout<<endl;
    }
}
void limpia_datos()
{
    for(int x=0;x<tam_datos;x++)
    datos[x]="";
}

bool carga_archivo(string cad)
{
    ifstream file(cad);
    if(file)
    {
        int x=0;
        while(x<tam_datos)
        {           
            getline(file,datos[x]);
            x++;
        }
	return true;
    }
    else
    {
    cout<<"Error-No se encontro el archivo"<<endl;
    return false;
    }
}

 void muestra_archivo()
 { 
 for(unsigned long int y=0;y<tam_datos;y++)
 {
	if(datos[y].length()>0)
	{
	 cout<<setw(4)<<y<<",c:"<<datos[y].length()<<'\t'<<"|";	 
	 for(int x=0;x<datos[y].length();x++)
	 {
		 if(datos[y][x]==' ')
			cout<<"~";
		 else
	 		cout<<datos[y][x];
	 }
 	cout<<"|"<<endl;
 	}
 }
 }

void remueve_comentarios()
{
	for(int y=0;y<tam_datos;y++)
	for(int x=0;x<datos[y].length();x++)
	if(datos[y][x]==';')
	{
		datos[y].pop_back();
		remueve_comentarios();
	}
	
}

void quitar_lineas()
{
	for(int y=0;y<tam_datos-1;y++)
	for(int x=0;x<tam_datos-1;x++)
	{
		if(datos[x].size() <= 1 )//para las lineas vacias o saltos de linea
		{
			datos[x]=datos[x+1];
			datos[x+1]="";
		}
	}
//	for(int x=0;x<tam_datos;x++)
//		cout<<x<<": "<<datos[x].length()<<endl;
}

void quitar_espacios()
{
	for(int x=0;x<tam_datos;x++)
	{
		if(!datos[x].empty())//si no esta vacio
		{
			if(datos[x].back()==' ' or datos[x].back()=='\t' or datos[x].back()=='\n')//si  el ultimo es vacio
			{
			       datos[x].pop_back();	//quitalo
			       quitar_espacios();   //repite 
			}
			if(datos[x].front()==' ' or datos[x].front()=='\t' or datos[x].front()=='\n')//si el primero es vacio
			{  
				char aux;
				datos[x].erase(0,1);
				quitar_espacios();
			}
		}
	}
}

void todo_mayus()
{
	for(int y=0;y<tam_datos;y++)
	for(int x=0;x<datos[y].length();x++)
	{
		if((int)datos[y][x] >= 97 and (int)datos[y][x] <= 122) //si es una letra minuscula
			datos[y][x] = datos[y][x]-32;  //conviertela en mayus
	}
}

void muestra_tabla()
{
	int margen=20;
	int resto=0;

	cout<<"|-------NOMBRE-------||--------VALOR-------||--------TIPO--------|"<<endl;
	
	for(int z=0;z<tam_datos;z++)
	{
		int k;
		for(int y=0;y<3;y++)
		{ k=y;
			if(tabla[z][y].length()>0) //si existe la linea, imprimela con formato
			{
				cout<<'|';
				resto=margen-tabla[z][y].length();
				for(int x=0;x<tabla[z][y].length();x++)
				{	
						cout<<tabla[z][y][x];
				}
				for(int x=0;x<resto;x++)
					cout<<" ";
				cout<<'|';
			}
			else
				if(tabla[z][0].length()>0)
				cout<<"|*                   |";
		}
		if(tabla[z][k].length()>0)
		cout<<endl;
	}
	for(int x=0;x<66;x++)
			cout<<"-";
	cout<<endl;
}

void muestra_tabla_s()
{
	int margen=20;
	int resto=0;

	cout<<"|-------NOMBRE-------||-------SIMBOLO------||--------TIPO--------|"<<endl;
	
	for(int z=0;z<tam_datos;z++)
	{
		int k;
		for(int y=0;y<3;y++)
		{ k=y;
			if(tabla_s[z][y].length()>0) //si existe la linea, imprimela con formato
			{
				cout<<'|';
				resto=margen-tabla_s[z][y].length();
				for(int x=0;x<tabla_s[z][y].length();x++)
				{	
						cout<<tabla_s[z][y][x];
				}
				for(int x=0;x<resto;x++)
					cout<<" ";
				cout<<'|';
			}
			else
				if(tabla_s[z][0].length()>0)
				cout<<"|*                   |";
		}
		if(tabla_s[z][k].length()>0)
		cout<<endl;
	}
	for(int x=0;x<66;x++)
			cout<<"-";
	cout<<endl;
}

void procesa_tabla()
{
	//separa las lineas de entrada en 2, palabra y variable
	//necesito modificar para arreglos
	for(int x=0;x<tam_datos;x++) //recorre cada linea de datos
	{
		int pos= datos[x].find_first_of(' ',0);//posicion del espacio, pos es -1 si el string es vacio o si no hay ' '
		tabla[x][0].insert(0,datos[x].substr(0,pos));
	
		if(pos!=-1)	
		tabla[x][1].insert(0,datos[x].substr(pos+1,80));
	
	}
}

bool es_numero(string cad)
{
	if(cad[0]<0 or cad[0]>0)
	       return false;
	else
	       return true;	
}

int a_numero(string cad)
{
	return stoi(cad,nullptr,10);
}

bool es_reservada(string cad)
{
	return  (cad=="DATOS"||cad=="CODIGO");
}

int valor_instruccion(string cad)
{
	if(cad == "CARGAAI")
		return 1;
	if(cad == "CARGAAD")
		return 2;
	if(cad == "CARGAAX")
		return 3;
	if(cad == "GUARDAAD")
		return 4;
	if(cad == "GUARDAAX")
		return 5;
	if(cad == "SUMAAI")
		return 6;
	if(cad == "SUMAAD")
		return 7;
	if(cad == "SUMAAX")
		return 8;
	if(cad == "RESTAAI")
		return 9;
	if(cad == "RESTAAD")
		return 10;
	if(cad == "RESTAAX")
		return 11;
	if(cad == "INCA")
		return 12;
	if(cad == "DECA")
		return 13;
	if(cad == "COMPAI")
		return 14;
	if(cad == "COMPAD")
		return 15;
	if(cad == "COMPAX")
		return 16;
	if(cad == "NOTA")
		return 17;
	if(cad == "ANDAI")
		return 18;
	if(cad == "ANDAD")
		return 19;
	if(cad == "ANDAX")
		return 20;
	if(cad == "ORAI")
		return 21;
	if(cad == "ORAD")
		return 22;
	if(cad == "ORAX")
		return 23;
	if(cad == "SALTA")
		return 24;
	if(cad == "SALTA+")
		return 25;
	if(cad == "SALTA-")
		return 26;
	if(cad == "SALTA0")
		return 27;
	if(cad == "SALTAN0")
		return 28;
	if(cad == "SALTA=")
		return 29;
	if(cad == "SATALN=")
		return 30;
	if(cad == "SALTA>")
		return 31;
	if(cad == "SALTA<")
		return 32;
	if(cad == "SALTA>=")
		return 33;
	if(cad == "SALTA<=")
		return 34;
	if(cad == "CARDAXI")
		return 35;
	if(cad == "CARDAXD")
		return 36;
	if(cad == "GUARDAXD")
		return 37;
	if(cad == "INCX")
		return 38;
	if(cad == "DECX")
		return 39;
	if(cad == "COMPXI")
		return 40;
	if(cad == "COMPXD")
		return 41;
	else
		return -1;
}

void obten_tipo()
{
	for(int x=0;x<tam_datos;x++)
	{
		if(tabla[x][0].length()>0) //si existe la palabra
		{
			if(tabla[x][1].length()>0 and valor_instruccion(tabla[x][0])!=-1) //si tiene un valor y es una funcion
				tabla[x][2]="F";
			else if(es_reservada(tabla[x][0])) //si es una palabra reservada
			       tabla[x][2]="R";	
			else if(tabla[x][1].length()>0) //si tiene un valor y no es una funcion, es una variable
				tabla[x][2]="V";
			else//si no, es una eiqueta, #######comprobar como se comportan las etiquetas segun el diseño
				tabla[x][2]="E";
		}
	}
}

void procesa_reservadas()
{
	//obten el valor de datos y codigo
	for(int x=0;x<tam_datos;x++)
	{
		if(tabla[x][0]=="DATOS")
		{
			cdatos=stoi(tabla[x][1],nullptr,10);
			cdatosf=cdatos;
			pos_datos=x;
		}
		if(tabla[x][0]=="CODIGO")
		{
			ccodigo=stoi(tabla[x][1],nullptr,10);
			ccodigof=ccodigo;
			pos_codigo=x;
		}
	}
	//quitar las lineas que contienen a datos y a codigo
	//ya guarde sus valores en las variables globales
	for(int y=0;y<3;y++)
	{
		while(!tabla[pos_datos][y].empty())
		tabla[pos_datos][y].pop_back();

		while(!tabla[pos_codigo][y].empty())
		tabla[pos_codigo][y].pop_back();
	}
	//recorre las lineas vacias	
	quitar_lineas();
}

int largo_instruccion(string cad)
{
	//regresa la cantidad de bytes que requiere la instruccion para funcionar: 0, 1, 2
	if(cad == "CARGAAI")
		return 1;
	if(cad == "CARGAAD")
		return 2;
	if(cad == "CARGAAX")
		return 3;
	if(cad == "GUARDAAD")
		return 4;
	if(cad == "GUARDAAX")
		return 5;
	if(cad == "SUMAAI")
		return 6;
	if(cad == "SUMAAD")
		return 7;
	if(cad == "SUMAAX")
		return 8;
	if(cad == "RESTAAI")
		return 9;
	if(cad == "RESTAAD")
		return 10;
	if(cad == "RESTAAX")
		return 11;
	if(cad == "INCA")
		return 12;
	if(cad == "DECA")
		return 13;
	if(cad == "COMPAI")
		return 14;
	if(cad == "COMPAD")
		return 15;
	if(cad == "COMPAX")
		return 16;
	if(cad == "NOTA")
		return 17;
	if(cad == "ANDAI")
		return 18;
	if(cad == "ANDAD")
		return 19;
	if(cad == "ANDAX")
		return 20;
	if(cad == "ORAI")
		return 21;
	if(cad == "ORAD")
		return 22;
	if(cad == "ORAX")
		return 23;
	if(cad == "SALTA")
		return 24;
	if(cad == "SALTA+")
		return 25;
	if(cad == "SALTA-")
		return 26;
	if(cad == "SALTA0")
		return 27;
	if(cad == "SALTAN0")
		return 28;
	if(cad == "SALTA=")
		return 29;
	if(cad == "SATALN=")
		return 30;
	if(cad == "SALTA>")
		return 31;
	if(cad == "SALTA<")
		return 32;
	if(cad == "SALTA>=")
		return 33;
	if(cad == "SALTA<=")
		return 34;
	if(cad == "CARDAXI")
		return 35;
	if(cad == "CARDAXD")
		return 36;
	if(cad == "GUARDAXD")
		return 37;
	if(cad == "INCX")
		return 38;
	if(cad == "DECX")
		return 39;
	if(cad == "COMPXI")
		return 40;
	if(cad == "COMPXD")
		return 41;
	else
		return -1;
}

void calcula_funciones(int pos)
{
	//debo preguntar su tamaño y luego agregarlo con largo_instruccion
	tabla_s[pos][1]=to_string(ccodigo);
	tabla_s[pos][0]=tabla[pos][0];
	tabla_s[pos][2]=tabla[pos][2];
	ccodigo++;
}

void calcula_etiquetas(int pos)
{	//debo revisar por casos raros
	tabla_s[pos][1]=to_string(ccodigo);
	tabla_s[pos][0]=tabla[pos][0];
	tabla_s[pos][2]=tabla[pos][2];
}

void calcula_variables(int pos)
{
	//si es un arreglo
	//se le quita el ultimo y primer espacio al arreglo
	//se considera que solo hay un espacio entre cada valor despues.
	//quita los espacios laterales
	while(tabla[pos][1].front()==' ')
		tabla[pos][1].erase(0,1);		
	while(tabla[pos][1].back()==' ')
		tabla[pos][1].pop_back();
	
	if(tabla[pos][1].find_first_of(' ',0)==-1)//si eres una variable
	{
		tabla_s[pos][1]=to_string(cdatos);
		tabla_s[pos][0]=tabla[pos][0];
		tabla_s[pos][2]=tabla[pos][2];
		cdatos++;
	}else//es un arreglo
		//BUG: solo fuciona si los  valores en el son de un solo digito
	{	int aux=ceil(tabla[pos][1].length()/2); //obtengo la cantidad de valores del arreglo
		tabla_s[pos][1]=to_string(cdatos);	//coloco su pos inicial en memoria
		tabla_s[pos][0]=tabla[pos][0];		
		tabla_s[pos][2]=tabla[pos][2];
		cdatos+= aux;	//incremento el contador
	}
}

void calcula_direcciones()
{
	for(int x=0;x<tam_datos;x++)
	{
		if(tabla[x][2]=="F")
			calcula_funciones(x);
		if(tabla[x][2]=="E")
			calcula_etiquetas(x);
		if(tabla[x][2]=="V")
			calcula_variables(x);
	}
}

bool primera_pasada(string cad)
{
	bool ret=carga_archivo(cad);
	remueve_comentarios();
	quitar_lineas();
	quitar_espacios();
	todo_mayus();
	procesa_tabla();
	obten_tipo();
	procesa_reservadas();
	calcula_direcciones();
	cout<<"Tabla con valores"<<endl;
	muestra_tabla();
	cout<<"Tabla de simbolos"<<endl;
	muestra_tabla_s();
	return ret;
}

int numdatos()
{
	int contador=0;
	for(int x=0;x<tam_datos;x++)
	{
		if(tabla[x][2]=="V")
			contador++;
	}
	return contador;
}

int obten_direccion(string cad)
{
	int valor=-1;
	//cad es una variable,debo encontrarla y obtener su  direccion
	for(int x=0;x<tam_datos;x++)
	{
		if(tabla_s[x][0]==cad)
			valor=stoi(tabla_s[x][1],nullptr,10);
	}
	return valor;
}

void segunda_pasada(string cad)
{
	//genera el archivo .abc8 para la maquina virtual
	ofstream salida(cad);

	salida<<cdatosf<<endl;  //donde inicia en memoria los datos
	salida<<numdatos()<<endl; //cuantos datos son
	cout<<cdatosf<<endl;  //donde inicia en memoria los datos
	cout<<numdatos()<<endl; //cuantos datos son
	salida<<ccodigof<<endl; //donde inicia en memoria los codigos
	cout<<ccodigof<<endl; //donde inicia en memoria los codigos

    //cuales son cada uno de los datos
    for(int x=0;x<tam_datos;x++)
	{
		if(tabla[x][2]=="V")
		{
			cout<<tabla[x][1]<<endl;
			salida<<tabla[x][1]<<endl;	
		}
	}
	
	//cuales son cada uno de los codigos
	int dir=ccodigof;
    for(int x=0;x<tam_datos;x++)
	{		
		if(tabla[x][2]=="F")//BUG: debo revisar cuantos operadores usa la funcion,x ahora solo funciona de 1
		{
			/*
			Esto envia ala salida la pocision en la memoria en donde esta la funcion
			cout<<dir<<endl;
			cout<<obten_direccion(tabla[x][1])<<endl;
			salida<<dir<<endl;
			salida<<obten_direccion(tabla[x][1])<<endl;
			dir++;	
			*/
			//envia el id de la funcion
			cout<<valor_instruccion(tabla[x][0])<<endl;
			cout<<obten_direccion(tabla[x][1])<<endl;
			salida<<valor_instruccion(tabla[x][0])<<endl;
			salida<<obten_direccion(tabla[x][1])<<endl;
			dir++;
		}
		if(tabla[x][2]=="E")//si es una etiqueta ,solo apunta al que sigue
		{
			cout<<dir<<endl;
			salida<<dir<<endl;
		}
	}

	salida.close();
}

void menu()
{
    //se busca que luego se pueda llamar el programa desde la terminal
    //ej: ensamblador-8 programa1.asm8  --> programa1.abc8
    int opcion=0;
    do{
	cout<<"Menu"<<endl;    
        cout<<"1.-Cargar archivo"<<endl;
        cout<<"2.-Mostrar archivo"<<endl;
        cout<<"3.-Mostrar memoria"<<endl;
        cout<<"4.-Remover comentarios"<<endl;
	cout<<"5.-Quitar lineas vacias"<<endl;
	cout<<"6.-Quitar espacios"<<endl;
	cout<<"7.-Procesar entrada(hace 1,4,5,6,2)"<<endl;
	cout<<"8.-Todo a mayusculas"<<endl;
	cout<<"9.-Mostrar tabla"<<endl;
	cout<<"10.-Procesa tabla"<<endl;
	cout<<"11.-Prepara archivo(hace 1,4,5,6,8,10,9)"<<endl;
	cout<<"12.-Calcula el tipo de dato"<<endl;
	cout<<"13.-1ra pasada"<<endl;
	cout<<"14.-2da pasada"<<endl;
	cout<<"99.-Salir"<<endl;
        cout<<"opcion?: ";
        cin>>opcion;

        switch (opcion) {
		case 1: 
			cout<<"Nombre del archivo: ";
			cin>>nombre;
		carga_archivo(nombre); break;
        case 2: muestra_archivo(); break;
        case 3: muestra_memoria(); break;
	case 4: remueve_comentarios(); break;
	case 5: quitar_lineas(); break;
	case 6: quitar_espacios();break;
	case 7: cout<<"Nombre del archivo: ";
		cin>>nombre;
		carga_archivo(nombre);
		remueve_comentarios();
		quitar_lineas();
		quitar_espacios();
		muestra_archivo(); break;
	case 8: todo_mayus(); break;
	case 9: muestra_tabla(); break;
	case 10: procesa_tabla(); break;
	case 11: cout<<"Nombre del archivo: ";
		 cin>>nombre;
		 carga_archivo(nombre);
		remueve_comentarios();
		quitar_lineas();
       		quitar_espacios();
		todo_mayus();
		procesa_tabla();
		muestra_tabla(); break;
	case 12:obten_tipo(); break;
	case 13:
		cout<<"Nombre del archivo: ";
		cin>>nombre;
		primera_pasada(nombre); break;
	case 14:
		cout<<"Nombre del archivo: ";
		cin>>nombre;
		segunda_pasada(nombre); break;
        default: break;
        }

    }while (opcion!=99);

}

int main(int argc, char *argv[])	
{
    if(argc==1)	//si se llama sin argumentos, se ejecuta el menu
    menu();
    if(argc==3) //si le paso el nombre del archivo a leer, se procesa y el resultado se guarda en pantalla y en su <nombre_archivo>.abc8
    {
    primera_pasada(argv[1]);
    segunda_pasada(argv[2]);
	}
	else
		cout<<"Error.-Parametros incorrectos"<<endl;
    return 0;
}

