namespace SUPERCLIENTE_PRINCIPAL
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
            this.aceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.TextBox();
            this.contraseña = new System.Windows.Forms.TextBox();
            this.crear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // aceptar
            // 
            this.aceptar.Location = new System.Drawing.Point(190, 140);
            this.aceptar.Name = "aceptar";
            this.aceptar.Size = new System.Drawing.Size(104, 33);
            this.aceptar.TabIndex = 0;
            this.aceptar.Text = "iniciar sesion";
            this.aceptar.UseVisualStyleBackColor = true;
            this.aceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "contraseña";
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(141, 45);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(162, 22);
            this.usuario.TabIndex = 3;
            this.usuario.TextChanged += new System.EventHandler(this.usuario_TextChanged);
            // 
            // contraseña
            // 
            this.contraseña.Location = new System.Drawing.Point(141, 96);
            this.contraseña.Name = "contraseña";
            this.contraseña.PasswordChar = '*';
            this.contraseña.Size = new System.Drawing.Size(162, 22);
            this.contraseña.TabIndex = 4;
            this.contraseña.TextChanged += new System.EventHandler(this.contraseña_TextChanged);
            // 
            // crear
            // 
            this.crear.Location = new System.Drawing.Point(33, 140);
            this.crear.Name = "crear";
            this.crear.Size = new System.Drawing.Size(102, 33);
            this.crear.TabIndex = 5;
            this.crear.Text = "registrarse";
            this.crear.UseVisualStyleBackColor = true;
            this.crear.Click += new System.EventHandler(this.crear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 185);
            this.Controls.Add(this.crear);
            this.Controls.Add(this.contraseña);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aceptar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox contraseña;
        private System.Windows.Forms.Button crear;
    }
}

