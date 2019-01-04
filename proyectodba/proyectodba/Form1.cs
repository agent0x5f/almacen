using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectodba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Boton_ok_Click(object sender, EventArgs e)
        {
            string id = texto_usuario.Text;
            string pass = texto_contra.Text;
            //llamada a la funcion de log-in

            //si se permite la entrada, cambiar a la interfaz
            tabControl1.SelectTab(1);
            //si no, mostrar mensaje de error
            MessageBox.Show("datos invalidos");
        }
    }
}
