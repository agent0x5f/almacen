/*
 * Ensamblador para el lenguaje asm-8
 * Realizado para la clase de programacion de sistemas
 * Miguel Mateo Hernández Vargas
 * Septiembre 2018
 */
#include <iostream>
using namespace std;
extern int m[];
int m[256];

void muestra_memoria()
{
    cout<<"MEMORIA"<<endl;
    for(int x=0;x<256;x++)
    m[x]=0;

    for(int x=0;x<16;x++)
    {
        cout<<x*16<<": ";
        for(int y=0;y<16;y++)
        {
            if(y % 4 == 0)
                cout<<" ";
            cout<<m[x+y];
        }
        cout<<endl;
    }
}

int main()
{
    muestra_memoria();
    return 0;
}
