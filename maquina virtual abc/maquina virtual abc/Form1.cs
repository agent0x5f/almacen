using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maquina_virtual_abc
{

    public partial class Form1 : Form
    {
        int[,] memoria = new int[16, 16];
        int dir_datos = 0;
        int num_datos = 0;
        int dir_codigo = 0;
        int valor_leido = 0;
        bool mayor = false;
        bool menor = false;
        bool igual = true;
        bool cero = false;
        bool neg = false;
        int cp = 0;
        int A = 0;
        int X = 0;
        string i_actual;
        int i_actual_op = 0;
        int i_actual_op_val = 0;
        int op = 0;
        bool primer_arranque = true;
        bool fin_archivo = false;

        void Procesa()
        {
            //lee los valores del archivo    
            string aux = richTextBox1.Text;
            dir_datos = 0;
            num_datos = 0;
            dir_codigo = 0;
            valor_leido = 0;

            for (int x = 0; x < aux.Length || valor_leido == 3; x++) //recorre el textbox
            {
                int cont = 0;
                int ini = x;
                while (x < aux.Length && aux[x] != '\n')
                {
                    cont++;
                    x++;
                }
                if (valor_leido == 0)
                    dir_datos = Convert.ToInt32(aux.Substring(ini, cont));
                if (valor_leido == 1)
                    num_datos = Convert.ToInt32(aux.Substring(ini, cont));
                if (valor_leido == 2)
                    dir_codigo = Convert.ToInt32(aux.Substring(ini, cont));

                valor_leido++;
            }
            int posy = dir_datos;
            int posx = 0;
            //guarda en memoria los datos      
            while (posy > 15)
            {
                posy = posy - 16;
                posx++;
            }
            
            int z = 0;
            int agregados = 0;

            int k = 0;
            int saltos = 0;

            while (k < aux.Length && saltos < 3) //ve a la 4ta cantidad
            {
                if (aux[k] == '\n')
                    saltos++;

                k++;
            }

            for (int x = k; x < aux.Length && agregados < num_datos; x++) //recorre el textbox
            {
                int ini = x;
                int cont = 0;

                while (x < aux.Length && aux[x] != '\n')
                {
                    cont++;
                    x++;
                }
                z++;
                memoria[posx, posy] = Int32.Parse(aux.Substring(ini, cont));
                agregados++;
                //se encarga de la coordenada en la matriz
                posy++;
                if (posy == 16)
                {
                    posx++;
                    posy = 0;
                }
            }

            //guarda en memoria las funciones   
            int guardados = dir_codigo;
            int guardados_aux = 0;
            while (guardados > 15)
            {
                guardados = guardados - 16;
                guardados_aux++;
            }
            posy = guardados;
            posx = guardados_aux; //solo funciona si dir_datos es menor que 16

            k = 0;
            saltos = 0;

            while (k < aux.Length && saltos < 3 + num_datos) //ve a la Nva cantidad
            {
                if (aux[k] == '\n')
                    saltos++;

                k++;
            }

            for (int x = k; x < aux.Length; x++) //recorre el textbox
            {
                int ini = x;
                int cont = 0;

                while (x < aux.Length && aux[x] != '\n')
                {
                    cont++;
                    x++;
                }
                z++;

                memoria[posx, posy] = Int32.Parse(aux.Substring(ini, cont));
                //se encarga de la coordenada en la matriz
                posy++;
                if (posy == 16)
                {
                    posx++;
                    posy = 0;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.Rows.Add(15);
        }   

        private void Cargar_archivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog lector = new OpenFileDialog();

            if (lector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(lector.FileName);
                richTextBox1.Text = (sr.ReadToEnd());
                sr.Close();
            }
        }

        private void Cargar_memoria_Click(object sender, EventArgs e)
        {
            //en caso de reuso en tiempo de ejecucion esto limpia todas las variables usables
            //al momento de cargar un nuevo archivo hacia la memoria
            dir_datos = 0;
            num_datos = 0;
            dir_codigo = 0;
            valor_leido = 0;
            mayor = false;
            menor = false;
            igual = true;
            cero = false;
            neg = false;
            cp = 0;
            A = 0;
            X = 0;
            op = 0;
            i_actual = "";
            i_actual_op = 0;
            i_actual_op_val = 0;
            primer_arranque = true;
            fin_archivo = false;
            //inicializa la variable matriz
            for (int x = 0; x < memoria.GetLength(0); x++)
                for (int y = 0; y < memoria.GetLength(1); y++)
                    memoria[x, y] = 999;

            //magia
            Procesa();

            //escribe a la tabla
            for (int x = 0; x < memoria.GetLength(0); x++)
                for (int y = 0; y < memoria.GetLength(1); y++)
                    dataGridView1.Rows[x].Cells[y].Value = memoria[x, y];

            //cambia el color a negro de la memoria sin usar
            for (int x = 0; x < memoria.GetLength(0); x++)
                for (int y = 0; y < memoria.GetLength(1); y++)
                    if(memoria[x,y] == 999)
                        dataGridView1.Rows[x].Cells[y].Style.BackColor = Color.Black;
                   else
                        dataGridView1.Rows[x].Cells[y].Style.BackColor = Color.White;
            //cp = dir_codigo;

           Actualiza();
        }

        void Actualiza()
        {
            //actualiza la ventana con el estado actual de los registros
            bandera_cero.Checked = cero;
            bandera_negativo.Checked = neg;
            bandera_mayor.Checked = mayor;
            bandera_menor.Checked = menor;
            bandera_igual.Checked = igual;
            registro_cp.Text = cp.ToString();
            registro_acomulador.Text = A.ToString();
            registro_x.Text = X.ToString();
            instruccion_actual.Text = i_actual.ToString();
            instruccion_op.Text = i_actual_op.ToString();
            instruccion_op_val.Text = i_actual_op_val.ToString();

            for (int x = 0; x < 16; x++)
                for (int y = 0; y < 16; y++)
                    dataGridView1.Rows[x].Cells[y].Value = memoria[x, y];
        }

        void reset_flags()
        {//igual=mayor=menor=0
            igual = false;
            mayor = false;
            menor = false;
        }

        bool tiene_op(int f)
        {
            bool flag = true;
            switch (f)
            {
                case 1:
                    flag = true;
                    break;
                case 2:
                    flag = true;
                    break;
                case 3:
                    flag = false;
                    break;
                case 4:
                    flag = true;
                    break;
                case 5:
                    flag = false;
                    break;
                case 6:
                    flag = true;
                    break;
                case 7:
                    flag = true;
                    break;
                case 8:
                    flag = false;
                    break;
                case 9:
                    flag = true;
                    break;
                case 10:
                    flag = true;
                    break;
                case 11:
                    flag = false;
                    break;
                case 12:
                    flag = false;
                    break;
                case 13:
                    flag = false;
                    break;
                case 14:
                    flag = true;
                    break;
                case 15:
                    flag = true;
                    break;
                case 16:
                    flag = false;
                    break;
                case 17:
                    flag = false;
                    break;
                case 18:
                    flag = true;
                    break;
                case 19:
                    flag = true;
                    break;
                case 20:
                    flag = false;
                    break;
                case 21:
                    flag = true;
                    break;
                case 22:
                    flag = true;
                    break;
                case 23:
                    flag = false;
                    break;
                case 24:
                    flag = true;
                    break;
                case 25:
                    flag = true;
                    break;
                case 26:
                    flag = true;
                    break;
                case 27:
                    flag = true;
                    break;
                case 28:
                    flag = true;
                    break;
                case 29:
                    flag = true;
                    break;
                case 30:
                    flag = true;
                    break;
                case 31:
                    flag = true;
                    break;
                case 32:
                    flag = true;
                    break;
                case 33:
                    flag = true;
                    break;
                case 34:
                    flag = true;
                    break;
                case 35:
                    flag = true;
                    break;
                case 36:
                    flag = true;
                    break;
                case 37:
                    flag = true;
                    break;
                case 38:
                    flag = false;
                    break;
                case 39:
                    flag = false;
                    break;
                case 40:
                    flag = true;
                    break;
                case 41:
                    flag = true;
                    break;
            }
            return flag;
        }

        void procesa_funcion()
        {
            i_actual = Nombre_instruccion(tom(cp));

            if (tiene_op(tom(cp)) == true)
                i_actual_op = tom(cp + 1);
            else
                i_actual_op = 0;

            //tiene detalles dependiendo de alguna funcion que se me escapara de debugear..
            if (cp + 1 != 999 && tom(cp + 1) != 999 && tiene_op(tom(cp)) == true)
                i_actual_op_val = tom(tom(cp + 1));
            else
                i_actual_op_val = 0;

            switch (tom(cp))
            {               
                //cargaAI
                case 1:
                    op = tom(cp + 1);
                    A = op;
                    cp++;
                    break;
                //cargaAD
                case 2:
                    op = tom(cp + 1);
                    A = tom(op);
                    cp++;
                    break;
                //cargaAX
                case 3:
                    A = tom(tom(X));
                    break;
                //guardaAD
                case 4:
                    op = tom(cp + 1);
                    A = tom(op);
                    cp++;
                    break;
                //guardaAX
                case 5:
                    A=tom(X);
                    break;
                //sumaAI
                case 6:
                    op = tom(cp + 1);
                    A = A + op;
                    cp++;
                    break;
                //sumaAD
                case 7:
                    op = tom(cp + 1);
                    A = A + tom(op);
                    cp++;
                    break;
                //sumaAX
                case 8:
                    A = A + tom(tom(X));
                    break;
                //restaAI
                case 9:
                    op = tom(cp + 1);
                    A = A - op;
                    cp++;
                    break;
                //restaAD
                case 10:
                    op = tom(cp + 1);
                    A = A - tom(op);
                    cp++;
                    break;
                //restaAX
                case 11:
                    A = A - tom(tom(X));
                    break;
                //incA
                case 12:
                    A++;
                    break;
                //decA
                case 13:
                    A--;
                    break;
                //compAI
                case 14:
                    op = tom(cp + 1);
                    reset_flags();//igual=mayor=menor=0
                    if (A == op) igual = true;
                    if (A > op) mayor = true;
                    if (A < op) menor = true;
                    cp++;
                    break;
                //compAD
                case 15:
                    op = tom(cp + 1);
                    reset_flags();//igual=mayor=menor=0
                    if (A == tom(op)) igual = true;
                    if (A > tom(op)) mayor = true;
                    if (A < tom(op)) menor = true;
                    cp++;
                    break;
                //compAX
                case 16:
                    reset_flags();//igual=mayor=menor=0
                    if (A == tom(tom(X))) igual = true;
                    if (A > tom(tom(X))) mayor = true;
                    if (A < tom(tom(X))) menor = true;
                    break;
                //notA
                case 17:
                    if (A == 1)
                        A = 0;
                    if (A == 0)
                        A = 1;
                    break;
                //andAI
                case 18:
                    op = tom(cp + 1);
                    if (A == 1 && op == 1)
                        A = 1;
                    else
                        A = 0;
                    cp++;
                    break;
                //andAD
                case 19:
                    op = tom(cp + 1);
                    if (A == 1 && tom(op) == 1)
                        A = 1;
                    else
                        A = 0;
                    cp++;
                    break;
                //andAX
                case 20:
                    if (A == 1 && tom(tom(X)) == 1)
                        A = 1;
                    else
                        A = 0;
                    break;
                //orAI
                case 21:
                    op = tom(cp + 1);
                    if (A == 1 || op == 1)
                        A = 1;
                    else
                        A = 0;
                    cp++;
                    break;
                //orAD
                case 22:
                    op = tom(cp + 1);
                    if (A == 1 || tom(op) == 1)
                        A = 1;
                    else
                        A = 0;
                    cp++;
                    break;
                //orAX
                case 23:
                    if (A == 1 || tom(tom(X)) == 1)
                        A = 1;
                    else
                        A = 0;
                    break;
                //salta
                case 24:
                    op = tom(cp + 1);
                    cp = op;
                    cp++;
                    break;
                //salta+
                case 25:
                    op = tom(cp + 1);
                    if (neg == false) cp = op;
                    cp++;
                    break;
                //salta-
                case 26:
                    op = tom(cp + 1);
                    if (neg == true) cp = op;
                    cp++;
                    break;
                //salta0
                case 27:
                    op = tom(cp + 1);
                    if (cero == true) cp = op;
                    cp++;
                    break;
                //saltaN0
                case 28:
                    op = tom(cp + 1);
                    if (cero == false) cp = op;
                    cp++;
                    break;
                //salta=
                case 29:
                    op = tom(cp + 1);
                    if (igual == true) cp = op;
                    cp++;
                    break;
                //saltaN=
                case 30:
                    op = tom(cp + 1);
                    if (igual == false) cp = op;
                    cp++;
                    break;
                //salta>
                case 31:
                    op = tom(cp + 1);
                    if (mayor == true) cp = op;
                    cp++;
                    break;
                //salta<
                case 32:
                    op = tom(cp + 1);
                    if (menor == true) cp = op;
                    cp++;
                    break;
                //salta>=
                case 33:
                    op = tom(cp + 1);
                    if (mayor == true || igual == true) cp = op;
                    cp++;
                    break;
                //salta<=
                case 34:
                    op = tom(cp + 1);
                    if (menor == true || igual == true) cp = op;
                    cp++;
                    break;
                //cargaXI
                case 35:
                    op = tom(cp + 1);
                    X = op;
                    cp++;
                    break;
                //cargaXD
                case 36:
                    op = tom(cp + 1);
                    X = tom(op);
                    cp++;
                    break;
                //guardaXD
                case 37:
                    op = tom(cp + 1);
                    mot(op,X);
                    cp++;
                    break;
                //incX
                case 38:
                    X++;
                    break;
                //decX
                case 39:
                    X--;
                    break;
                //compXI
                case 40:
                    op = tom(cp + 1);
                    reset_flags();//igual=mayor=menor=0
                    if (X == op) igual = true;
                    if (X > op) mayor = true;
                    if (X < op) menor = true;
                    cp++;
                    break;
                //compXD
                case 41:
                    op = tom(cp + 1);
                    reset_flags();//igual=mayor=menor=0
                    if (X == tom(op)) igual = true;
                    if (X > tom(op)) mayor = true;
                    if (X < tom(op)) menor = true;
                    cp++;
                    break;
            }
            cp++;
        }

        //problema: A= mem[X]
        //ahora seran A= tom(X)
        //etom pondra en la matriz el valor en la pocision X equivalente
        int tom(int pos)
        {
            int p = pos;
            int px = 0;
            int py = 0;
            while(p>15)
            {
                p = p - 16;
                py++;
            }
            px = p;
            return memoria[py, px];
        }

        //problema mem[X]=A
        //ahora sera mot(X A)
        void mot(int j,int k)
        {
            int p = j;
            int px = 0;
            int py = 0;
            while (p > 15)
            {
                p = p - 16;
                py++;
            }
            px = p;
            memoria[px, py] = k;
        }

        string Nombre_instruccion(int cad)
        {
            if (cad == 1)
                return "CARGAAI";
            if (cad == 2)
                return "CARGAAD";
            if (cad == 3)
                return "CARGAAX";
            if (cad == 4)
                return "GUARDAAD";
            if (cad == 5)
                return "GUARDAAX";
            if (cad == 6)
                return "SUMAAI";
            if (cad == 7)
                return "SUMAAD";
            if (cad == 8)
                return "SUMAAX";
            if (cad == 9)
                return "RESTAAI";
            if (cad == 10)
                return "RESTAAD";
            if (cad == 11)
                return "RESTAAX";
            if (cad == 12)
                return "INCA";
            if (cad == 13)
                return "DECA";
            if (cad == 14)
                return "COMPAI";
            if (cad == 15)
                return "COMPAD";
            if (cad == 16)
                return "COMPAX";
            if (cad == 17)
                return "NOTA";
            if (cad == 18)
                return "ANDAI";
            if (cad == 19)
                return "ANDAD";
            if (cad == 20)
                return "ANDAX";
            if (cad == 21)
                return "ORAI";
            if (cad == 22)
                return "ORAD";
            if (cad == 23)
                return "ORAX";
            if (cad == 24)
                return "SALTA";
            if (cad == 25)
                return "SALTA+";
            if (cad == 26)
                return "SALTA-";
            if (cad == 27)
                return "SALTA0";
            if (cad == 28)
                return "SALTAN0";
            if (cad == 29)
                return "SALTA=";
            if (cad == 30)
                return "SATALN=";
            if (cad == 31)
                return "SALTA>";
            if (cad == 32)
                return "SALTA<";
            if (cad == 33)
                return "SALTA>=";
            if (cad == 34)
                return "SALTA<=";
            if (cad == 35)
                return "CARDAXI";
            if (cad == 36)
                return "CARDAXD";
            if (cad == 37)
                return "GUARDAXD";
            if (cad == 38)
                return "INCX";
            if (cad == 39)
                return "DECX";
            if (cad == 40)
                return "COMPXI";
            if (cad == 41)
                return "COMPXD";
            else
                return "MISSIGNO";
        }

        //ejecuta una linea de codigo
        void ejecuta1()
        {
            if (primer_arranque == true)
            {
                cp = dir_codigo;               
                procesa_funcion();
                primer_arranque = false;
            }
            else
            {
                if (tom(cp) != 999)
                    procesa_funcion();
                else
                {
                    MessageBox.Show("Fin de archivo");
                    fin_archivo = true;
                }
            }
        }
        private void Run1_Click(object sender, EventArgs e)
        {
           ejecuta1();
           Actualiza();
        }

        private void run_all_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
                MessageBox.Show("Fin de archivo/Seguro que cargó el archivo?");
            else
            {
                while (fin_archivo==false)
                    ejecuta1();
                Actualiza();
            }
        }
    }
}
