using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace admin_casa
{ 
    public partial class Form1 : Form
    {
        int logeo = 0; //0 = no logeado, 1 = asistente, 2 = contador, 3 = recepcionista, 4 = director, 6 = admin
        int id_logeado = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Boton_contadores_Click(object sender, EventArgs e)
        {
            if (logeo == 2)
            {
                tabControl1.Visible = true;
                tabControl1.SelectedTab = tabPage1;
            }
            else
                MessageBox.Show("Porfavor, ingrese al sistema","Error en login", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_direccion_Click(object sender, EventArgs e)
        {
            if (logeo == 4)
            {
                tabControl1.Visible = true;
                tabControl1.SelectedTab = tabPage2;
            }
            else
                MessageBox.Show("Porfavor, ingrese al sistema", "Error en login", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Boton_asistentes_Click(object sender, EventArgs e)
        {
            if (logeo == 1)
            {
                tabControl1.Visible = true;
                tabControl1.SelectedTab = tabPage3;
            }
            else
                MessageBox.Show("Porfavor, ingrese al sistema", "Error en login", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_recepcion_Click(object sender, EventArgs e)
        {
            if (logeo == 3)
            {
                tabControl1.Visible = true;
                tabControl1.SelectedTab = tabPage4;
            }
            else
                MessageBox.Show("Porfavor, ingrese al sistema", "Error en login", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Boton_admin_Click(object sender, EventArgs e)
        {
            if (logeo == 6)
            {
                tabControl1.Visible = true;
                tabControl1.SelectedTab = tabPage5;
            }
            else
                MessageBox.Show("Porfavor, ingrese al sistema", "Error en login", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void Boton_salir_contadores_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Boton_salir_direccion_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Boton_asistente_salir_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Boton_recepcion_salir_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Boton_salir_admin_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Boton_direcion_empleados_Click(object sender, EventArgs e)
        {           
            try
            {
                conexion.Open();
                string strSelectCmd = "select* from Persona where perfil not like '%5'";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_direccion.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_direcion_proyectos_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string strSelectCmd = "select* from Proyectos";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_direccion.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_asistentes_ver_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string strSelectCmd = "select* from Asistencia";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_asistentes.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_recepciones_ver_Click(object sender, EventArgs e)
        {
            //asistencia: 0 = no a llegado hoy, 1 = en lugar, 2 = retirado por hoy
            try
            {
                conexion.Open();
                string strSelectCmd = "select IdPersona,Nombre,apellido_paterno,apellido_materno,asistencia from Persona where perfil not like '%5'";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_recepciones.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_sistema_cambiar_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text != "")
            {
                try
                {
                    sql.CommandText = "UPDATE Persona SET Nombre='" + textBox_nombre.Text + "',apellido_paterno='" + textBox_ap.Text + "',apellido_materno='" + textBox_am.Text + "',fecha_nacimiento='" + textBox_fecha_n.Text + "',Direccion='" + textBox_direccion.Text + "',Estado='" + textBox_estado.Text + "',Pais='" + textBox_pais + "',contra='" + textBox_contra.Text + "',perfil='" + textBox_perfil.Text + "'WHERE IdPersona = " + UInt32.Parse(textBox_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Boton_proyectos_Click(object sender, EventArgs e)
        {

            try
            {
                conexion.Open();
                string strSelectCmd = "select* from Proyectos";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_contadores.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_asistencia_Click(object sender, EventArgs e)
        {
            if (textBox_contadores_id.Text != "")
            {
                try
                {
                    sql.CommandText = "insert into asistencia(Contador,Proyecto,Descripcion,Estatus)VALUES('" + id_logeado + "','" + textBox_contadores_id.Text + "','" + textBox_contadores_informe.Text +"','" + "Pendiente')";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    
        }

        private void Boton_contra_salir_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void Boton_login_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.SelectedTab = tabPage6;
            id_textBox.Text = "";
            pass_textBox.Text = "";
        }

        private void Boton_contra_entrar_Click(object sender, EventArgs e)
        {
          //int logeo = 0; //0 = no logeado, 1 = asistente, 2 = contador, 3 = recepcionista, 4 = director, 6 = admin, 5 = externo
            //cargo las cuentas
            try
            {
                conexion.Open();
                string strSelectCmd = "select IdPersona,contra,perfil from Persona";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_contra.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            int aux = Int32.Parse(id_textBox.Text);
            aux--;
            if(tabla_contra.Rows[aux].Cells[0].Value.ToString() == id_textBox.Text && tabla_contra.Rows[aux].Cells[1].Value.ToString() == pass_textBox.Text)
            {
                logeo = Int32.Parse(tabla_contra.Rows[aux].Cells[2].Value.ToString());
                id_logeado = Int32.Parse(tabla_contra.Rows[aux].Cells[0].Value.ToString());
                MessageBox.Show("Welcolme", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error", "Error en login", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_logout_Click(object sender, EventArgs e)
        {
            logeo = 0;
            id_logeado = 0;
            MessageBox.Show("Bye");
        }

        private void Boton_recepciones_salida_Click(object sender, EventArgs e)
        {
            //0 = no ha llegado, 1 = esta en el lugar, 2 = termino hoy
            if (textBox_recepciones_id.Text != "")
            {
                try
                {
                    sql.CommandText = "UPDATE Persona SET asistencia= 2 WHERE IdPersona = " + UInt32.Parse(textBox_recepciones_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_contadores_reportes_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string strSelectCmd = "select * from reporte";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_contadores.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_asistentes_aceptar_Click(object sender, EventArgs e)
        {
            if (textBox_asistentes_id.Text != "")
            {
                try
                {
                    sql.CommandText = "UPDATE asistencia SET Estatus='" + "Proceso" + "',Asistente_id='" + id_logeado + "'WHERE IdAsistencia = " + UInt32.Parse(textBox_asistentes_id.Text ) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_asistentes_entregar_Click(object sender, EventArgs e)
        {

            if (textBox_asistentes_id.Text != "")
            {
                try
                {
                    sql.CommandText = "UPDATE asistencia SET Estatus='" + "Terminado" + "',Notas='" + textBox_asistentes_notas.Text + "'WHERE IdAsistencia = " + UInt32.Parse(textBox_asistentes_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_recepciones_crear_Click(object sender, EventArgs e)
        {
            if (textBox_recepcion_visitante.Text != "")
            {
                try
                {
                    sql.CommandText = "insert into citas(fecha,hora,IdPersona,visitante)VALUES('" + textBox_recepcion_fecha.Text  + "','" + textBox_recepcion_hora.Text + "','" + textBox_recepcion_idp.Text + "','" + textBox_recepcion_visitante.Text + "')";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte almenos su nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_recepciones_borrar_Click(object sender, EventArgs e)
        {
            if (textBox_recepciones_id.Text != "")
            {
                try
                {
                    sql.CommandText = "DELETE FROM citas WHERE Id = " + UInt32.Parse(textBox_recepciones_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a borrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_recepciones_entrada_Click(object sender, EventArgs e)
        {
            //0 = no ha llegado, 1 = esta en el lugar, 2 = termino hoy
            if (textBox_recepciones_id.Text != "")
            {
                try
                {
                    sql.CommandText = "UPDATE Persona SET asistencia= 1 WHERE IdPersona = " + UInt32.Parse(textBox_recepciones_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_sistema_crear_Click(object sender, EventArgs e)
        {
            if (textBox_nombre.Text != "")
            {
                try
                {
                    sql.CommandText = "insert into Persona(Nombre,apellido_paterno,apellido_materno,fecha_nacimiento,Direccion,Estado,Pais,contra,perfil)VALUES('" + textBox_nombre.Text + "','" + textBox_ap.Text + "','" + textBox_am.Text + "','" + textBox_fecha_n.Text + "','" + textBox_direccion.Text + "','" + textBox_estado.Text + "','" + textBox_pais.Text + "','" + textBox_contra.Text + "','" + textBox_perfil.Text + "')";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte almenos su nombre","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    

    private void Boton_sistema_borrar_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text != "")
            {
                try
                {
                    sql.CommandText = "DELETE FROM Persona WHERE IdPersona = " + UInt32.Parse(textBox_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a borrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Boton_recepciones_citas_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string strSelectCmd = "select * from citas";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_recepciones.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void Boton_sistema_ver_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string strSelectCmd = "select * from Persona";
                MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(strSelectCmd, conexion);
                DataSet ds = new DataSet();
                objDataAdapter.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                tabla_sistema.DataSource = dt;
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Boton_direccion_agregar_Click(object sender, EventArgs e)
        {
            if (textBox_direccion_nombre.Text != "")
            {
                try
                {
                    sql.CommandText = "insert into Persona(Nombre,apellido_paterno,apellido_materno,fecha_nacimiento,Direccion,Estado,Pais,contra,perfil)VALUES('" + textBox_direccion_nombre.Text + "','" + textBox_direccion_ap.Text + "','" + textBox_direccion_am.Text + "','" + textBox_direccion_fecha_n.Text + "','" + textBox_direccion_direccion.Text + "','" + textBox_direccion_estado.Text + "','" + textBox_direccion_pais.Text + "','" + textBox_direccion_contra.Text + "','" + textBox_direccion_perfil.Text + "')";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte almenos su nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_direccion_cambiar_empleado_Click(object sender, EventArgs e)
        {
            if (textBox_direccion_id.Text != "")
            {
                try
                {
                    sql.CommandText = "UPDATE Persona SET Nombre='" + textBox_direccion_nombre.Text + "',apellido_paterno='" + textBox_direccion_ap.Text + "',apellido_materno='" + textBox_direccion_am.Text + "',fecha_nacimiento='" + textBox_direccion_fecha_n.Text + "',Direccion='" + textBox_direccion_direccion.Text + "',Estado='" + textBox_direccion_estado.Text + "',Pais='" + textBox_direccion_pais + "',contra='" + textBox_direccion_contra.Text + "',perfil='" + textBox_direccion_perfil.Text + "'WHERE IdPersona = " + UInt32.Parse(textBox_direccion_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_direccion_agregar_p_Click(object sender, EventArgs e)
        {
            if (textBox_direccion_nombre_p.Text != "")
            {
                try
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                    sql.CommandText = "insert into proyectos(Nombre,Descripcion,Valor,Fecha_inicio,Fecha_entrega,Estatus,Estado,Pais,Registro,Encargado)VALUES('" + textBox_direccion_nombre_p.Text + "','" + textBox_direccion_descripcion_p.Text + "','" + textBox_direccion_valor_p.Text + "','" + textBox_direccion_fi_p.Text + "','" + textBox_direccion_ff_p.Text + "','" + textBox_direccion_estatus_p.Text + "','" + textBox_direccion_estado_p.Text + "','" + textBox_direccion_pais_p.Text + "','" + textBox_direccion_registro_p.Text + "','" + textBox_direccion_encargado_p.Text + "')";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte almenos su nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Boton_direccion_cambiar_p_Click(object sender, EventArgs e)
        {
            if (textBox_direccion_id.Text != "")
            {
                try
                {                                                                                                                                                                                                                                                                                                                                                                                                                                     
                    sql.CommandText = "UPDATE proyectos SET Nombre='" + textBox_direccion_nombre_p.Text + "',Descripcion='" + textBox_direccion_descripcion_p.Text + "',Valor='" + textBox_direccion_valor_p.Text + "',Fecha_inicio='" + textBox_direccion_fi_p.Text + "',Fecha_entrega='" + textBox_direccion_ff_p.Text + "',Estatus='" + textBox_direccion_estatus_p.Text + "',Estado='" + textBox_direccion_estado_p.Text + "',Pais='" + textBox_direccion_pais_p.Text + "',Registro='" + textBox_direccion_registro_p.Text + "',Encargado='" + textBox_direccion_encargado_p.Text + "'WHERE IdProyecto = " + UInt32.Parse(textBox_direccion_id.Text) + "";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte Id a cambiar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Boton_contadores_reportar_Click(object sender, EventArgs e)
        {
            if (textBox_contadores_id.Text != "")
            {
                try
                {
                    sql.CommandText = "insert into reporte(idProyecto,Idpersona,Descripcion)VALUES('" + textBox_contadores_id.Text + "','" + id_logeado + "','" + textBox_contadores_informe.Text + "')";
                    conexion.Open();
                    sql.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("OK", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void panel_contadores_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_contadores_informe_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
