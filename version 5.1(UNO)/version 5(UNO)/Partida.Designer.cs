namespace Form1
{
    partial class Partida
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
            this.numForm = new System.Windows.Forms.Label();
            this.CHAT = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ENVIAR = new System.Windows.Forms.Button();
            this.texto = new System.Windows.Forms.TextBox();
            this.CHAT.SuspendLayout();
            this.SuspendLayout();
            // 
            // numForm
            // 
            this.numForm.AutoSize = true;
            this.numForm.Location = new System.Drawing.Point(12, 33);
            this.numForm.Name = "numForm";
            this.numForm.Size = new System.Drawing.Size(46, 17);
            this.numForm.TabIndex = 5;
            this.numForm.Text = "label1";
            this.numForm.Click += new System.EventHandler(this.label1_Click);
            // 
            // CHAT
            // 
            this.CHAT.Controls.Add(this.listBox1);
            this.CHAT.Controls.Add(this.ENVIAR);
            this.CHAT.Controls.Add(this.texto);
            this.CHAT.Location = new System.Drawing.Point(1326, 12);
            this.CHAT.Name = "CHAT";
            this.CHAT.Size = new System.Drawing.Size(200, 380);
            this.CHAT.TabIndex = 6;
            this.CHAT.TabStop = false;
            this.CHAT.Text = "CHAT";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(8, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(187, 324);
            this.listBox1.TabIndex = 0;
            // 
            // ENVIAR
            // 
            this.ENVIAR.Location = new System.Drawing.Point(126, 350);
            this.ENVIAR.Name = "ENVIAR";
            this.ENVIAR.Size = new System.Drawing.Size(68, 23);
            this.ENVIAR.TabIndex = 1;
            this.ENVIAR.Text = "button1";
            this.ENVIAR.UseVisualStyleBackColor = true;
            this.ENVIAR.Click += new System.EventHandler(this.ENVIAR_Click);
            // 
            // texto
            // 
            this.texto.Location = new System.Drawing.Point(7, 351);
            this.texto.Name = "texto";
            this.texto.Size = new System.Drawing.Size(113, 22);
            this.texto.TabIndex = 2;
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1570, 556);
            this.Controls.Add(this.CHAT);
            this.Controls.Add(this.numForm);
            this.Name = "Partida";
            this.Text = "Partida";
            this.Load += new System.EventHandler(this.Partida_Load);
            this.CHAT.ResumeLayout(false);
            this.CHAT.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label numForm;
        private System.Windows.Forms.GroupBox CHAT;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button ENVIAR;
        private System.Windows.Forms.TextBox texto;
    }
}