#include <stdio.h>
#include <string.h>
#include <unistd.h>
int pid_b = 0;
char salida = 0;
int control = 0;
int nodos = 0;
int pos = 0;
int aux = 0;
int aux2 = 0;
int time = 1;
int menu_opcion =0;
int tiempo_rr = 1;
int tabla_tiempos[10][20];
char text[20];
int times_procesos[10];
struct proceso{
    char nombre[20];
    int prioridad; // 0=baja, 1=normal, 2=alta
    int duracion;  //tiempo que necesita el proceso
    int pid;
    int val; //me dice si el nodo se a creado
}lista[10];

void listar_procesos();
void listar_procesos_prio();
void agregar_proceso(struct proceso p);
void cambiar_prioridad(int pos,int pri);
void eliminar_proceso(int aux);
void cambiar_duracion(int pos, int tiempo);
void run();
void run2();
void run3();
int menu_fifo();
int menu_rr();
int menu_prio();
int main()
{
    int opcion=0;
    do{
        printf("---------------------------\n");
        printf("Administradores de procesos\n");
        printf("---------------------------\n");
        printf("1.-FIFO\n");
        printf("2.-RR\n");
        printf("3.-Prioridad\n");
        printf("4.-Salir\n");
        printf("Opcion?: ");
        scanf("%d",&menu_opcion);

        switch (menu_opcion) {
        case 1:
            menu_fifo();
            break;
        case 2:
            menu_rr();
            break;
        case 3:
            menu_prio();
            break;
        case 4:
            opcion=1;
            break;
        default:
            printf("Escoga una opcion(1-4)\n");
            break;
        }
    }while(opcion==0);
    return 0;
}

void listar_procesos(){
    int x=0;
    printf("Listando...\n");

    while(x<10){
    printf("Slot ");
    printf("%d",x);
    printf(": ");
    printf(lista[x].nombre);
    printf(" ,t: ");
    printf("%d",lista[x].duracion);
    printf("\n");
    x++;
    }
    printf("\n");

return;
}

void listar_procesos_prio(){
    int x=0;
    printf("Listando...\n");

    while(x<10){
    printf("Slot ");
    printf("%d",x);
    printf(": ");
    printf(lista[x].nombre);
    printf(" ,t: ");
    printf("%d",lista[x].duracion);
    printf(" ,X: %d",lista[x].prioridad);
    printf("\n");
    x++;
    }
    printf("\n");

return;
}

void agregar_proceso(struct proceso p){
    strcpy(lista[nodos].nombre,p.nombre);
    lista[nodos].prioridad = p.prioridad;
    lista[nodos].pid = pid_b;
    lista[nodos].val = 1;
    lista[nodos].duracion = 2;
    nodos++;
return;
}

void cambiar_duracion(int pos,int tiempo)
{
    lista[pos].duracion = tiempo;
}

void cambiar_prioridad(int pos,int pri){
    lista[pos].prioridad = pri;
return;
}

void eliminar_proceso(int aux){
    strcpy(lista[aux].nombre,"");
    lista[aux].prioridad = 0;
    lista[aux].pid = 0;
    lista[aux].val = 0;
    lista[aux].duracion = 0;
    nodos--;
return;
}

void run(){
    int total = 0;

    for(int x=0;x<10;x++)
        times_procesos[x]=lista[x].duracion;

    if(nodos==0)
        printf("Nada\n");
    else
    {
        printf("Nodos trabajando\n");
        while (nodos>0) {
            for(int k=0;k<10;k++)
            {
                if(lista[k].val==1)
                {
                    sleep(lista[k].duracion);
                    printf("Proceso termino\n");
                    eliminar_proceso(k);
                }
            }
        }
    }

    for(int x=0;x<10;x++)
    {
        int y=times_procesos[x];
        total = total + y;
        int z=total;
        while(y>0)
        {
            printf("#");
            y--;
        }
        printf("\n");
        while(z>0)
        {
            printf(" ");
            z--;
        }

    }
    printf("Time: %d\n",total);
}


