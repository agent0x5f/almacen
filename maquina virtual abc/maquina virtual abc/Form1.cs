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
        bool mayor = true;
        bool menor = true;
        bool igual = true;
        bool cero = true;
        bool neg = true;
        int cp = 0;
        int acu = 0;
        int x = 0;
        int i_actual = 0;
        int i_actual_p = 0;

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
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            registro_acomulador.Text = acu.ToString();
            registro_x.Text = x.ToString();
            instruccion_actual.Text = i_actual.ToString();
            instruccion_op.Text = i_actual_p.ToString();
        }
        //ejecuta una linea de codigo
        private void Run1_Click(object sender, EventArgs e)
        {
            Actualiza();
        }
    }
}
