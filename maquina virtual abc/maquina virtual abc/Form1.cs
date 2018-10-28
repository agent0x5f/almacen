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
            //llenar la memoria con 111
            for(int x = 0; x < 256; x++)
            {
                dataGridView1.Rows.Add(x,1111);
            }
            //necesito que lea numeros de varios digitos.
            string aux = richTextBox1.Text;
            int pos = 0;
            for (int x = 0; x < aux.Length; x++)
            {
                int cont = 0;
                int ini = x;
                while (x<aux.Length && aux[x] != '\n')
                {
                    cont++;
                    x++;
                }
                dataGridView1.Rows[pos].Cells[1].Value = (aux.Substring(ini, cont));
                pos++;
            }
        }
    }
}