void run2(){
    int total = 0;
    int j=0;

    for(int x=0;x<10;x++)
    {
        times_procesos[x]=lista[x].duracion;
        total=total+times_procesos[x];
    }

    if(nodos==0)
        printf("Nada\n");
    else
    {
        printf("Procesos trabajando\n");
        while (nodos>0) {
            for(int k=0;k<10;k++)
            {
                if(lista[k].val==1)
                {
                    tabla_tiempos[k][j]=1;
                    j++;
                    sleep(tiempo_rr);
                    lista[k].duracion = lista[k].duracion-tiempo_rr;
                    printf("P %d",k);
                    printf(" running\n");
                    if(lista[k].duracion <= 0)
                    {
                    printf("P %d",k);
                    printf(" termino\n");
                    eliminar_proceso(k);
                    }
                }
            }
        }
    }

    for(int x=0;x<10;x++)
    {
        for(int y=0;y<20;y++)
        {
            if(tabla_tiempos[x][y]==1)
                printf("#");
            else
                printf(" ");
        }
         printf("\n");
    }



    printf("Time: %d\n",total);
}


void run3(){
    int total = 0;
    int j=0;

    for(int x=0;x<10;x++){
        times_procesos[x]=lista[x].duracion;
        total=total+times_procesos[x];
    }

    if(nodos==0)
        printf("Nada\n");
    else{
        printf("Procesos trabajando\n");
        while (nodos>0) {
            for(int k=0;k<10;k++){
                if(lista[k].val==1){  //si el p existe
                    switch (lista[k].prioridad) { //tratalo de acuerdo a prio
                    case 0:
                        tabla_tiempos[k][j]=1;
                        j++;
                        sleep(tiempo_rr);
                        lista[k].duracion = lista[k].duracion-tiempo_rr;
                        break;
                    case 1:
                        tabla_tiempos[k][j]=1;
                        tabla_tiempos[k][j+1]=1;
                        j++;
                        j++;
                        sleep(tiempo_rr*2);
                        lista[k].duracion = lista[k].duracion-(tiempo_rr*2);
                        break;
                    case 2:
                        tabla_tiempos[k][j]=1;
                        tabla_tiempos[k][j+1]=1;
                        tabla_tiempos[k][j+2]=1;
                        j++;
                        j++;
                        j++;
                        sleep(tiempo_rr*3);
                        lista[k].duracion = lista[k].duracion-(tiempo_rr*3);
                        break;
                        printf("P %d",k);
                        printf(" running\n");
                    }
                    if(lista[k].duracion <= 0){
                    printf("P %d",k);
                    printf(" termino\n");
                    eliminar_proceso(k);
                    }
                }
            }
        }
    }

    for(int x=0;x<10;x++)
    {
        for(int y=0;y<20;y++)
        {
            if(tabla_tiempos[x][y]==1)
                printf("#");
            else
                printf(" ");
        }
         printf("\n");
    }

    printf("Time: %d\n",total);
}

int menu_fifo()
{
    do {
        printf("------------------------\n");
        printf("FIFO\n");
        printf("0-Listar procesos\n");
        printf("1-Agregar procesos\n");
        printf("2-Cambiar tiempo\n");
        printf("3-Matar proceso\n");
        printf("4-Simula\n");
        printf("5-Salir\n");
        printf("Opcion: ");
        scanf("%d",&control);
        printf("\n");

        switch (control) {
        case 0:
            printf("Listar procesos\n");
            listar_procesos();
            break;
        case 1:
            if(nodos>9)
                printf("Full\n");
            else{
                printf("Agregar proceso: \n");
                struct proceso p;
                printf("Nombre: ");
                scanf("%s",text);
                strcpy(p.nombre,text);
                p.pid = pid_b;
                p.prioridad = 1;
                agregar_proceso(p);
                pid_b++;
            }
            break;
        case 2:
            printf("Cambiar tiempo\n");
            printf("Slot: ");
            scanf("%d",&aux);
            if(lista[aux].val==0)
                printf("Error-No existe el proceso\n");
            else
            {
                printf("Duracion?(s): ");
                scanf("%d",&aux2);
                cambiar_duracion(aux,aux2);
            }
            break;
        case 3:
            printf("Matar proceso\n");
            printf("Numero: ");
            scanf("%d",&aux);
            eliminar_proceso(aux);
            pos--;
            break;
        case 4:
            run();
            break;
        case 5:
            printf("Saliendo\n");
            salida = 1;
            break;
        default:
            printf("Elija una opcion (0-5): ");
            scanf("%d",&control);
            break;
        }

    }while(salida==0);

    return 0;
}


