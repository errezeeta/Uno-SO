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
using System.IO;
namespace Form1
{
    public partial class Form1 : Form
    {

        int cuenta = 0;
        Socket server;
        Thread atender;//objeto de la clase thread 
        delegate void DelegadoParaEscribir(string mensaje);//creo una clase especial que se llama Delegado
        delegate void DelegarParaHablar(string mensaje);

        //guardo los forms que voy creando
        List<Partida> formularios = new List<Partida>();

        public string mipropionombre;
        public string invitador;
        public int respuestainvitacion;
        public int posicionJug;
        public int idPartida;
        public int numInvitados;
        public string nombrePlayers;
        public int mipropiosocket;
        int cont;
        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            //pictureBox1.Image = Image.FromFile("foto_uno.gif");
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos nos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50064);

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
            button3.Enabled = false;
            button1.Enabled = false;
            salir.Enabled = false;
            nombre.Enabled = false;
            label1.Enabled = false;
            Consulta2.Enabled = false;
            Consulta3.Enabled = false;
            conectar.Enabled = true;
            button4.Enabled = false;
            texto.Enabled = false;
            Consulta1.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
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
                string[] trozos = Encoding.ASCII.GetString(msgr).Split('/'); // partimos por la barra, [] tienes al menos 2 strings
                int codigos;
                try
                {
                    codigos = Convert.ToInt32(trozos[0]);// el primer string lo convierte a numero
                    string mensaje;
                    int nform;
                    int men;
                    string el_nombre;
                    switch (codigos)
                    {
                        case 1: //Consulta 1
                            mensaje = trozos[1].Split('\0')[0];
                            Funcion1(mensaje);
                            break;
                        case 2://Consulta 2
                            mensaje = trozos[1].Split('\0')[0];
                            Funcion2(mensaje);
                            break;
                        case 3://Consulta3
                            mensaje = trozos[1].Split('\0')[0];
                            Funcion3(mensaje);
                            break;
                        case 4: //recibimos notificacion
                            mensaje = trozos[1].Split('\0')[0];
                            d1 t = new d1(ParticionLogin);
                            Invoke(t, new object[] { mensaje });

                            break;

                        case 6://Chat
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
                        case 12:
                            nform = Convert.ToInt32(trozos[1]);
                            mensaje = trozos[3].Split('\0')[0];
                            formularios[nform].Cartas_principio(mensaje);
                            int primeraCarta = Convert.ToInt32(trozos[2]);
                            formularios[nform].Espejo(primeraCarta);
                            break;
                        case 13:
                            nform = Convert.ToInt32(trozos[1]);
                            formularios[nform].TuTurno();
                            break;
                        case 14:
                            nform = Convert.ToInt32(trozos[1]);
                            men = Convert.ToInt32(trozos[2].Split('\0')[0]);
                            formularios[nform].Espejo(men);
                            break;
                        case 15:
                            nform = Convert.ToInt32(trozos[1]);
                            men = Convert.ToInt32(trozos[2].Split('\0')[0]);
                            formularios[nform].CartaRobada(men);
                            break;
                        case 16:
                            nform = Convert.ToInt32(trozos[1]);
                            el_nombre = trozos[2].Split('\0')[0];
                            formularios[nform].Notificacion_Ganador(el_nombre);
                            break;
                    }
                }

