/*
 * Ensamblador para el lenguaje asm-8
 * Realizado para la clase de programacion de sistemas
 * Miguel Mateo Hernández Vargas
 * Septiembre 2018
 */
#include <iostream>
#include <iomanip>
#include <fstream>
using namespace std;
extern int m[];
int m[256];

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

void carga_archivo()
{
 ifstream file;
}

void menu()
{
    //se busca que luego se pueda llamar el programa desde la terminal
    //ej: ensamblador-8 programa1.asm8  --> programa1.abc8
    cout<<"Menu para administrar el ensamblador"<<endl;
    int opcion=0;
    do{
        cout<<"1.-Cargar archivo"<<endl;
        cout<<"2.-Mostrar memoria"<<endl;
        cout<<"9.-Salir"<<endl;
        cout<<"opcion?: ";
        cin>>opcion;

        switch (opcion) {
        case 1: break;
        case 2: muestra_memoria(); break;
        case 3: break;
        }

    }while (opcion!=9);

}

int main()
{
    menu();
    return 0;
}
