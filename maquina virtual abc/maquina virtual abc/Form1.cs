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
        public Form1()
        {
            InitializeComponent();
        }   

        private void cargar_archivo_Click(object sender, EventArgs e)
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

        private void cargar_memoria_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
          
            for(int x=0;x<16;x++)
            dataGridView1.Rows.Add(x, 111);
            
            //llenar la memoria con 111
            for(int y = 0; y < 16; y++)
            for(int x = 0; x < 16; x++)
            {
                    dataGridView1.Rows[y].Cells[x].Value = 111;
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            //lee los valores del archivo    
            string aux = richTextBox1.Text;

            int dir_datos = 0;
            int num_datos = 0;
            int dir_codigo = 0;
            int valor_leido = 0;
            for (int x = 0; x < aux.Length || valor_leido==3; x++) //recorre el textbox
            {
                int cont = 0;
                int ini = x;
                while (x<aux.Length && aux[x] != '\n')
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


            //guarda en memoria los datos      
            int posx = 0;
            int posy = dir_datos; //solo funciona si dir_datos es menor que 16
            int z = 0;
            int agregados = 0;

            int k = 0;
            int saltos = 0;
              
                while (k < aux.Length && saltos<3) //ve a la 4ta cantidad
                {
                   if(aux[k]=='\n')
                    saltos++;

                k++;
                }
            
            for (int x = k; x < aux.Length && agregados<num_datos; x++) //recorre el textbox
            {
                int ini = x;
                int cont = 0;
           
                    while (x < aux.Length && aux[x] != '\n')
                    {
                        cont++;
                        x++;
                    }
                    z++;

                dataGridView1.Rows[posx].Cells[posy].Value = (aux.Substring(ini, cont));
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
            while(guardados>16)
            {
                guardados = guardados - 16;
                guardados_aux++;
            }
            posy = guardados;
            posx = guardados_aux; //solo funciona si dir_datos es menor que 16

            z = 0;
            agregados = 0;
            k = 0;
            saltos = 0;

            while (k < aux.Length && saltos < 3+num_datos) //ve a la Nva cantidad
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

                dataGridView1.Rows[posx].Cells[posy].Value = (aux.Substring(ini, cont));
                //se encarga de la coordenada en la matriz
                posy++;
                if (posy == 16)
                {
                    posx++;
                    posy = 0;
                }
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
