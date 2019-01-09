using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace proyectodba
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //pruebo la conexion a la db
            
            //DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1
            const string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            con.Open();

            string sql= "select usuario from cuentas where id = 1";
            OracleCommand comando = new OracleCommand(sql,con);

            OracleDataReader dr = comando.ExecuteReader();
            dr.Read();
            MessageBox.Show(dr[0].ToString());

            con.Dispose();
            
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
