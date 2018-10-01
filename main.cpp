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
const int tam_datos=12;
string datos[tam_datos];

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

void carga_archivo()
{

    ifstream file("suma.asm8");
    if(file)
    {
        int x=0;
        while(x<tam_datos)
        {           
            getline(file,datos[x]);
            x++;
        }
    }
    else
    cout<<"Error-No se encontro el archivo"<<endl;
}

 void muestra_archivo()
 { 
 for(unsigned long int y=0;y<tam_datos;y++)
 {
 cout<<setw(3)<<y<<",c:"<<datos[y].length()<<'\t'<<"|";	 

	 for(int x=0;x<datos[y].length();x++)
	 cout<<datos[y][x];
 cout<<"|"<<endl;
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
		if(datos[x].size()== 0)//para las lineas vacias
		{
			datos[x]=datos[x+1];
			datos[x+1]="";
		}
	}
	for(int x=0;x<tam_datos;x++)
		cout<<x<<": "<<datos[x].length()<<endl;
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
	cout<<"9.-Salir"<<endl;
        cout<<"opcion?: ";
        cin>>opcion;

        switch (opcion) {
        case 1: carga_archivo(); break;
        case 2: muestra_archivo(); break;
        case 3: muestra_memoria(); break;
	case 4: remueve_comentarios(); break;
	case 5: quitar_lineas(); break;
        default: break;
        }

    }while (opcion!=9);

}

int main()
{
    menu();
    return 0;
}

