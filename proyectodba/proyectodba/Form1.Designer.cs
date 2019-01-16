namespace proyectodba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.login = new System.Windows.Forms.TabPage();
            this.Boton_ok = new System.Windows.Forms.Button();
            this.texto_contra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.texto_usuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.servidor = new System.Windows.Forms.TabPage();
            this.admin = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.Boton_buscar_viaje = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.login.SuspendLayout();
            this.servidor.SuspendLayout();
            this.admin.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.login);
            this.tabControl1.Controls.Add(this.servidor);
            this.tabControl1.Controls.Add(this.admin);
            this.tabControl1.Location = new System.Drawing.Point(-5, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 412);
            this.tabControl1.TabIndex = 0;
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.Gray;
            this.login.Controls.Add(this.Boton_ok);
            this.login.Controls.Add(this.texto_contra);
            this.login.Controls.Add(this.label2);
            this.login.Controls.Add(this.texto_usuario);
            this.login.Controls.Add(this.label1);
            this.login.Location = new System.Drawing.Point(4, 22);
            this.login.Name = "login";
            this.login.Padding = new System.Windows.Forms.Padding(3);
            this.login.Size = new System.Drawing.Size(790, 386);
            this.login.TabIndex = 0;
            this.login.Text = "login";
            // 
            // Boton_ok
            // 
            this.Boton_ok.BackColor = System.Drawing.Color.White;
            this.Boton_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.Boton_ok.Location = new System.Drawing.Point(332, 241);
            this.Boton_ok.Name = "Boton_ok";
            this.Boton_ok.Size = new System.Drawing.Size(70, 31);
            this.Boton_ok.TabIndex = 4;
            this.Boton_ok.Text = "ok";
            this.Boton_ok.UseVisualStyleBackColor = false;
            this.Boton_ok.Click += new System.EventHandler(this.Boton_ok_Click);
            // 
            // texto_contra
            // 
            this.texto_contra.Location = new System.Drawing.Point(288, 145);
            this.texto_contra.Name = "texto_contra";
            this.texto_contra.Size = new System.Drawing.Size(253, 20);
            this.texto_contra.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(182, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña:";
            // 
            // texto_usuario
            // 
            this.texto_usuario.Location = new System.Drawing.Point(288, 97);
            this.texto_usuario.Name = "texto_usuario";
            this.texto_usuario.Size = new System.Drawing.Size(253, 20);
            this.texto_usuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(193, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // servidor
            // 
            this.servidor.Controls.Add(this.button1);
            this.servidor.Controls.Add(this.groupBox8);
            this.servidor.Controls.Add(this.dataGridView1);
            this.servidor.Controls.Add(this.groupBox7);
            this.servidor.Location = new System.Drawing.Point(4, 22);
            this.servidor.Name = "servidor";
            this.servidor.Padding = new System.Windows.Forms.Padding(3);
            this.servidor.Size = new System.Drawing.Size(790, 386);
            this.servidor.TabIndex = 1;
            this.servidor.Text = "servidor";
            this.servidor.UseVisualStyleBackColor = true;
            // 
            // admin
            // 
            this.admin.Controls.Add(this.groupBox6);
            this.admin.Controls.Add(this.groupBox5);
            this.admin.Controls.Add(this.groupBox4);
            this.admin.Controls.Add(this.groupBox3);
            this.admin.Controls.Add(this.groupBox2);
            this.admin.Controls.Add(this.groupBox1);
            this.admin.Location = new System.Drawing.Point(4, 22);
            this.admin.Name = "admin";
            this.admin.Size = new System.Drawing.Size(790, 386);
            this.admin.TabIndex = 2;
            this.admin.Text = "admin";
            this.admin.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(14, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "personal";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(275, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "aviones";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(14, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "clientes";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(275, 171);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "viajes";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(14, 283);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 100);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "vuelos";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(275, 283);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 100);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "boleto";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.Boton_buscar_viaje);
            this.groupBox7.Controls.Add(this.comboBox2);
            this.groupBox7.Controls.Add(this.comboBox1);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Location = new System.Drawing.Point(14, 16);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(359, 108);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "viaje";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "origen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "destino";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(102, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(173, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(102, 46);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(173, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // Boton_buscar_viaje
            // 
            this.Boton_buscar_viaje.Location = new System.Drawing.Point(125, 73);
            this.Boton_buscar_viaje.Name = "Boton_buscar_viaje";
            this.Boton_buscar_viaje.Size = new System.Drawing.Size(75, 23);
            this.Boton_buscar_viaje.TabIndex = 4;
            this.Boton_buscar_viaje.Text = "buscar";
            this.Boton_buscar_viaje.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(379, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(394, 357);
            this.dataGridView1.TabIndex = 3;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox3);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.textBox4);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.textBox2);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.textBox1);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Location = new System.Drawing.Point(14, 145);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(359, 161);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "datos del pasajero";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nombre";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(96, 67);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "APP";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(99, 126);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 20);
            this.textBox3.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Curp";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(96, 92);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(182, 20);
            this.textBox4.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "APM";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Comprar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "AEROLINEAS DEL POZOL";
            this.tabControl1.ResumeLayout(false);
            this.login.ResumeLayout(false);
            this.login.PerformLayout();
            this.servidor.ResumeLayout(false);
            this.admin.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage login;
        private System.Windows.Forms.TabPage servidor;
        private System.Windows.Forms.Button Boton_ok;
        private System.Windows.Forms.TextBox texto_contra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox texto_usuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage admin;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button Boton_buscar_viaje;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}

