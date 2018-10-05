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

using namespace std;
extern int m[];
int m[256];
const int tam_datos=50;
string datos[tam_datos];
string tabla[50][3];
string nombre;

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
	int margen=10;
	int resto=0;
	for(int x=0;x<12*3;x++)
		cout<<"-";
	cout<<endl;
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
				cout<<"|*         |";
		}
		if(tabla[z][k].length()>0)
		cout<<endl;
	}
	cout<<endl;
}

void procesa_tabla()
{
	//separa las lineas de entrada en 2, palabra y variable
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

bool primera_pasada(string cad)
{
	bool ret=carga_archivo(cad);
	remueve_comentarios();
	quitar_lineas();
	quitar_espacios();
	todo_mayus();
	procesa_tabla();
	obten_tipo();
	muestra_tabla();
	return ret;
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
	case 13:cout<<"Nombre del archivo: ";
		cin>>nombre;
		primera_pasada(nombre); break;		
        default: break;
        }

    }while (opcion!=99);

}

int main(int argc, char *argv[])	
{
    if(argc==1)	//si se llama sin argumentos, se ejecuta el menu
    menu();
    if(argc==2) //si le paso el nombre del archivo a leer, se procesa y el resultado se guarda en pantalla y en su <nombre_archivo>.abc8
    primera_pasada(argv[1]);
    return 0;
}

