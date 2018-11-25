namespace maquina_virtual_abc
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cargar_archivo = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cargar_memoria = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.instruccion_op = new System.Windows.Forms.TextBox();
            this.instruccion_actual = new System.Windows.Forms.TextBox();
            this.bandera_negativo = new System.Windows.Forms.CheckBox();
            this.bandera_cero = new System.Windows.Forms.CheckBox();
            this.bandera_igual = new System.Windows.Forms.CheckBox();
            this.bandera_menor = new System.Windows.Forms.CheckBox();
            this.bandera_mayor = new System.Windows.Forms.CheckBox();
            this.run_all = new System.Windows.Forms.Button();
            this.run_1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.registro_x = new System.Windows.Forms.TextBox();
            this.registro_cp = new System.Windows.Forms.TextBox();
            this.registro_acomulador = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.instruccion_op_val = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cargar_archivo);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(84, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archivo";
            // 
            // cargar_archivo
            // 
            this.cargar_archivo.Location = new System.Drawing.Point(6, 396);
            this.cargar_archivo.Name = "cargar_archivo";
            this.cargar_archivo.Size = new System.Drawing.Size(75, 23);
            this.cargar_archivo.TabIndex = 1;
            this.cargar_archivo.Text = "ARCHIVO";
            this.cargar_archivo.UseVisualStyleBackColor = true;
            this.cargar_archivo.Click += new System.EventHandler(this.Cargar_archivo_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(7, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(71, 370);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.cargar_memoria);
            this.groupBox2.Location = new System.Drawing.Point(133, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 425);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "    0         1          2         3          4         5         6          7   " +
    "      8         9         10        11        12       13        14        15";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(7, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.Size = new System.Drawing.Size(554, 324);
            this.dataGridView1.TabIndex = 99;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Column12";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Column13";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Column14";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Column16";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // cargar_memoria
            // 
            this.cargar_memoria.Location = new System.Drawing.Point(235, 383);
            this.cargar_memoria.Name = "cargar_memoria";
            this.cargar_memoria.Size = new System.Drawing.Size(75, 23);
            this.cargar_memoria.TabIndex = 2;
            this.cargar_memoria.Text = "CARGA";
            this.cargar_memoria.UseVisualStyleBackColor = true;
            this.cargar_memoria.Click += new System.EventHandler(this.Cargar_memoria_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.instruccion_op_val);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.instruccion_op);
            this.groupBox3.Controls.Add(this.instruccion_actual);
            this.groupBox3.Controls.Add(this.bandera_negativo);
            this.groupBox3.Controls.Add(this.bandera_cero);
            this.groupBox3.Controls.Add(this.bandera_igual);
            this.groupBox3.Controls.Add(this.bandera_menor);
            this.groupBox3.Controls.Add(this.bandera_mayor);
            this.groupBox3.Controls.Add(this.run_all);
            this.groupBox3.Controls.Add(this.run_1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.registro_x);
            this.groupBox3.Controls.Add(this.registro_cp);
            this.groupBox3.Controls.Add(this.registro_acomulador);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(706, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 450);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CPU";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "OP DIR";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = " Instruccion Actual";
            // 
            // instruccion_op
            // 
            this.instruccion_op.Location = new System.Drawing.Point(9, 240);
            this.instruccion_op.Name = "instruccion_op";
            this.instruccion_op.ReadOnly = true;
            this.instruccion_op.Size = new System.Drawing.Size(75, 20);
            this.instruccion_op.TabIndex = 14;
            // 
            // instruccion_actual
            // 
            this.instruccion_actual.Location = new System.Drawing.Point(9, 180);
            this.instruccion_actual.Name = "instruccion_actual";
            this.instruccion_actual.ReadOnly = true;
            this.instruccion_actual.Size = new System.Drawing.Size(185, 20);
            this.instruccion_actual.TabIndex = 13;
            // 
            // bandera_negativo
            // 
            this.bandera_negativo.AutoSize = true;
            this.bandera_negativo.Enabled = false;
            this.bandera_negativo.Location = new System.Drawing.Point(47, 122);
            this.bandera_negativo.Name = "bandera_negativo";
            this.bandera_negativo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bandera_negativo.Size = new System.Drawing.Size(29, 17);
            this.bandera_negativo.TabIndex = 12;
            this.bandera_negativo.Text = "-";
            this.bandera_negativo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bandera_negativo.UseVisualStyleBackColor = true;
            // 
            // bandera_cero
            // 
            this.bandera_cero.AutoSize = true;
            this.bandera_cero.Enabled = false;
            this.bandera_cero.Location = new System.Drawing.Point(9, 122);
            this.bandera_cero.Name = "bandera_cero";
            this.bandera_cero.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bandera_cero.Size = new System.Drawing.Size(32, 17);
            this.bandera_cero.TabIndex = 11;
            this.bandera_cero.Text = "0";
            this.bandera_cero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bandera_cero.UseVisualStyleBackColor = true;
            // 
            // bandera_igual
            // 
            this.bandera_igual.AutoSize = true;
            this.bandera_igual.Enabled = false;
            this.bandera_igual.Location = new System.Drawing.Point(151, 122);
            this.bandera_igual.Name = "bandera_igual";
            this.bandera_igual.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bandera_igual.Size = new System.Drawing.Size(32, 17);
            this.bandera_igual.TabIndex = 10;
            this.bandera_igual.Text = "=";
            this.bandera_igual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bandera_igual.UseVisualStyleBackColor = true;
            // 
            // bandera_menor
            // 
            this.bandera_menor.AutoSize = true;
            this.bandera_menor.Enabled = false;
            this.bandera_menor.Location = new System.Drawing.Point(113, 122);
            this.bandera_menor.Name = "bandera_menor";
            this.bandera_menor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bandera_menor.Size = new System.Drawing.Size(32, 17);
            this.bandera_menor.TabIndex = 9;
            this.bandera_menor.Text = "<";
            this.bandera_menor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bandera_menor.UseVisualStyleBackColor = true;
            // 
            // bandera_mayor
            // 
            this.bandera_mayor.AutoSize = true;
            this.bandera_mayor.Enabled = false;
            this.bandera_mayor.Location = new System.Drawing.Point(82, 122);
            this.bandera_mayor.Name = "bandera_mayor";
            this.bandera_mayor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bandera_mayor.Size = new System.Drawing.Size(32, 17);
            this.bandera_mayor.TabIndex = 8;
            this.bandera_mayor.Text = ">";
            this.bandera_mayor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bandera_mayor.UseVisualStyleBackColor = true;
            // 
            // run_all
            // 
            this.run_all.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.run_all.ForeColor = System.Drawing.SystemColors.ControlText;
            this.run_all.Location = new System.Drawing.Point(119, 396);
            this.run_all.Name = "run_all";
            this.run_all.Size = new System.Drawing.Size(75, 23);
            this.run_all.TabIndex = 7;
            this.run_all.Text = "RUN ALL";
            this.run_all.UseVisualStyleBackColor = false;
            this.run_all.Click += new System.EventHandler(this.run_all_Click);
            // 
            // run_1
            // 
            this.run_1.Location = new System.Drawing.Point(9, 396);
            this.run_1.Name = "run_1";
            this.run_1.Size = new System.Drawing.Size(75, 23);
            this.run_1.TabIndex = 6;
            this.run_1.Text = "RUN 1";
            this.run_1.UseVisualStyleBackColor = true;
            this.run_1.Click += new System.EventHandler(this.Run1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "CP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "X";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "A";
            // 
            // registro_x
            // 
            this.registro_x.Location = new System.Drawing.Point(41, 60);
            this.registro_x.Name = "registro_x";
            this.registro_x.ReadOnly = true;
            this.registro_x.Size = new System.Drawing.Size(153, 20);
            this.registro_x.TabIndex = 2;
            // 
            // registro_cp
            // 
            this.registro_cp.Location = new System.Drawing.Point(41, 86);
            this.registro_cp.Name = "registro_cp";
            this.registro_cp.ReadOnly = true;
            this.registro_cp.Size = new System.Drawing.Size(153, 20);
            this.registro_cp.TabIndex = 1;
            // 
            // registro_acomulador
            // 
            this.registro_acomulador.Location = new System.Drawing.Point(41, 34);
            this.registro_acomulador.Name = "registro_acomulador";
            this.registro_acomulador.ReadOnly = true;
            this.registro_acomulador.Size = new System.Drawing.Size(153, 20);
            this.registro_acomulador.TabIndex = 0;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.richTextBox2.Location = new System.Drawing.Point(97, 34);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(37, 323);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "000\n016\n032\n048\n064\n080\n096\n112\n128\n144\n160\n176\n192\n208\n224\n240";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "OP VALOR";
            // 
            // instruccion_op_val
            // 
            this.instruccion_op_val.Location = new System.Drawing.Point(108, 240);
            this.instruccion_op_val.Name = "instruccion_op_val";
            this.instruccion_op_val.ReadOnly = true;
            this.instruccion_op_val.Size = new System.Drawing.Size(75, 20);
            this.instruccion_op_val.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 450);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Maquina Virtual MM";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cargar_archivo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cargar_memoria;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox registro_x;
        private System.Windows.Forms.TextBox registro_cp;
        private System.Windows.Forms.TextBox registro_acomulador;
        private System.Windows.Forms.Button run_all;
        private System.Windows.Forms.Button run_1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox bandera_mayor;
        private System.Windows.Forms.CheckBox bandera_negativo;
        private System.Windows.Forms.CheckBox bandera_cero;
        private System.Windows.Forms.CheckBox bandera_igual;
        private System.Windows.Forms.CheckBox bandera_menor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox instruccion_op;
        private System.Windows.Forms.TextBox instruccion_actual;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox instruccion_op_val;
    }
}

