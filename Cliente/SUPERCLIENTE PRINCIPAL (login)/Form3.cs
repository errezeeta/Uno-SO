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
    public partial class Form3 : Form
    {

        Socket server;
        public Form3()
        {
            InitializeComponent();
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.138");
            IPEndPoint ipep = new IPEndPoint(direc, 9070);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipep);//Intentamos conectar el socket
            
        }
    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

           

        }

        private void crear_Click(object sender, EventArgs e)
        {
            try
            {
                
                //MessageBox.Show("Conectado");
                string mensaje = "5/" + usuario.Text + "/" + contraseña.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);


                //Ahora recivimos la respuesta del servidor

                
                this.Hide();
            
           

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            MessageBox.Show("YA ESA");
        
        }
    }
}