                catch (FormatException)
                {
                    string mensaje = "0/" + mipropionombre;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    MessageBox.Show("Error 404: Conexión No Encontrada! Reinicia la aplicación.");
                    
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    Application.Exit();
                    atender.Abort();
                    
                    
                }
            }
        }

        private void Funcion3(string mensaje)
        {
            string[] partidas = (mensaje).Split('.');
            int longitud = partidas.Length;
            int i = 0;

            while (i< longitud-1)
            {
                string[] info = partidas[i].Split(',');
                listBox2.Items.Add(info[0]);
                listBox4.Items.Add(info[1]);
                listBox5.Items.Add(info[2]);
                listBox6.Items.Add(info[3]);
                listBox7.Items.Add(info[4]);
                listBox8.Items.Add(info[5]);
                listBox9.Items.Add(info[6]);
                i++;
            }
        }
        private void Funcion1(string mensaje)
        {
            string[] nombres = (mensaje).Split(',');
            int i = 0;
            while (i< nombres.Length)
            {
                if (nombres[i] != mipropionombre)
                {
                    listBox3.Items.Add(nombres[i]);
                    i++;
                }
                else
                    i++;
            }
        }

        private void Funcion2(string mensaje)
        {
            string[] nombres = (mensaje).Split(',');
            int i = 0;
            while (i < nombres.Length)
            {
                if (nombres[i] == mipropionombre)
                {
                    listBox3.Items.Add("Victoria");
                    i++;
                }else if(nombres[i]=="-")
                {
                    listBox3.Items.Add("Faltan datos");
                    i++;
                }
                else
                {
                    listBox3.Items.Add("Derrota");
                    i++;
                }
            }
        }
        private void ParticionLogin(string mensaje)
        {
            string[] Socketinfo = (mensaje).Split(',');
            mipropiosocket = Convert.ToInt32(Socketinfo[0]);
            int SioNo = Convert.ToInt32( Socketinfo[1].Split('\0')[0]);
          
            if (SioNo == 0)
            {
                MessageBox.Show("Bienvenido a UNO!");
                nombre.Enabled = true;
                conectar.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;
                salir.Enabled = true;
                nombre.Enabled = true;
                label1.Enabled = true;
                Consulta2.Enabled = true;
                Consulta3.Enabled = true;
                conectar.Enabled = true;
                button4.Enabled = true;
                texto.Enabled = true;
                usuario.Enabled = false;
                contraseña.Enabled = false;
                iniciar.Enabled = false;
                registro.Enabled = false;
                Consulta1.Enabled = true;
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                comboBox1.Enabled = true;

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
             
            }
           
        }
        

        private void PonerenMarchaForm()
        {
            cont = formularios.Count;
            Partida t = new Partida(cont, server, mipropionombre, idPartida, posicionJug,numInvitados, nombrePlayers,mipropiosocket);
            formularios.Add(t);
            t.ShowDialog();
        }
        private void SIoNO(string mensaje)
        {
            string[] informacion = (mensaje).Split(',');
            string aceptacion = informacion[0];
            
            if (aceptacion == "SI")
            {
                idPartida = Convert.ToInt32(informacion[1]);
                posicionJug = Convert.ToInt32(informacion[2]);
                numInvitados = Convert.ToInt32(informacion[3]);
                nombrePlayers = informacion[4];
                ThreadStart ts = delegate { PonerenMarchaForm(); };
                Thread T = new Thread(ts);
                T.Start();
            }
            else if (aceptacion == "NO")
            {
                MessageBox.Show("Invitación rechazada");
            }

        }
        private void MeInvitan(string mensaje1, string primernombre)
        {
            try
            {
                cont = formularios.Count;
                if (MessageBox.Show("Te ha invitado " + mensaje1 + ".¿Aceptas la invitación?", "Invitación", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string mensaje = "10/" + mipropionombre + "/" + mensaje1 + "/1/" + mipropiosocket + "/" + cont;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);
                }
                else
                {
                    string mensaje = "10/" + mipropionombre + "/" + mensaje1 + "/0/" + mipropiosocket;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);
                }
            }
            catch (SocketException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();


            }
        }

        public void partir_lista_conectados(string message)
        {
            string[] Nombre_Usuarios = (message).Split(',');
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            int Numero_Conectados = Convert.ToInt32(Nombre_Usuarios[0]);
            int i = 0;
            while (Numero_Conectados > i)
            {
                string mens = Nombre_Usuarios[i + 1].Split(',')[0];
                listBox1.Items.Add(mens);
                comboBox2.Items.Add(mens);
                comboBox3.Items.Add(mens);
                comboBox4.Items.Add(mens);
                i++;
                comboBox2.Items.Remove(mipropionombre);
                comboBox3.Items.Remove(mipropionombre);
                comboBox4.Items.Remove(mipropionombre);
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
          
        }
        private void Funcion6(string mensaje)
        {
            listabox2.Items.Add(mensaje);
        }


        private void enviarinvitacion_Click_1(object sender, EventArgs e)
        {
            try
            {
                cont = formularios.Count;
                if (listBox1.SelectedItem == mipropionombre)
                {
                    MessageBox.Show("No puedes invitarte a tí mismo!");
                }
                if (listBox1.SelectedItem != null && listBox1.SelectedItem != mipropionombre)
                {
                    string invitar = listBox1.SelectedItem.ToString();
                    string mensaje = "9/" + mipropionombre + "/1," + invitar + "/" + mipropiosocket + "/" + cont;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);
                }
                else
                {
                    MessageBox.Show("No has seleccionado ningún nombre");
                }
            }
            catch (SocketException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }

        }

        private void salir_Click_1(object sender, EventArgs e)
        {
            try
            {

                button1.Enabled = false;
                salir.Enabled = false;
                nombre.Enabled = false;
                label1.Enabled = false;
                Consulta1.Enabled = false;
                Consulta2.Enabled = false;
                Consulta3.Enabled = false;
                conectar.Enabled = true;
                usuario.Enabled = true;
                contraseña.Enabled = true;
                iniciar.Enabled = true;
                registro.Enabled = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox6.Enabled = false;

                //Mensaje de desconexión
                string mensaje = "0/" + mipropionombre;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Nos desconectamos
                atender.Abort();// detenemos el thread
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                this.Close();
            }
            catch (SocketException)
            {
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            try
            {
                if (Consulta1.Checked)
                {
                    //Pedimos al server los nombres de la gente contra la que hemos jugado
                    string mensaje = "1/" + mipropionombre;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);
                }
                else if (Consulta2.Checked)
                {
                    //enviamos al servidor el nombre tecleado 
                    string mensaje = "2/" + mipropionombre +"/"+ nombre.Text;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);
                }
                else if (Consulta3.Checked)
                {
                    string mensaje = "3/" + mipropionombre;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);
                }
            }
            catch (SocketException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }

        }

        private void iniciar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = "4/" + usuario.Text + "/" + contraseña.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
                mipropionombre = usuario.Text;
            }
            catch (SocketException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                cont = formularios.Count;
                if (comboBox1.Text == "1")
                {
                    if (comboBox2.Text != null)
                    {
                        string mensaje = "9/" + mipropionombre + "/" + comboBox1.Text + "," + comboBox2.Text + "/" + mipropiosocket + "/" + cont;
                        byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msge);
                    }
                    else
                        MessageBox.Show("Te falta información bro");
                }
                else if (comboBox1.Text == "2")
                {
                    if (comboBox2.Text != null || comboBox3.Text!= null)
                    {
                        string mensaje = "9/" + mipropionombre + "/" + comboBox1.Text + "," + comboBox2.Text + "," + comboBox3.Text + "/" + mipropiosocket + "/" + cont;
                        byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msge);
                    }
                    else
                        MessageBox.Show("Te falta selecionar un jugador o más");
                }
                else if (comboBox1.Text == "3")
                {
                    if (comboBox2.Text != null || comboBox3.Text != null || comboBox4.Text != null)
                    {
                        string mensaje = "9/" + mipropionombre + "/" + comboBox1.Text + "," + comboBox2.Text + "," + comboBox3.Text + "," + comboBox4.Text + "/" + mipropiosocket + "/" + cont;
                        byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msge);
                    }
                    else
                        MessageBox.Show("Te falta selecionar un jugador o más");
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }

        }
        private void reg_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = "5/" + usuario.Text + "/" + contraseña.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
                iniciar.Enabled = true;
            }
            catch (SocketException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }
        }

        private void registro_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox6.Enabled = false;
            iniciar.Enabled = false;
            registro.Enabled = false;

        }

        private void conectar_Click_1(object sender, EventArgs e)
        {
             DialogResult result = MessageBox.Show("Estas seguro que quieres darte de baja del juego??", "Ganador", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    string mensaje = "15/" + mipropionombre;
                    byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msge);

                    mensaje = "0/" + mipropionombre;

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Nos desconectamos
                    atender.Abort();// detenemos el thread
                    this.BackColor = Color.Gray;
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    this.Close();
                }
                else if (result == DialogResult.Cancel)
                {

                }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                string mensaje = "6/" + mipropionombre + "/: " + texto.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
                texto.Text = null;
            }
            catch (SocketException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                atender.Abort();

            }
        }

        private void DameCon_Click(object sender, EventArgs e)
        {
            int i;
            if (listBox1.SelectedItem != null)
            {
                string seleccionado = listBox1.SelectedItem.ToString();
                cuenta++;
            }
            else
            {
                MessageBox.Show("no has seleccionado ningún nombre");
            }
        }

        private void INVITACION_Click(object sender, EventArgs e)
        {
            string consulta;
            consulta = "9";
            int usuariosAInvitar;
            usuariosAInvitar = listabox2.Items.Count;
            int i=1;
            string usuario;
            while (i<= cuenta)
            {
                usuario= listabox2.Items[i].ToString();
                consulta = consulta + "," + usuario;
            }
            string mensaje = "9/" + mipropionombre + "/" + cuenta + "," + consulta + "/" + mipropiosocket + "/" + cont;
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);


        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "1")
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
            }
            if (comboBox1.Text == "2")
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = false;
            }
            if (comboBox1.Text == "3")
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            string eliminar = comboBox2.Text;
            int i = 0;
            comboBox3.Items.Remove(eliminar);
            comboBox4.Items.Remove(eliminar);
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            string eliminar = comboBox3.Text;
            int i = 0;
            comboBox2.Items.Remove(eliminar);
            comboBox4.Items.Remove(eliminar);
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            string eliminar = comboBox4.Text;
            int i = 0;
            comboBox2.Items.Remove(eliminar);
            comboBox3.Items.Remove(eliminar);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        

        





      
  
  
   

      
    }
}
