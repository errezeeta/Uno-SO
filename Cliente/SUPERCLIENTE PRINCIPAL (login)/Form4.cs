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
using System.Threading;

namespace SUPERCLIENTE_PRINCIPAL
{
    public partial class Form4 : Form
    {
        Socket server;
        Thread atender;
        public Form4()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            //pictureBox1.Image = Image.FromFile("foto_uno.gif");
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50063);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Black;
                //MessageBox.Show("Conectado");

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }
          private void AtenderServidor()
        {
            try
            {
                while (true)
                {
                    //recibimos  mensaje del servidor
                    byte[] msgr = new byte[300];
                    server.Receive(msgr);
                    string[] trozos = Encoding.ASCII.GetString(msgr).Split('/');// partimos por la barra, [] tienes al menos 2 strings
                    int codigo = Convert.ToInt32(trozos[0]);// el primer string lo convierte a numero
                    string mensaje = trozos[1].Split('\0')[0];
                    switch (codigo)
                    {
                        case 6: //escribes algo en el texto

                            siguiente9.Text = null;
                            siguiente9.Text = siguiente8.Text;
                            siguiente8.Text = siguiente7.Text;
                            siguiente7.Text = siguiente6.Text;
                            siguiente6.Text = siguiente5.Text;
                            siguiente5.Text = siguiente4.Text;
                            siguiente4.Text = siguiente3.Text;
                            siguiente3.Text = siguiente2.Text;
                            siguiente2.Text = siguiente1.Text;
                            siguiente1.Text = siguiente.Text;
                            siguiente.Text = mensaje;
                            break;
                    }

                }
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("Se ha parado un momento");
                return;
            }
            }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void enviar_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" + texto.Text;
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);
            texto.Text = null;
        }
    }
}
