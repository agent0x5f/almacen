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
        string dato_puntero;
        int control_activo = 0;
        public Form1()
        {
            InitializeComponent();           
        }
 
        private void Boton_ok_Click(object sender, EventArgs e)
        {
            string user = texto_usuario.Text;
            string pass = texto_contra.Text;
            //llamada a la funcion de log-in
            const string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            con.Open();
            string sql = "select pass,admin from cuentas where usuario = '"+ user +"'";
            OracleCommand comando = new OracleCommand(sql, con);
            OracleDataReader dr = comando.ExecuteReader();

            //si esta vacio
            if (user == "" || pass == "")
                MessageBox.Show("Insertar datos");
            else
            while (dr.Read())
            {
                    //si se permite la entrada, cambiar a la interfaz correspondiente        
                    if (dr[0].ToString() == pass)
                    {
                        if (dr[1].ToString() == "SI")
                        {
                            tabControl1.SelectTab(2);
                            groupBox1.Visible = true;
                            boton_regresar_servidor.Visible = true;
                            boton_regresar_cliente.Visible = false;
                        }
                        else
                        {
                            tabControl1.SelectTab(1);
                            groupBox1.Visible = false;
                            boton_regresar_servidor.Visible = false;
                            boton_regresar_cliente.Visible = true;
                        }
                    }
                    else
                        MessageBox.Show("Error en datos");
            }

            con.Dispose();          
                  
        }

        private void Crear_viaje_Click(object sender, EventArgs e)
        {
            //comprobar la entrada
            if (texto_origen.Text == "" || texto_destino.Text == "")
                MessageBox.Show("Error-Rellenar los datos primero");
            else
            {
                string sql = "insertar_viaje";
                string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
                OracleConnection con = new OracleConnection(conexion);
                OracleCommand comando = new OracleCommand(sql, con)
                {
                    BindByName = true,
                    CommandType = CommandType.StoredProcedure
                };
                //se asignan los parametros de la funcion/procedimiento
                var prm1 = new OracleParameter("iorigen", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_origen.Text };
                comando.Parameters.Add(prm1);
                var prm2 = new OracleParameter("idestino", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_destino.Text };
                comando.Parameters.Add(prm2);
                var prm3 = new OracleParameter("ifecha", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = fecha_picker.Value.ToString("dd,MM,yyyy") };
                comando.Parameters.Add(prm3);
                var prm4 = new OracleParameter("ihora", OracleDbType.Int32, 10, ParameterDirection.Input) { Value = texto_hora.Value.ToString() };
                comando.Parameters.Add(prm4);
                var prm5 = new OracleParameter("icapacidad", OracleDbType.Int32, 10, ParameterDirection.Input) { Value = texto_capacidad.Value.ToString() };
                comando.Parameters.Add(prm5);
                //se crea el valor de retorno para la funcion
                var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
                comando.Parameters.Add(returnVal);

                comando.Connection.Open();
                comando.ExecuteNonQuery();

                if (returnVal.Value.ToString() == "1")
                    MessageBox.Show("OK");
                else
                    MessageBox.Show("Error-Ya existe");
                comando.Connection.Close();
            }
        }

        private void Boton_agregar_personal_Click(object sender, EventArgs e)
        {
            string derechos;

            if (check_admin.Checked == true)
                derechos = "SI";
            else
                derechos = "NO";
            //comprobar la entrada
            if (texto_user.Text == "" || texto_pass.Text == "")
                MessageBox.Show("Error-Rellenar los datos primero");
            else
            {
                string sql = "insertar_cuenta";
                string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
                OracleConnection con = new OracleConnection(conexion);
                OracleCommand comando = new OracleCommand(sql, con)
                {
                    BindByName = true,
                    CommandType = CommandType.StoredProcedure
                };
                //se asignan los parametros de la funcion/procedimiento
                var prm1 = new OracleParameter("nombre", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_user.Text };
                comando.Parameters.Add(prm1);
                var prm2 = new OracleParameter("contra", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_pass.Text };
                comando.Parameters.Add(prm2);
                var prm3 = new OracleParameter("tipo", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = derechos };
                comando.Parameters.Add(prm3);
                //se crea el valor de retorno para la funcion
                var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
                comando.Parameters.Add(returnVal);

                comando.Connection.Open();
                comando.ExecuteNonQuery();

                if (returnVal.Value.ToString() == "0")
                    MessageBox.Show("OK");
                else
                    MessageBox.Show("Error-Ya existe");
                comando.Connection.Close();
            }
        }

        private void Boton_mostrar_cuentas_Click(object sender, EventArgs e)
        {
            //muestra en la tabla las cuentas de personal registradas
            //aun usa sql directo, si se necesita cambiar a embedido
            string sql = "select id,usuario,pass,admin from cuentas";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con);

            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            tabla_cuentas.DataSource = dt;
            con.Close();
        }

        private void Boton_eliminar_cuenta_Click(object sender, EventArgs e)
        {
            string sql = "eliminar_cuenta";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con)
            {
                BindByName = true,
                CommandType = CommandType.StoredProcedure
            };
            //se asignan los parametros de la funcion/procedimiento
            var prm1 = new OracleParameter("nombre", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = dato_puntero };
            comando.Parameters.Add(prm1);
            //se crea el valor de retorno para la funcion
            var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
            comando.Parameters.Add(returnVal);

            comando.Connection.Open();
            comando.ExecuteNonQuery();

            if (returnVal.Value.ToString() == "1")
                MessageBox.Show("OK");
            else
                MessageBox.Show("Error");
            comando.Connection.Close();
        }

        private void Boton_modificar_cuenta_Click(object sender, EventArgs e)
        {
            string sql = "cambiar_cuenta";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con)
            {
                BindByName = true,
                CommandType = CommandType.StoredProcedure
            };
            //se asignan los parametros de la funcion/procedimiento
            var prm1 = new OracleParameter("nref", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = dato_puntero };
            comando.Parameters.Add(prm1);
            var prm2 = new OracleParameter("inombre", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_user.Text };
            comando.Parameters.Add(prm2);
            var prm3 = new OracleParameter("icontra", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_pass.Text };
            comando.Parameters.Add(prm3);
            //se crea el valor de retorno para la funcion
            var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
            comando.Parameters.Add(returnVal);

            comando.Connection.Open();
            comando.ExecuteNonQuery();

            if (returnVal.Value.ToString() == "1")
                MessageBox.Show("OK");
            else
                MessageBox.Show("Error");
            comando.Connection.Close();
        }

        private void Tabla_cuentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                if (e.ColumnIndex != -1)
                {
                    DataGridViewCell cell = (DataGridViewCell)tabla_cuentas.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dato_puntero = tabla_cuentas.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (control_activo == 1)
                    {
                        texto_user.Text = tabla_cuentas.Rows[e.RowIndex].Cells[1].Value.ToString();
                        texto_pass.Text = tabla_cuentas.Rows[e.RowIndex].Cells[2].Value.ToString();
                    }
                    if(control_activo == 3)
                    {
                        texto_clientes_nombre.Text = tabla_cuentas.Rows[e.RowIndex].Cells[1].Value.ToString();
                        texto_clientes_ap.Text = tabla_cuentas.Rows[e.RowIndex].Cells[2].Value.ToString();
                        texto_clientes_am.Text = tabla_cuentas.Rows[e.RowIndex].Cells[3].Value.ToString();
                        texto_clientes_is.Text = tabla_cuentas.Rows[e.RowIndex].Cells[4].Value.ToString();
                    }
                   
                }
            }

        }
     
        private void Boton_clientes_agregar_Click(object sender, EventArgs e)
        {
            //comprobar la entrada
            //solo requiero de nombre y primer apellido
            if (texto_clientes_nombre.Text == "" || texto_clientes_ap.Text == "")
                MessageBox.Show("Error-Rellenar los datos primero");
            else
            {
                string sql = "insertar_cliente";
                string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
                OracleConnection con = new OracleConnection(conexion);
                OracleCommand comando = new OracleCommand(sql, con)
                {
                    BindByName = true,
                    CommandType = CommandType.StoredProcedure
                };
                //se asignan los parametros de la funcion/procedimiento
                var prm1 = new OracleParameter("inombre", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_nombre.Text };
                comando.Parameters.Add(prm1);
                var prm2 = new OracleParameter("iapep", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_ap.Text };
                comando.Parameters.Add(prm2);
                var prm3 = new OracleParameter("iapem", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_am.Text };
                comando.Parameters.Add(prm3);
                var prm4 = new OracleParameter("issn", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_is.Text };
                comando.Parameters.Add(prm4);
                //se crea el valor de retorno para la funcion
                var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
                comando.Parameters.Add(returnVal);

                comando.Connection.Open();
                comando.ExecuteNonQuery();

                if (returnVal.Value.ToString() == "0")
                    MessageBox.Show("OK");
                else
                    MessageBox.Show("Error-Ya existe");
                comando.Connection.Close();

                groupBox1.Visible = true;
                tabControl1.SelectTab(1);
            }
        }

        private void Boton_clientes_cambiar_Click(object sender, EventArgs e)
        {
            string sql = "cambiar_cliente";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con)
            {
                BindByName = true,
                CommandType = CommandType.StoredProcedure
            };
            //se asignan los parametros de la funcion/procedimiento
            var prm1 = new OracleParameter("nref", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = dato_puntero };
            comando.Parameters.Add(prm1);
            var prm2 = new OracleParameter("inombre", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_nombre.Text };
            comando.Parameters.Add(prm2);
            var prm3 = new OracleParameter("iapep", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_ap.Text };
            comando.Parameters.Add(prm3);
            var prm4 = new OracleParameter("iapem", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_am.Text };
            comando.Parameters.Add(prm4);
            var prm5 = new OracleParameter("issn", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_is.Text };
            comando.Parameters.Add(prm5);
            //se crea el valor de retorno para la funcion
            var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
            comando.Parameters.Add(returnVal);

            comando.Connection.Open();
            comando.ExecuteNonQuery();

            if (returnVal.Value.ToString() == "1")
                MessageBox.Show("OK");
            else
                MessageBox.Show("Error");
            comando.Connection.Close();
        }

        private void Boton_clientes_eliminar_Click(object sender, EventArgs e)
        {
            string sql = "eliminar_cliente";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con)
            {
                BindByName = true,
                CommandType = CommandType.StoredProcedure
            };
            //se asignan los parametros de la funcion/procedimiento
            var prm1 = new OracleParameter("inombre", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_nombre.Text };
            comando.Parameters.Add(prm1);
            var prm2 = new OracleParameter("iapep", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_ap.Text };
            comando.Parameters.Add(prm2);
            var prm3 = new OracleParameter("iapem", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = texto_clientes_am.Text };
            comando.Parameters.Add(prm3);
            //se crea el valor de retorno para la funcion
            var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
            comando.Parameters.Add(returnVal);

            comando.Connection.Open();
            comando.ExecuteNonQuery();

            if (returnVal.Value.ToString() == "1")
                MessageBox.Show("OK");
            else
                MessageBox.Show("Error");
            comando.Connection.Close();
        }

        private void Boton_clientes_mostrar_Click(object sender, EventArgs e)
        {
            //muestra en la tabla las cuentas de personal registradas
            //aun usa sql directo, si se necesita cambiar a embedido
            string sql = "select id,nombre,apep,apem,ssn,contador from cliente";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con);

            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            tabla_cuentas.DataSource = dt;
            con.Close();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            control_activo = 3;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            control_activo = 1;
        }

        private void boton_mostrar_viajes_Click(object sender, EventArgs e)
        {
            //muestra en la tabla los viajes registrados
            //aun usa sql directo, si se necesita cambiar a embebido
            string sql = "select id,origen,destino,fecha,hora,capacidad,estado from viajes";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con);

            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            datos_viaje.DataSource = dt;
            con.Close();
        }

        private void cancelar_viaje_Click(object sender, EventArgs e)
        {
            string sql = "cancelar_viaje";
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con)
            {
                BindByName = true,
                CommandType = CommandType.StoredProcedure
            };
            //se asignan los parametros de la funcion/procedimiento
            var prm1 = new OracleParameter("nref", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = dato_puntero };
            comando.Parameters.Add(prm1);
           
            //se crea el valor de retorno para la funcion
            var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
            comando.Parameters.Add(returnVal);

            comando.Connection.Open();
            comando.ExecuteNonQuery();

            if (returnVal.Value.ToString() == "1")
                MessageBox.Show("OK");
            else
                MessageBox.Show("Error");
            comando.Connection.Close();
        }

        private void datos_viaje_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex != -1)
                {
                    DataGridViewCell cell = (DataGridViewCell)datos_viaje.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dato_puntero = datos_viaje.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (control_activo == 4)
                    {
                        texto_origen.Text = datos_viaje.Rows[e.RowIndex].Cells[1].Value.ToString();
                        texto_destino.Text = datos_viaje.Rows[e.RowIndex].Cells[2].Value.ToString();
                        fecha_picker.Text = datos_viaje.Rows[e.RowIndex].Cells[3].Value.ToString();
                        texto_hora.Text = datos_viaje.Rows[e.RowIndex].Cells[4].Value.ToString();
                        texto_capacidad.Text = datos_viaje.Rows[e.RowIndex].Cells[5].Value.ToString();
                    }
                   

                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            control_activo = 4;
        }

        private void Boton_buscar_viaje_Click(object sender, EventArgs e)
        {
            //muestra en la tabla los viajes DISPONIBLES    
            //aun usa sql directo, si se necesita cambiar a embedido
            string sql;
            if (texto_boleto_origen.Text == "" && texto_boleto_destino.Text == "")
            {
                 sql = "select id,origen,destino,fecha,hora,capacidad from viajes where estado='DISPONIBLE'";
            }
            else
            {
                sql = "select id,origen,destino,fecha,hora,capacidad from viajes where estado='DISPONIBLE' and origen='"+texto_boleto_origen.Text+"' and destino='"+texto_boleto_destino.Text+ "'and fecha > sysdate";
            }
                string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con);

            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            tabla_ver_vuelos.DataSource = dt;
            con.Close();
        }

        private void tabla_ver_vuelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex != -1)
                {
                    DataGridViewCell cell = (DataGridViewCell)tabla_ver_vuelos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dato_puntero = tabla_ver_vuelos.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (control_activo == 7)
                    {
                        texto_id_viaje.Text = tabla_ver_vuelos.Rows[e.RowIndex].Cells[0].Value.ToString();
                    }
                    if(control_activo == 8)
                    {
                        texto_id_cliente.Text = tabla_ver_vuelos.Rows[e.RowIndex].Cells[0].Value.ToString();
                    }
                }
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {
            control_activo = 7;
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {
            control_activo = 8;
        }

        private void boton_buscar_cliente_Click(object sender, EventArgs e)
        {
            //muestra en la tabla los clientes con el mismo nombr
            //aun usa sql directo, si se necesita cambiar a embedido
            string sql;
            if (texto_boleto_nombre.Text == "")
            {
                sql = "select id,nombre,apep,apem from cliente";
            }
            else
            {
                sql = "select id,nombre,apep,apem from cliente where nombre='" + texto_boleto_nombre.Text + "'";
            }
            string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
            OracleConnection con = new OracleConnection(conexion);
            OracleCommand comando = new OracleCommand(sql, con);

            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            tabla_ver_vuelos.DataSource = dt;
            con.Close();

            texto_id_cliente.Text = "";
        }

        private void boton_nuevo_cliente_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            groupBox1.Visible = false;
            boton_regresar_servidor.Visible = false;
            boton_regresar_cliente.Visible = true;
        }

        private void ir_cuentas_clientes_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            boton_regresar_cliente.Visible = false;
            boton_regresar_servidor.Visible = true;
        }

        private void boton_regresar_servidor_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void boton_logout_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void boton_salir_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void boton_regresar_cliente_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void cuentas_Click(object sender, EventArgs e)
        {

        }

        private void check_admin_CheckedChanged(object sender, EventArgs e)
        {

        }

            private void boton_comprar_boleto_Click(object sender, EventArgs e)
            {
                if (texto_id_cliente.Text == "" || texto_id_viaje.Text == "")
                    MessageBox.Show("Error-Escoger #Cliente y #Viaje primero");
                else
                {
                    DialogResult respuesta = MessageBox.Show("Confirmar compra?" + "Cliente N: " + texto_id_cliente.Text + "Viaje N: " + texto_id_viaje.Text + "", "Confirmar compra", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        string sql = "insertar_boleto";
                        string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
                        OracleConnection con = new OracleConnection(conexion);
                        OracleCommand comando = new OracleCommand(sql, con)
                        {
                            BindByName = true,
                            CommandType = CommandType.StoredProcedure
                        };
                        //se asignan los parametros de la funcion/procedimiento
                        var prm1 = new OracleParameter("icliente", OracleDbType.Int32, ParameterDirection.Input) { Value = texto_id_cliente.Text };
                        comando.Parameters.Add(prm1);
                        var prm2 = new OracleParameter("iviaje", OracleDbType.Int32, ParameterDirection.Input) { Value = texto_id_viaje.Text };
                        comando.Parameters.Add(prm2);

                        //se crea el valor de retorno para la funcion
                        var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1, ParameterDirection.ReturnValue);
                        comando.Parameters.Add(returnVal);

                        comando.Connection.Open();
                        comando.ExecuteNonQuery();

                        if (returnVal.Value.ToString() == "1")
                        {
                            MessageBox.Show("OK");
                            Form form2 = new Form();
                            Label letreo1 = new Label() { Text = "Hola", Location = new Point(50, 50) };
                            form2.Controls.Add(letreo1);

                            form2.ShowDialog();

                        }
                        else
                            MessageBox.Show("Error de red");
                        comando.Connection.Close();
                    }
                }
            }

            private void boton_ver_boletos_Click(object sender, EventArgs e)
            {
                //muestra en la tabla todos los boletos
                string sql;
                sql = "select id,idvuelo,idcliente from boleto";

                string conexion = "DATA SOURCE=localhost;PASSWORD=5695;PERSIST SECURITY INFO=True;USER ID=HR1";
                OracleConnection con = new OracleConnection(conexion);
                OracleCommand comando = new OracleCommand(sql, con);

                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                datos_viaje.DataSource = dt;
                con.Close();
            }
        
    }
}
