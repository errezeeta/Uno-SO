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

namespace SUPERCLIENTE_PRINCIPAL
{
    public partial class Form1 : Form
    {

        Socket server;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.138");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            try
            {
                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ipep);//Intentamos conectar el socket

                //MessageBox.Show("Conectado");
                string mensaje = "4/" + usuario.Text + "/" + contraseña.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);


                //Ahora recivimos la respuesta del servidor

                byte[] msgr = new byte[30];
                server.Receive(msgr);
                mensaje = Encoding.ASCII.GetString(msgr).Split('\0')[0];
                if (mensaje == "0")
                {
                  
                    Form2 f = new Form2();
                    f.datos.Text = usuario.Text;
                    this.Hide();
                    f.ShowDialog();
                    
                    //var Form2 = new Form2();
                    //Form2.Show();
                    //MessageBox.Show("Bienvenido guapeton");
                } else
                {
                    MessageBox.Show("Intentalo de nuevo");
                    usuario.Text = null;
                    contraseña.Text = null;
                    this.BackColor = Color.Gray;
                    //server.Shutdown(SocketShutdown.Both);
                    //server.Close();

                }

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        
        }

        private void contraseña_TextChanged(object sender, EventArgs e)
        {
            if (contraseña.Text == null)
            {
                aceptar.Enabled = false;
            }
        }

        private void usuario_TextChanged(object sender, EventArgs e)
        {
            if (usuario.Text == null)
            {
                aceptar.Enabled = false;
            }
        }

        private void crear_Click(object sender, EventArgs e)
        {
            var Form3= new Form3();
            Form3.Show();
            //this.Hide();
        }
    }
}