int menu_rr()
{
    do {
        printf("------------------------\n");
        printf("RR\n");
        printf("0-Listar procesos\n");
        printf("1-Agregar procesos\n");
        printf("2-Cambiar tiempo rr\n");
        printf("3-Cambiar duracion proceso\n");
        printf("4-Matar proceso\n");
        printf("5-Simula\n");
        printf("6-Salir\n");
        printf("Opcion: ");
        scanf("%d",&control);
        printf("\n");

        switch (control) {
        case 0:
            printf("Listar procesos\n");
            listar_procesos();
            break;
        case 1:
            if(nodos>9)
                printf("Full\n");
            else{
                printf("Agregar proceso: \n");
                struct proceso p;
                printf("Nombre: ");
                scanf("%s",text);
                strcpy(p.nombre,text);
                p.pid = pid_b;
                p.prioridad = 1;
                agregar_proceso(p);
                pid_b++;
            }
            break;
        case 2:
            printf("Cambiar tiempo rr\n");
            printf("Duracion?(s): ");
            scanf("%d",&aux2);
            tiempo_rr = aux2;
            break;
        case 3:
            printf("Cambiar duracion proceso\n");
            printf("Slot: ");
            scanf("%d",&aux);
            if(lista[aux].val==0)
                printf("Error-No existe el proceso\n");
            else
            {
                printf("Duracion?(s): ");
                scanf("%d",&aux2);
                cambiar_duracion(aux,aux2);
            }
            break;
        case 4:
            printf("Matar proceso\n");
            printf("Numero: ");
            scanf("%d",&aux);
            eliminar_proceso(aux);
            pos--;
            break;
        case 5:
            run2();
            break;
        case 6:
            printf("Saliendo\n");
            salida = 1;
            break;
        default:
            printf("Elija una opcion(0-5): ");
            scanf("%d",&control);
            break;
        }

    }while(salida==0);
    return 0;
}

int menu_prio()
{
    do {
        printf("------------------------\n");
        printf("Prioridad\n");
        printf("0-Listar procesos\n");
        printf("1-Agregar procesos\n");
        printf("2-Cambiar prioridad a proceso\n");
        printf("3-Cambiar duracion de proceso\n");
        printf("4-Matar proceso\n");
        printf("5-Simula\n");
        printf("6-Salir\n");
        printf("Opcion: ");
        scanf("%d",&control);
        printf("\n");

        switch (control) {
        case 0:
            printf("Listar procesos\n");
            listar_procesos_prio();
            break;
        case 1:
            if(nodos>9)
                printf("Full\n");
            else{
                printf("Agregar proceso: \n");
                struct proceso p;
                printf("Nombre: ");
                scanf("%s",text);
                strcpy(p.nombre,text);
                p.pid = pid_b;
                p.prioridad = 1;
                agregar_proceso(p);
                pid_b++;
            }
            break;
        case 2:
            printf("Cambiar prioridad\n");
            printf("Slot: ");
            scanf("%d",&aux);
            if(lista[aux].val==0)
                printf("Error-No existe el proceso\n");
            else
            {
                printf("Prioridad?(0-2): ");
                scanf("%d",&aux2);
                cambiar_prioridad(aux,aux2);
            }
            break;
        case 3:
            printf("Cambiar tiempo\n");
            printf("Slot: ");
            scanf("%d",&aux);
            if(lista[aux].val==0)
                printf("Error-No existe el proceso\n");
            else
            {
                printf("Duracion?(s): ");
                scanf("%d",&aux2);
                cambiar_duracion(aux,aux2);
            }

            break;
        case 4:
            printf("Matar proceso\n");
            printf("Numero: ");
            scanf("%d",&aux);
            eliminar_proceso(aux);
            pos--;
            break;
        case 5:
            run3();
            break;
        case 6:
            printf("Saliendo\n");
            salida = 1;
            break;
        default:
            printf("Elija una opcion(0-5): ");
            break;
        }

    }while(salida==0);
    return 0;
}
