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
namespace Form1
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;//objeto de la clase thread 
        delegate void DelegadoParaEscribir(string mensaje);//creo una clase especial que se llama Delegado
        delegate void DelegarParaHablar(string mensaje);

        //guardo los forms que voy creando
        List<Partida> formularios = new List<Partida>();

        public string mipropionombre;
        public string invitador;
        public int respuestainvitacion;
        public int idPartida;
        //Form2 _form2;
        //Form4 _form4;
        public int mipropiosocket;
        int cont;
        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            //pictureBox1.Image = Image.FromFile("foto_uno.gif");
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.135");
            IPEndPoint ipep = new IPEndPoint(direc, 9070);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.White;
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
            invitarbien.Enabled = false;
            // textbox1.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = false;
            salir.Enabled = false;
            nombre.Enabled = false;
            label1.Enabled = false;
            Bonito.Enabled = false;
            altura.Enabled = false;
            conectar.Enabled = true;
            DameCon.Enabled = false;
            button4.Enabled = false;
            texto.Enabled = false;
            Longitud.Enabled = false;


            listBox1.Items.Add("soy guapo");
            listBox1.Items.Add("soy rico");
        }
        private void PonContador(string contador)
        {
            //contlbl.Text = contador;
        }

        delegate void d0();
        delegate void d1(string mensaje);
        delegate void d2(string mensaje, string segundo_mensaje);
        private void AtenderServidor()
        {
            while (true)
            {
                //recibimos  mensaje del servidor
                byte[] msgr = new byte[2000];
                server.Receive(msgr);
                string[] trozos = Encoding.ASCII.GetString(msgr).Split('/');        // partimos por la barra, [] tienes al menos 2 strings
                int codigos= Convert.ToInt32(trozos[0]);                            // el primer string lo convierte a numero
                string mensaje;// = trozos[1].Split('\0')[0];
                int nform;
                switch (codigos)
                {
                    case 1://partida mas corta del jugador con mas partidas
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("La partida mas corta del jugador con mas vistorias es " + mensaje + " minutos");
                        if (mensaje == "-1")
                        {
                            MessageBox.Show("te has equivocado zoquete");
                        }
                        break;
                    case 2://el gandador de la partida mas corta
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("El ganador de la partida mas corta es " + mensaje);
                        if (mensaje == "-1")
                        {
                            MessageBox.Show("te has equivocado zoquete");
                        }
                        break;
                    case 3://+4
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("La posicion final en la que el jugador a recividio mas +4 es " + mensaje + "º");
                        if (mensaje == "-1")
                        {
                            MessageBox.Show("te has equivocado zoquete");
                        }
                        break;
                    case 4: //recibimos notificacion
                        mensaje = trozos[1].Split('\0')[0];
                        d1 t = new d1(ParticionLogin);
                        Invoke(t, new object[] { mensaje });
                        break;

                    case 6:
                        mensaje = trozos[1].Split('\0')[0];
                        Funcion6(mensaje);
                        break;
                    case 7:// recibo lista conectados

                        break;
                    case 8:
                        mensaje = trozos[1].Split('\0')[0];
                        listBox1.Items.Clear();
                        string elusuario = mensaje;
                        partir_lista_conectados(mensaje);
                        break;
                    case 9:

                        mensaje = trozos[1].Split('\0')[0];
                        //invitaciones(mensaje);
                        d2 d = new d2(MeInvitan);
                        Invoke(d, new object[] { mensaje, mipropionombre });
                        break;
                    case 10:
                         mensaje = trozos[1].Split('\0')[0];
                        d1 h = new d1(SIoNO);
                        Invoke(h, new object[] { mensaje });
                        break;
                     
                    case 11:
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].Tomachat(mensaje);
                        break;

                }
            }
        }
        private void ParticionLogin(string mensaje)
        {
            string[] Socketinfo = (mensaje).Split(',');
            mipropiosocket = Convert.ToInt32(Socketinfo[0]);
            string SioNo = Socketinfo[1].Split('\0')[0];
          
            if (SioNo == "si")
            {

              

              
                MessageBox.Show("Bienvenido guapeton");
                nombre.Enabled = true;
                conectar.Enabled = true;
                invitarbien.Enabled = true;
                // textbox1.Enabled = false;
                button3.Enabled = true;
                button1.Enabled = true;
                salir.Enabled = true;
                nombre.Enabled = true;
                label1.Enabled = true;
                Bonito.Enabled = true;
                altura.Enabled = true;
                conectar.Enabled = true;
                DameCon.Enabled = true;
                button4.Enabled = true;
                texto.Enabled = true;
                usuario.Enabled = false;
                contraseña.Enabled = false;
                iniciar.Enabled = false;
                registro.Enabled = false;
                Longitud.Enabled = true;

            }
            else
            {
                MessageBox.Show("Intentalo de nuevo");
             
            }
            //contlbl.Text = mensaje;
        }
        private void PonerenMarchaForm()
        {
            cont = formularios.Count;
            Partida t = new Partida(cont, server, mipropionombre, idPartida);
            formularios.Add(t);
            t.ShowDialog();
        }
        private void SIoNO(string mensaje)
        {
            //string[] NombreSIoNO = (mensaje).Split('/');
            //string respuesta = NombreSIoNO[1].Split('\0')[0];
            string[] informacion = (mensaje).Split(',');
            string aceptacion = informacion[0];
         
            if (aceptacion == "SI")
            {
                idPartida = Convert.ToInt32(informacion[1]);
                
                ThreadStart ts = delegate { PonerenMarchaForm(); };
                Thread T = new Thread(ts);
                T.Start();
            }
            else if (aceptacion == "NO")
            {
                MessageBox.Show("Lo siento pero te ha rechazado amigo");
            }

        }
        private void MeInvitan(string mensaje1, string primernombre)
        {
            cont = formularios.Count;
            if (MessageBox.Show("Te ha invigado " + mensaje1 + ".¿Aceptas la invitacion?", "invitacion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
                 ) == DialogResult.Yes)
            {

                string mensaje = "10/" + mipropionombre + "/" + mensaje1 + "/1/" + mipropiosocket+ "/" +cont;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
            }
            else
            {
                string mensaje = "10/" + mipropionombre + "/" + mensaje1 + "/0/" + mipropiosocket;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);

                //var Invitacion = new Invitacion();
                //Invitacion.invitacion1 = mensaje;
                //  Invitacion.mi_nombre = primernombre;
                //  Invitacion.Show();

                //MessageBox.Show("mery");
            }
        }
        public void invitaciones(string invitador)
        {
           // var Invitacion = new Invitacion();
       //     Invitacion.invitacion1 = invitador;
         //   Invitacion.Show();

        }
        public void partir_lista_conectados(string message)
        {
            string[] Nombre_Usuarios = (message).Split(',');
            int Numero_Conectados = Convert.ToInt32(Nombre_Usuarios[0]);
            int i = 0;
            while (Numero_Conectados > i)
            {
                string mens = Nombre_Usuarios[i + 1].Split(',')[0];
                listBox1.Items.Add(mens);
                i++;
            }

        }
        private void conectar_Click(object sender, EventArgs e)
        {
            var Form1 = new Form1();
            Form1.Show();
            this.Hide();

        }

        private void altura_CheckedChanged(object sender, EventArgs e)
        {
            nombre.Enabled = true;
        }

        private void Bonito_CheckedChanged(object sender, EventArgs e)
        {
            nombre.Enabled = false;
        }

        private void Longitud_CheckedChanged(object sender, EventArgs e)
        {
            nombre.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" + mipropionombre + "/: " + texto.Text;
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);
            texto.Text = null;
        }
        private void Funcion6(string mensaje)
        {
            listabox2.Items.Add(mensaje);
        }


        private void enviarinvitacion_Click_1(object sender, EventArgs e)
        {
            cont = formularios.Count;
            if (listBox1.SelectedItem != null)
            {
                string invitar = listBox1.SelectedItem.ToString();
                string mensaje = "9/" + mipropionombre + "/1," + invitar + "/" + mipropiosocket +"/"+ cont;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
            }
            else
            {
                MessageBox.Show("no has seleccionado ningún nombre");
            }

        }

        private void salir_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            salir.Enabled = false;
            nombre.Enabled = false;
            label1.Enabled = false;
            Longitud.Enabled = false;
            Bonito.Enabled = false;
            altura.Enabled = false;
            conectar.Enabled = true;
            usuario.Enabled = true;
            contraseña.Enabled = true;
            iniciar.Enabled = true;
            registro.Enabled = true;

            //Mensaje de desconexión
            string mensaje = "0/" + mipropionombre;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();// detenemos el thread
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void iniciar_Click(object sender, EventArgs e)
        {
            string mensaje = "4/" + usuario.Text + "/" + contraseña.Text;
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);
            mipropionombre = usuario.Text;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            cont = formularios.Count;
            //string invitar = listBox1.SelectedItem.ToString();
            string mensaje = "9/" + mipropionombre + "/" + invitarbien.Text + "/" + mipropiosocket + "/" + cont; 
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);

        }

        private void invitar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
