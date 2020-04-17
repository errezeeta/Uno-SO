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
    public partial class Form2 : Form
    {
        Socket server;
        Thread atender;//objeto de la clase thread 
        delegate void DelegadoParaEscribir(string mensaje);//creo una clase especial que se llama Delegado
        delegate void DelegarParaHablar(string mensaje);

        Form2 _form2;
        Form4 _form4;
        private void MakeForms()
        {
            _form2 = new Form2();
            _form2.Load += HandleSomeEvent;

            _form4 = new Form4();
        }


        private void HandleSomeEvent(object sender, EventArgs args)
        {

            _form2.Close();
            _form4.Close();
        }
        public Form2()
        {
            InitializeComponent();
            
           
            CheckForIllegalCrossThreadCalls = false;
  
            //pictureBox1.Image = Image.FromFile("foto_uno.gif");
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.138");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Gold;
                //MessageBox.Show("Conectado");

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            //pongo en marcha el tread que atendera a los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

            nombre.Enabled = false;
            conectar.Enabled = false;
        }

        private void PonContador(string contador)
        {
            contlbl.Text = contador;
        }
        private void PonChat(string texto)
        {
            //textlbl.Text = texto;
        }
        private void AtenderServidor()
        {
            while (true)
            {
                //recibimos  mensaje del servidor
                byte[] msgr = new byte[100];
                server.Receive(msgr);
                string[] trozos = Encoding.ASCII.GetString(msgr).Split('/');// partimos por la barra, [] tienes al menos 2 strings
                int codigo = Convert.ToInt32(trozos[0]);// el primer string lo convierte a numero
                string mensaje= trozos[1].Split('\0')[0];
              
                switch (codigo)
                {
                    case 1://partida mas corta del jugador con mas partidas

                        MessageBox.Show("La partida mas corta del jugador con mas vistorias es " + mensaje + " minutos");
                        if (mensaje == "-1")
                        {
                            MessageBox.Show("te has equivocado zoquete");
                        }
                        break;
                    case 2://el gandador de la partida mas corta

                        MessageBox.Show("El ganador de la partida mas corta es " + mensaje);
                        if (mensaje == "-1")
                        {
                            MessageBox.Show("te has equivocado zoquete");
                        }
                        break;
                    case 3://+4

                        MessageBox.Show("La posicion final en la que el jugador a recividio mas +4 es " + mensaje + "º");
                        if (mensaje == "-1")
                        {
                            MessageBox.Show("te has equivocado zoquete");
                        }
                        break;
                    case 4: //recibimos notificacion

                        contlbl.Text = mensaje;
                        //DelegadoParaEscribir delegado =  new DelegadoParaEscribir (PonContador);
                        //contlbl.Invoke(delegado, new object[] {mensaje});
                        break;
                    case 7:// recibo lista conectados
                        ListaConectados.Text = mensaje;
                     
                        break;
                    case 8:
                        string elusuario = mensaje;
                   
                        break;
                }
            }
        }
        private void AtenderChat(string loquesea)
        {
         
           // siguiente3.Text = null;
            //siguiente3.Text = siguiente2.Text;
            //siguiente2.Text = siguiente1.Text;
            //siguiente1.Text = siguiente.Text;
            //siguiente.Text = siguiente4.Text;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void conectar_Click(object sender, EventArgs e)
        {
            var Form1 = new Form1();
            Form1.Show();
            this.Hide();
           
        }

        private void salir_Click(object sender, System.EventArgs e)
        {
            button1.Enabled = false;
            salir.Enabled = false;
            nombre.Enabled = false;
            label1.Enabled = false;
            Longitud.Enabled = false;
            Bonito.Enabled = false;
            altura.Enabled = false;
            conectar.Enabled = true;
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();// detenemos el thread
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            
          
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (Longitud.Checked)
            {
                //enviamos al servidor el nombre tecleado
                string mensaje = "1/-";
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
            }
            else if (Bonito.Checked)
            {
                //enviamos al servidor el nombre tecleado
                string mensaje = "2/-";
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
            }
            else if (altura.Checked)
            {
                string mensaje = "3/" + nombre.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
            }
         
            }

        private void datos_Click(object sender, EventArgs e)
        {
         
        }
        

        private void Longitud_CheckedChanged(object sender, EventArgs e)
        {
            nombre.Enabled = false;
        }

        private void altura_CheckedChanged(object sender, EventArgs e)
        {
            nombre.Enabled = true;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            nombre.Enabled = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void Bonito_CheckedChanged(object sender, EventArgs e)
        {
            nombre.Enabled = false;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void contlbl_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
           
        }

        private void enviarchat_Click(object sender, EventArgs e)
        {
         
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void siguiente_Click(object sender, EventArgs e)
        {

        }

        private void CHAT_Click(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void CHAT_Click_1(object sender, EventArgs e)
        {
            var Form4 = new Form4();
            Form4.Show();
            
        }

        private void Longitud_CheckedChanged_1(object sender, EventArgs e)
        {

        }

}
    
}
