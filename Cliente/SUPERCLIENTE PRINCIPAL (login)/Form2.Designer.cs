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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // conectar
            // 
            this.conectar.Location = new System.Drawing.Point(697, 99);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(140, 23);
            this.conectar.TabIndex = 0;
            this.conectar.Text = "INICIAR SESION";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // salir
            // 
            this.salir.Location = new System.Drawing.Point(733, 128);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(104, 23);
            this.salir.TabIndex = 1;
            this.salir.Text = "SALIR";
            this.salir.UseVisualStyleBackColor = true;
            this.salir.Click += new System.EventHandler(this.salir_Click);
            // 
            // Longitud
            // 
            this.Longitud.AutoSize = true;
            this.Longitud.Location = new System.Drawing.Point(21, 21);
            this.Longitud.Name = "Longitud";
            this.Longitud.Size = new System.Drawing.Size(327, 21);
            this.Longitud.TabIndex = 2;
            this.Longitud.TabStop = true;
            this.Longitud.Text = "partida mas corta del jugador con mas victorias";
            this.Longitud.UseVisualStyleBackColor = true;
            this.Longitud.CheckedChanged += new System.EventHandler(this.Longitud_CheckedChanged_1);
            // 
            // Bonito
            // 
            this.Bonito.AutoSize = true;
            this.Bonito.Location = new System.Drawing.Point(21, 63);
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
            this.altura.Name = "altura";
            this.altura.Size = new System.Drawing.Size(354, 21);
            this.altura.TabIndex = 4;
            this.altura.TabStop = true;
            this.altura.Text = "posicion final de la persona que ha recibido mas +4";
            this.altura.UseVisualStyleBackColor = true;
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(92, 156);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(224, 22);
            this.nombre.TabIndex = 5;
            this.nombre.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "NOMBRE";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(342, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "ENVIAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // datos
            // 
            this.datos.AutoSize = true;
            this.datos.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datos.Location = new System.Drawing.Point(693, 69);
            this.datos.Name = "datos";
            this.datos.Size = new System.Drawing.Size(51, 19);
            this.datos.TabIndex = 8;
            this.datos.Text = "label2";
            this.datos.Click += new System.EventHandler(this.datos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 91);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 277);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "consultas";
            // 
            // contlbl
            // 
            this.contlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contlbl.Location = new System.Drawing.Point(250, 111);
            this.contlbl.Name = "contlbl";
            this.contlbl.Size = new System.Drawing.Size(56, 30);
            this.contlbl.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "nº servicios";
            // 
            // CHAT
            // 
            this.CHAT.Location = new System.Drawing.Point(746, 157);
            this.CHAT.Name = "CHAT";
            this.CHAT.Size = new System.Drawing.Size(91, 23);
            this.CHAT.TabIndex = 16;
            this.CHAT.Text = "CHAT";
            this.CHAT.UseVisualStyleBackColor = true;
            this.CHAT.Click += new System.EventHandler(this.CHAT_Click_1);
            // 
            // ListaConectados
            // 
            this.ListaConectados.Location = new System.Drawing.Point(333, 27);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.Size = new System.Drawing.Size(385, 17);
            this.ListaConectados.TabIndex = 17;
            this.ListaConectados.Text = "label4";
            // 
            // partida
            // 
            this.partida.Location = new System.Drawing.Point(471, 241);
            this.partida.Name = "partida";
            this.partida.Size = new System.Drawing.Size(273, 147);
            this.partida.TabIndex = 18;
            this.partida.Text = "JUGAR PARTIDA";
            this.partida.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 486);
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
    }
}