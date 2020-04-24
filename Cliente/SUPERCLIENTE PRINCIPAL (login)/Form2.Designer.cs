namespace SUPERCLIENTE_PRINCIPAL
{
    partial class Form2
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
            this.Longitud = new System.Windows.Forms.RadioButton();
            this.Bonito = new System.Windows.Forms.RadioButton();
            this.altura = new System.Windows.Forms.RadioButton();
            this.nombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.datos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contlbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CHAT = new System.Windows.Forms.Button();
            this.ListaConectados = new System.Windows.Forms.Label();
            this.partida = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DameCon = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // conectar
            // 
            this.conectar.Location = new System.Drawing.Point(523, 80);
            this.conectar.Margin = new System.Windows.Forms.Padding(2);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(105, 19);
            this.conectar.TabIndex = 0;
            this.conectar.Text = "INICIAR SESION";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // salir
            // 
            this.salir.Location = new System.Drawing.Point(550, 104);
            this.salir.Margin = new System.Windows.Forms.Padding(2);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(78, 19);
            this.salir.TabIndex = 1;
            this.salir.Text = "SALIR";
            this.salir.UseVisualStyleBackColor = true;
            this.salir.Click += new System.EventHandler(this.salir_Click);
            // 
            // Longitud
            // 
            this.Longitud.AutoSize = true;
            this.Longitud.Location = new System.Drawing.Point(16, 17);
            this.Longitud.Margin = new System.Windows.Forms.Padding(2);
            this.Longitud.Name = "Longitud";
            this.Longitud.Size = new System.Drawing.Size(246, 17);
            this.Longitud.TabIndex = 2;
            this.Longitud.TabStop = true;
            this.Longitud.Text = "partida mas corta del jugador con mas victorias";
            this.Longitud.UseVisualStyleBackColor = true;
            this.Longitud.CheckedChanged += new System.EventHandler(this.Longitud_CheckedChanged);
            // 
            // Bonito
            // 
            this.Bonito.AutoSize = true;
            this.Bonito.Location = new System.Drawing.Point(16, 51);
            this.Bonito.Margin = new System.Windows.Forms.Padding(2);
            this.Bonito.Name = "Bonito";
            this.Bonito.Size = new System.Drawing.Size(185, 17);
            this.Bonito.TabIndex = 3;
            this.Bonito.TabStop = true;
            this.Bonito.Text = "el ganador de la partida más corta";
            this.Bonito.UseVisualStyleBackColor = true;
            this.Bonito.CheckedChanged += new System.EventHandler(this.Bonito_CheckedChanged);
            // 
            // altura
            // 
            this.altura.AutoSize = true;
            this.altura.Location = new System.Drawing.Point(16, 90);
            this.altura.Margin = new System.Windows.Forms.Padding(2);
            this.altura.Name = "altura";
            this.altura.Size = new System.Drawing.Size(266, 17);
            this.altura.TabIndex = 4;
            this.altura.TabStop = true;
            this.altura.Text = "posicion final de la persona que ha recibido mas +4";
            this.altura.UseVisualStyleBackColor = true;
            this.altura.CheckedChanged += new System.EventHandler(this.altura_CheckedChanged);
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(69, 127);
            this.nombre.Margin = new System.Windows.Forms.Padding(2);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(169, 20);
            this.nombre.TabIndex = 5;
            this.nombre.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "NOMBRE";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 179);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "ENVIAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // datos
            // 
            this.datos.AutoSize = true;
            this.datos.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datos.Location = new System.Drawing.Point(520, 56);
            this.datos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.datos.Name = "datos";
            this.datos.Size = new System.Drawing.Size(46, 16);
            this.datos.TabIndex = 8;
            this.datos.Text = "label2";
            this.datos.Click += new System.EventHandler(this.datos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 73);
            this.label2.TabIndex = 9;
            this.label2.Text = "UNO";
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
            this.groupBox1.Location = new System.Drawing.Point(9, 119);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(286, 225);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "consultas";
            // 
            // contlbl
            // 
            this.contlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contlbl.Location = new System.Drawing.Point(188, 90);
            this.contlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.contlbl.Name = "contlbl";
            this.contlbl.Size = new System.Drawing.Size(42, 25);
            this.contlbl.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "nº servicios";
            // 
            // CHAT
            // 
            this.CHAT.Location = new System.Drawing.Point(560, 128);
            this.CHAT.Margin = new System.Windows.Forms.Padding(2);
            this.CHAT.Name = "CHAT";
            this.CHAT.Size = new System.Drawing.Size(68, 19);
            this.CHAT.TabIndex = 16;
            this.CHAT.Text = "CHAT";
            this.CHAT.UseVisualStyleBackColor = true;
            this.CHAT.Click += new System.EventHandler(this.CHAT_Click_1);
            // 
            // ListaConectados
            // 
            this.ListaConectados.Location = new System.Drawing.Point(290, 35);
            this.ListaConectados.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.Size = new System.Drawing.Size(289, 14);
            this.ListaConectados.TabIndex = 17;
            this.ListaConectados.Text = " ";
            // 
            // partida
            // 
            this.partida.Location = new System.Drawing.Point(353, 196);
            this.partida.Margin = new System.Windows.Forms.Padding(2);
            this.partida.Name = "partida";
            this.partida.Size = new System.Drawing.Size(205, 119);
            this.partida.TabIndex = 18;
            this.partida.Text = "JUGAR PARTIDA";
            this.partida.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Usuarios conectados:";
            // 
            // DameCon
            // 
            this.DameCon.Location = new System.Drawing.Point(485, 357);
            this.DameCon.Name = "DameCon";
            this.DameCon.Size = new System.Drawing.Size(126, 27);
            this.DameCon.TabIndex = 20;
            this.DameCon.Text = "Dame Conectados";
            this.DameCon.UseVisualStyleBackColor = true;
            this.DameCon.Click += new System.EventHandler(this.DameCon_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 395);
            this.Controls.Add(this.DameCon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.partida);
            this.Controls.Add(this.ListaConectados);
            this.Controls.Add(this.CHAT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.contlbl);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datos);
            this.Controls.Add(this.salir);
            this.Controls.Add(this.conectar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Button salir;
        private System.Windows.Forms.RadioButton Longitud;
        private System.Windows.Forms.RadioButton Bonito;
        private System.Windows.Forms.RadioButton altura;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label datos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label contlbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CHAT;
        private System.Windows.Forms.Label ListaConectados;
        private System.Windows.Forms.Button partida;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DameCon;
    }
}