﻿using System;
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
            string sql = "select pass from cuentas where usuario = '"+ user +"'";
            OracleCommand comando = new OracleCommand(sql, con);
            OracleDataReader dr = comando.ExecuteReader();

            //si esta vacio
            if (user == "" || pass == "")
                MessageBox.Show("Insertar datos");
            else
            while (dr.Read())
            {
                //si se permite la entrada, cambiar a la interfaz          
                if (dr[0].ToString() == pass)
                    tabControl1.SelectTab(1);
                else
                    MessageBox.Show("Error en datos");
            }

            con.Dispose();          
                  
        }

        private void Crear_viaje_Click(object sender, EventArgs e)
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
            var prm3 = new OracleParameter("ifecha", OracleDbType.Varchar2, 50, ParameterDirection.Input) { Value = fecha_picker.Value.ToString("dd,MM,yyyy")};
            comando.Parameters.Add(prm3);
            var prm4 = new OracleParameter("ihora", OracleDbType.Int32, 10, ParameterDirection.Input) { Value = texto_hora.Value.ToString()};
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

        private void Boton_agregar_personal_Click(object sender, EventArgs e)
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
            //se crea el valor de retorno para la funcion
            var returnVal = new OracleParameter("resp", OracleDbType.Int32, 1,ParameterDirection.ReturnValue);           
            comando.Parameters.Add(returnVal);

            comando.Connection.Open();
            comando.ExecuteNonQuery();

            if (returnVal.Value.ToString() == "0")
                MessageBox.Show("OK");
            else
                MessageBox.Show("Error-Ya existe");
            comando.Connection.Close();
        }

        private void Boton_mostrar_cuentas_Click(object sender, EventArgs e)
        {
            //muestra en la tabla las cuentas de personal registradas
            //aun usa sql directo, si se necesita cambiar a embedido
            string sql = "select id,usuario,pass from cuentas";
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
    }
}
