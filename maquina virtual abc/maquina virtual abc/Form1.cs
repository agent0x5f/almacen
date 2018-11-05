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
            int posy = 0;
            int posx = 0;
            for (int x = 0; x < aux.Length; x++)
            {
                int cont = 0;
                int ini = x;
                while (x<aux.Length && aux[x] != '\n')
                {
                    cont++;
                    x++;
                }
                dataGridView1.Rows[posx].Cells[posy].Value = (aux.Substring(ini, cont));
                posy++;
                if(posy==16)
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
