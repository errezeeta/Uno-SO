namespace Form1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.conectar = new System.Windows.Forms.Button();
            this.salir = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.enviarinvitacion = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.DameCon = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Longitud = new System.Windows.Forms.RadioButton();
            this.Bonito = new System.Windows.Forms.RadioButton();
            this.altura = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.nombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datos = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.invitarbien = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.texto = new System.Windows.Forms.TextBox();
            this.listabox2 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.registro = new System.Windows.Forms.Button();
            this.iniciar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.contraseña = new System.Windows.Forms.TextBox();
            this.usuario = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.INVITACION = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // conectar
            // 
            this.conectar.Location = new System.Drawing.Point(527, 25);
            this.conectar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(140, 23);
            this.conectar.TabIndex = 1;
            this.conectar.Text = "INICIAR SESION";
            this.conectar.UseVisualStyleBackColor = true;
            // 
            // salir
            // 
            this.salir.Location = new System.Drawing.Point(673, 25);
            this.salir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(140, 23);
            this.salir.TabIndex = 2;
            this.salir.Text = "SALIR";
            this.salir.UseVisualStyleBackColor = true;
            this.salir.Click += new System.EventHandler(this.salir_Click_1);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.enviarinvitacion);
            this.groupBox4.Controls.Add(this.listBox1);
            this.groupBox4.Controls.Add(this.DameCon);
            this.groupBox4.Location = new System.Drawing.Point(619, 244);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 228);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CONECTADOS";
            // 
            // enviarinvitacion
            // 
            this.enviarinvitacion.Location = new System.Drawing.Point(15, 159);
            this.enviarinvitacion.Name = "enviarinvitacion";
            this.enviarinvitacion.Size = new System.Drawing.Size(168, 23);
            this.enviarinvitacion.TabIndex = 33;
            this.enviarinvitacion.Text = "INVITAR";
            this.enviarinvitacion.UseVisualStyleBackColor = true;
            this.enviarinvitacion.Click += new System.EventHandler(this.enviarinvitacion_Click_1);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(15, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(168, 132);
            this.listBox1.TabIndex = 21;
            // 
            // DameCon
            // 
            this.DameCon.Location = new System.Drawing.Point(15, 189);
            this.DameCon.Margin = new System.Windows.Forms.Padding(4);
            this.DameCon.Name = "DameCon";
            this.DameCon.Size = new System.Drawing.Size(168, 25);
            this.DameCon.TabIndex = 20;
            this.DameCon.Text = "AÑADIR A INVITAR";
            this.DameCon.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.Longitud);
            this.groupBox1.Controls.Add(this.Bonito);
            this.groupBox1.Controls.Add(this.altura);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.nombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(39, 243);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(381, 277);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "consultas";
            // 
            // Longitud
            // 
            this.Longitud.AutoSize = true;
            this.Longitud.Location = new System.Drawing.Point(21, 21);
            this.Longitud.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Longitud.Name = "Longitud";
            this.Longitud.Size = new System.Drawing.Size(327, 21);
            this.Longitud.TabIndex = 2;
            this.Longitud.TabStop = true;
            this.Longitud.Text = "partida mas corta del jugador con mas victorias";
            this.Longitud.UseVisualStyleBackColor = true;
            // 
            // Bonito
            // 
            this.Bonito.AutoSize = true;
            this.Bonito.Location = new System.Drawing.Point(21, 63);
            this.Bonito.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bonito.Name = "Bonito";
            this.Bonito.Size = new System.Drawing.Size(246, 21);
            this.Bonito.TabIndex = 3;
            this.Bonito.TabStop = true;
            this.Bonito.Text = "el ganador de la partida más corta";
            this.Bonito.UseVisualStyleBackColor = true;
            // 
            // altura
            // 
            this.altura.AutoSize = true;
            this.altura.Location = new System.Drawing.Point(21, 111);
            this.altura.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.altura.Name = "altura";
            this.altura.Size = new System.Drawing.Size(354, 21);
            this.altura.TabIndex = 4;
            this.altura.TabStop = true;
            this.altura.Text = "posicion final de la persona que ha recibido mas +4";
            this.altura.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 220);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(341, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "ENVIAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(92, 156);
            this.nombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(224, 22);
            this.nombre.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "NOMBRE";
            // 
            // datos
            // 
            this.datos.AutoSize = true;
            this.datos.Location = new System.Drawing.Point(302, 143);
            this.datos.Name = "datos";
            this.datos.Size = new System.Drawing.Size(46, 17);
            this.datos.TabIndex = 35;
            this.datos.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 36;
            this.label5.Text = "invitar:";
            // 
            // invitarbien
            // 
            this.invitarbien.Location = new System.Drawing.Point(101, 215);
            this.invitarbien.Name = "invitarbien";
            this.invitarbien.Size = new System.Drawing.Size(203, 22);
            this.invitarbien.TabIndex = 37;
            this.invitarbien.TextChanged += new System.EventHandler(this.invitar_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(310, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 38;
            this.button3.Text = "enviar";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.texto);
            this.groupBox3.Controls.Add(this.listabox2);
            this.groupBox3.Location = new System.Drawing.Point(825, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 626);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CHAT";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(188, 586);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 31;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // texto
            // 
            this.texto.Location = new System.Drawing.Point(19, 586);
            this.texto.Name = "texto";
            this.texto.Size = new System.Drawing.Size(163, 22);
            this.texto.TabIndex = 30;
            // 
            // listabox2
            // 
            this.listabox2.FormattingEnabled = true;
            this.listabox2.ItemHeight = 16;
            this.listabox2.Location = new System.Drawing.Point(16, 32);
            this.listabox2.Name = "listabox2";
            this.listabox2.Size = new System.Drawing.Size(247, 548);
            this.listabox2.TabIndex = 29;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.registro);
            this.groupBox2.Controls.Add(this.iniciar);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.contraseña);
            this.groupBox2.Controls.Add(this.usuario);
            this.groupBox2.Location = new System.Drawing.Point(504, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 179);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LOG IN";
            // 
            // registro
            // 
            this.registro.Location = new System.Drawing.Point(31, 132);
            this.registro.Name = "registro";
            this.registro.Size = new System.Drawing.Size(97, 32);
            this.registro.TabIndex = 32;
            this.registro.Text = "Registrarse";
            this.registro.UseVisualStyleBackColor = true;
            // 
            // iniciar
            // 
            this.iniciar.Location = new System.Drawing.Point(197, 132);
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(101, 32);
            this.iniciar.TabIndex = 31;
            this.iniciar.Text = "Iniciar sesion";
            this.iniciar.UseVisualStyleBackColor = true;
            this.iniciar.Click += new System.EventHandler(this.iniciar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "Contraseña";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "Usuario";
            // 
            // contraseña
            // 
            this.contraseña.Location = new System.Drawing.Point(104, 89);
            this.contraseña.Name = "contraseña";
            this.contraseña.Size = new System.Drawing.Size(194, 22);
            this.contraseña.TabIndex = 28;
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(104, 45);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(194, 22);
            this.usuario.TabIndex = 27;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.INVITACION);
            this.groupBox5.Controls.Add(this.listBox2);
            this.groupBox5.Location = new System.Drawing.Point(619, 478);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 194);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "INVITAR";
            // 
            // INVITACION
            // 
            this.INVITACION.Location = new System.Drawing.Point(15, 144);
            this.INVITACION.Name = "INVITACION";
            this.INVITACION.Size = new System.Drawing.Size(179, 23);
            this.INVITACION.TabIndex = 1;
            this.INVITACION.Text = "INVITAR A TODOS";
            this.INVITACION.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(15, 21);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(179, 116);
            this.listBox2.TabIndex = 0;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::Form1.Properties.Resources.uno_logo;
            this.pictureBox10.Location = new System.Drawing.Point(71, 44);
            this.pictureBox10.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(235, 126);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 42;
            this.pictureBox10.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 677);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.invitarbien);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.datos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.salir);
            this.Controls.Add(this.conectar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Button salir;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button enviarinvitacion;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button DameCon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Longitud;
        private System.Windows.Forms.RadioButton Bonito;
        private System.Windows.Forms.RadioButton altura;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label datos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox invitarbien;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox texto;
        private System.Windows.Forms.ListBox listabox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button registro;
        private System.Windows.Forms.Button iniciar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox contraseña;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button INVITACION;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.PictureBox pictureBox10;

    }
}

