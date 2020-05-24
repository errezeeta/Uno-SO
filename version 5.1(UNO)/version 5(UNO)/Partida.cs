using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
namespace Form1
{
    public partial class Partida : Form
    {
        int nForm;
        Socket server;
        string mipropionombre;
        int idPartida;
        public Partida(int nForm, Socket server, string mipropionombre, int idPartida)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.mipropionombre = mipropionombre;
            this.idPartida = idPartida;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Partida_Load(object sender, EventArgs e)
        {
            numForm.Text = nForm.ToString();
        }
         public void Tomachat(string mensaje)
        {
            listBox1.Items.Add(mensaje);
        }

        private void ENVIAR_Click(object sender, EventArgs e)
        {
            string mensaje = "11/" + nForm + "/" + idPartida + "/" + mipropionombre + "/: " + texto.Text;
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);
            texto.Text = null;

        }

    }
}
