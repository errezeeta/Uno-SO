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
using System.IO;

namespace Form1
{
    public partial class Partida : Form
    {
        int nForm;
        Socket server;
        string mipropionombre;
        int idPartida;
        int posicion;
        int numInvitados;
        int numInvitados1;
        Carta[] mano = new Carta[39];
        Carta[] misCartas = new Carta[15];
        int[] CartasOcupadas = new int[10];
        int movimiento;
        int Contador = 0;
        int eliminar;
        int miSocket;
        Carta central = new Carta();
        Boolean PuedesRobar = false;
        string[] nombresPlayers = new String[4];
        public Partida(int nForm, Socket server, string mipropionombre, int idPartida, int posicion, int numInvitado, string names,int mysock)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.mipropionombre = mipropionombre;
            MiNombre.Text = mipropionombre;
            this.idPartida = idPartida;
            this.posicion = posicion;
            this.miSocket = mysock;
            this.numInvitados1 = numInvitado;
            this.numInvitados = numInvitados1 - 1;
            nombresPlayers = (names).Split('<');
            ColocaNombres();
            if (idPartida == 1)
            {
                this.BackColor = Color.PaleGreen;
            }
            if (idPartida == 2)
            {
                this.BackColor = Color.Aqua;
            }
            if (idPartida == 3)
            {
                this.BackColor = Color.Khaki;
            }
            if (idPartida == 4)
            {
                this.BackColor = Color.MediumSlateBlue;
            }
            if (idPartida == 5)
            {
                this.BackColor = Color.Silver;
            }
            carta1.Enabled = false;
            carta2.Enabled = false;
            carta3.Enabled = false;
            carta4.Enabled = false;
            carta5.Enabled = false;
            carta6.Enabled = false;
            carta7.Enabled = false;
            carta8.Enabled = false;
            carta9.Enabled = false;
            carta10.Enabled = false;
            carta11.Enabled = false;
            carta12.Enabled = false;
            carta13.Enabled = false;
            carta14.Enabled = false;
            tira.Enabled = false;
            robar.Image = Image.FromFile("Baraja\\back.jpg");
            if (posicion == 0)
            {
                
                tira.Enabled = true;
            }


        }
        

        private void ColocaNombres()
        {
            int i=0;
            Boolean MyNameFound = false;
            while (i < 2 && MyNameFound==false)
            {
                if (nombresPlayers[i] == mipropionombre)
                {
                    MyNameFound = true;
                }
                else
                    i++;
            }
            if (i == 0)
            {
                if (numInvitados == 3)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Text = nombresPlayers[2];
                    oeste.Text = nombresPlayers[3];
                }
                if (numInvitados == 2)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Text = nombresPlayers[2];
                    oeste.Hide();
                }
                if (numInvitados == 1)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Hide();
                    oeste.Hide();
                }
            }
            else if (i == 1)
            {
                if (numInvitados == 3)
                {
                    Norte.Text = nombresPlayers[0];
                    este.Text = nombresPlayers[2];
                    oeste.Text = nombresPlayers[3];
                }
                if (numInvitados == 2)
                {
                    Norte.Text = nombresPlayers[0];
                    este.Text = nombresPlayers[2];
                    oeste.Hide();
                }
                if (numInvitados == 1)
                {
                    Norte.Text = nombresPlayers[0];
                    este.Hide();
                    oeste.Hide();
                }
            }
            else if (i == 2)
            {
                if (numInvitados == 3)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Text = nombresPlayers[0];
                    oeste.Text = nombresPlayers[3];
                }
                if (numInvitados == 2)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Text = nombresPlayers[0];
                    oeste.Hide();
                }
                if (numInvitados == 1)
                {
                    Norte.Text = nombresPlayers[0];
                    este.Hide();
                    oeste.Hide();
                }
            }
            else if (i == 3)
            {
                if (numInvitados == 3)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Text = nombresPlayers[2];
                    oeste.Text = nombresPlayers[0];
                }
                if (numInvitados == 2)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Text = nombresPlayers[2];
                    oeste.Hide();
                }
                if (numInvitados == 1)
                {
                    Norte.Text = nombresPlayers[1];
                    este.Hide();
                    oeste.Hide();
                }
            }
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
            try
            {
                string mensaje = "11/" + nForm + "/" + idPartida + "/" + mipropionombre + "/: " + texto.Text;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
                texto.Text = null;
            }
            catch (SocketException)
            {
                MessageBox.Show("Error 400. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
            

            }

        }
        private void PonCartasInicio(Carta[] misCartas)
        {
            int i = 0;
            while (i < 5)
            {
                ColocaCarta(misCartas[i], i);
                DejarEnOcupado(i);
                i++;
            }
        }
        private int TiraCarta(Carta usada)
        {
            int SePuede;
            SePuede = CompruebaMovimiento(central, usada);
            if (SePuede == 1)
            {
                cartaCentro.Image = Image.FromFile(usada.GetRuta());
                movimiento = usada.GetId();
                Contador--;

                if (Contador == 0)
                {
                    Ganador(movimiento);
                    return 2;
                }
                else
                {
                    Funcion13(movimiento);
                    if (Contador == 1)
                        Uno(Contador);
                }
               
                return 1;
            }
            else
                return 0; 
        }

        private void Uno(int Contador)
        {

            string mensaje = "11/" + nForm + "/" + idPartida + "/" + mipropionombre + "/: " + "A este usuario le queda UNA CARTA";
            byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msge);

        }
        private void Ganador(int movimiento)
        {
            try
            {

                string mensaje = "13/" + mipropionombre + "/" + idPartida + "/" + numInvitados + "/" + posicion + "/" + movimiento + "/1";
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
                carta1.Enabled = false;
                carta2.Enabled = false;
                carta3.Enabled = false;
                carta4.Enabled = false;
                carta5.Enabled = false;
                carta6.Enabled = false;
                carta7.Enabled = false;
                carta8.Enabled = false;
                carta9.Enabled = false;
                carta10.Enabled = false;
                carta11.Enabled = false;
                carta12.Enabled = false;
                carta13.Enabled = false;
                carta14.Enabled = false;
                Turno.Text = " ";
                PasaTurno.Hide();
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
                

            }

        }
        public void Notificacion_Ganador(string men)
        {
            carta1.Hide();
            carta2.Hide();
            carta3.Hide();
            carta4.Hide();
            carta5.Hide();
            carta6.Hide();
            carta7.Hide();
            carta8.Hide();
            carta9.Hide();
            carta10.Hide();
            carta12.Hide();
            carta11.Hide();
            carta13.Hide();
            carta14.Hide();
            if (mipropionombre != men)
            {
                DialogResult result = MessageBox.Show("El ganador es " + men, "Ganador", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.Close();

                }
                else if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("¡Has ganado, felicidades 😊" + men, "Ganador", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.Close();

                }
                else if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
        }
        private int CompruebaMovimiento(Carta central, Carta tirada)
        {
            try
            {
                if (central.GetColor() == tirada.GetColor() || tirada.GetNumero() == central.GetNumero())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (FileNotFoundException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 404.Reinicia la aplicación.Lo sentimos mucho");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                return 0;
            }
               

            
        }
        private void RobaCarta (Carta robada)
        {
            int i = 0;
            while (i < 7)
            {
                if (CartasOcupadas[i] == 0)
                {
                    ColocaCarta(robada, i);
                }
                else
                    i++;
            }
        }
        private void DejarEnOcupado(int posicion)
        {
            if (posicion == 0)
            {
                CartasOcupadas[0] = 1;
            }
            else if (posicion == 1)
            {
                CartasOcupadas[1] = 1;
            }
            else if (posicion == 2)
            {
                CartasOcupadas[2] = 1;
            }
            else if (posicion == 3)
            {
                CartasOcupadas[3] = 1;
            }
            else if (posicion == 4)
            {
                CartasOcupadas[4] = 1;
            }
            else if (posicion == 5)
            {
                CartasOcupadas[5] = 1;
            }
            else if (posicion == 6)
            {
                CartasOcupadas[6] = 1;
            }
            else if (posicion == 7)
            {
                CartasOcupadas[7] = 1;
            }
            else if (posicion == 8)
            {
                CartasOcupadas[8] = 1;
            }
            else if (posicion == 9)
            {
                CartasOcupadas[9] = 1;
            }
            else if (posicion == 10)
            {
                CartasOcupadas[10] = 1;
            }
            else if (posicion == 11)
            {
                CartasOcupadas[11] = 1;
            }
            else if (posicion == 12)
            {
                CartasOcupadas[12] = 1;
            }
            else if (posicion == 13)
            {
                CartasOcupadas[13] = 1;
            }
            else if (posicion == 14)
            {
                CartasOcupadas[14] = 1;
            }
            
        }
        public void Espejo(int movimiento)
        {
            try
            {
                if (movimiento < 116)
                {
                    if (central.GetId() != movimiento)
                    {
                        anterior.Image = cartaCentro.Image;
                    }
                    cartaCentro.Image = Image.FromFile("Baraja\\" + movimiento + ".PNG");
                    central.SetId(movimiento);
                }
            }
            catch (FileNotFoundException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 404. Reinicia la aplicación");
               
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
                

            }
            
        }
        public void Funcion13(int movimiento)
        {
            try
            {
                string mensaje = "13/" + mipropionombre + "/" + idPartida + "/" + numInvitados + "/" + posicion + "/" + movimiento + "/0";
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
                carta1.Enabled = false;
                carta2.Enabled = false;
                carta3.Enabled = false;
                carta4.Enabled = false;
                carta5.Enabled = false;
                carta6.Enabled = false;
                carta7.Enabled = false;
                carta8.Enabled = false;
                carta9.Enabled = false;
                carta10.Enabled = false;
                carta11.Enabled = false;
                carta12.Enabled = false;
                carta13.Enabled = false;
                carta14.Enabled = false;
                Turno.Text = " ";
                PasaTurno.Hide();
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
                

            }
        }
        public void Funcion12()
        {
            try
            {

                string mensaje = "12/" + mipropionombre + "/" + idPartida + "/" + numInvitados + "/" + posicion;
                byte[] msge = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msge);
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
               

            }
        }
        public void TuTurno()
        {
            Turno.Text = "Te toca!";
            PasaTurno.Show();
            PuedesRobar = true;
            if (misCartas[0]!=null)
            {
                carta1.Enabled = true;
            }
            if (misCartas[1] != null)
            {
                carta2.Enabled = true;
            }
            if (misCartas[2] != null)
            {
                carta3.Enabled = true;
            }
            if (misCartas[3] != null)
            {
                carta4.Enabled = true;
            }
            if (misCartas[4] != null)
            {
                carta5.Enabled = true;
            }
            if (misCartas[5] != null)
            {
                carta6.Enabled = true;
            }
            if (misCartas[6] != null)
            {
                carta7.Enabled = true;
            }
            if (misCartas[7] != null)
            {
                carta8.Enabled = true;
            }
            if (misCartas[8] != null)
            {
                carta9.Enabled = true;
            }
            if (misCartas[9] != null)
            {
                carta10.Enabled = true;
            }
            if (misCartas[10] != null)
            {
                carta11.Enabled = true;
            }
            if (misCartas[11] != null)
            {
                carta12.Enabled = true;
            }
            if (misCartas[12] != null)
            {
                carta13.Enabled = true;
            }
            if (misCartas[13] != null)
            {
                carta14.Enabled = true;
            }
        }
        public void Cartas_principio(string mensaje)
        {
            string[] cartitas = mensaje.Split(',');
            int i=0;
            int carta;
            while (i < 5)
            {
                carta = Convert.ToInt32(cartitas[i]);
                misCartas[i] = new Carta();
                misCartas[i].SetId(carta);
                i++;
                Contador++;
            }
            PonCartasInicio(misCartas);
        }
        public void Mazo_principio(string mensaje)
        {
            string[] cartitas = mensaje.Split(',');
            int i = 0;
            int carta;
            while (i < 39)
            {
                carta = Convert.ToInt32(cartitas[i]);
                mano[i] = new Carta();
                mano[i].SetId(carta);
            }
        }
        private void ColocaCarta(Carta nueva, int i)
        {
            try
            {
                if (i == 0)
                {
                    carta1.Image = Image.FromFile(nueva.GetRuta());
                    carta1.Show();
                    carta1.Enabled = true;
                }
                else if (i == 1)
                {
                    carta2.Image = Image.FromFile(nueva.GetRuta());
                    carta2.Show();
                    carta2.Enabled = true;
                }
                else if (i == 2)
                {
                    carta3.Image = Image.FromFile(nueva.GetRuta());
                    carta3.Show();
                    carta3.Enabled = true;
                }
                else if (i == 3)
                {
                    carta4.Image = Image.FromFile(nueva.GetRuta());
                    carta4.Show();
                    carta4.Enabled = true;
                }
                else if (i == 4)
                {
                    carta5.Image = Image.FromFile(nueva.GetRuta());
                    carta5.Show();
                    carta5.Enabled = true;
                }
                else if (i == 5)
                {
                    carta6.Image = Image.FromFile(nueva.GetRuta());
                    carta6.Show();
                    carta6.Enabled = true;
                }
                else if (i == 6)
                {
                    carta7.Image = Image.FromFile(nueva.GetRuta());
                    carta7.Show();
                    carta7.Enabled = true;
                }
                else if (i == 7)
                {
                    carta8.Image = Image.FromFile(nueva.GetRuta());
                    carta8.Show();
                    carta8.Enabled = true;
                }
                else if (i == 8)
                {
                    carta9.Image = Image.FromFile(nueva.GetRuta());
                    carta9.Show();
                    carta9.Enabled = true;
                }
                else if (i == 9)
                {
                    carta10.Image = Image.FromFile(nueva.GetRuta());
                    carta10.Show();
                    carta10.Enabled = true;
                }
                else if (i == 10)
                {
                    carta11.Image = Image.FromFile(nueva.GetRuta());
                    carta11.Show();
                    carta11.Enabled = true;
                }
                else if (i == 11)
                {
                    carta12.Image = Image.FromFile(nueva.GetRuta());
                    carta12.Show();
                    carta12.Enabled = true;
                }
                else if (i == 12)
                {
                    carta13.Image = Image.FromFile(nueva.GetRuta());
                    carta13.Show();
                    carta13.Enabled = true;
                }
                else if (i == 13)
                {
                    carta14.Image = Image.FromFile(nueva.GetRuta());
                    carta14.Show();
                    carta14.Enabled = true;
                }
            }
            catch (FileNotFoundException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 404. Reinicia la aplicación.");
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
               

            }
        }
        public void carta1_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[0];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta1.Hide();
                misCartas[0] = null;
            }
            if (eliminar==2)
            {
                carta1.Hide();
                misCartas[0] = null;
            }
        }
        private void carta2_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[1];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                misCartas[1] = null;
                carta2.Hide();
            }
            if (eliminar == 2)
            {
                carta2.Hide();
                misCartas[1] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta3_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[2];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta3.Hide();
                misCartas[2] = null;
            }
            if (eliminar == 2)
            {
                carta3.Hide();
                misCartas[2] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta4_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[3];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta4.Hide();
                misCartas[3] = null;
            }
            if (eliminar == 2)
            {
                carta4.Hide();
                misCartas[3] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta5_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[4];
            eliminar=TiraCarta(usada);
            if (eliminar == 1)
            {
                misCartas[4] = null;
                carta5.Hide();
            }
            if (eliminar == 2)
            {
                carta5.Hide();
                misCartas[4] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta6_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[5];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta6.Hide();
                misCartas[5] = null;
            }
            if (eliminar == 2)
            {
                carta6.Hide();
                misCartas[5] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta7_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[6];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta7.Hide();
                misCartas[6] = null;
            }
            if (eliminar == 2)
            {
                carta7.Hide();
                misCartas[6] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta8_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[7];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta8.Hide();
                misCartas[7] = null;
            }
            if (eliminar == 2)
            {
                carta8.Hide();
                misCartas[7] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta9_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[8];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta9.Hide();
                misCartas[8] = null;
            }
            if (eliminar == 2)
            {
                carta9.Hide();
                misCartas[8] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta10_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[9];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta10.Hide();
                misCartas[9] = null;
            }
            if (eliminar == 2)
            {
                carta10.Hide();
                misCartas[9] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta11_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[10];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta11.Hide();
                misCartas[10] = null;
            }
            if (eliminar == 2)
            {
                carta11.Hide();
                misCartas[10] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta12_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[11];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta12.Hide();
                misCartas[11] = null;
            }
            if (eliminar == 2)
            {
                carta12.Hide();
                misCartas[11] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta13_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[12];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta13.Hide();
                misCartas[12] = null;
            }
            if (eliminar == 2)
            {
                carta13.Hide();
                misCartas[12] = null;
                Ganador(usada.GetId());
            }
        }
        private void carta14_Click(object sender, EventArgs e)
        {
            Carta usada = misCartas[13];
            eliminar = TiraCarta(usada);
            if (eliminar == 1)
            {
                carta14.Hide();
                misCartas[13] = null;
            }
            if (eliminar == 2)
            {
                carta14.Hide();
                misCartas[13] = null;
                Ganador(usada.GetId());
            }
        }
        private void tira_Click(object sender, EventArgs e)
        {
            tira.Enabled = false;
            Funcion12();
            TuTurno();
        }  
        private void ImagenRobada(Carta robada, int posicion)
        {
            try
            {
                if (posicion == 0)
                {
                    carta1.Image = Image.FromFile(robada.GetRuta());
                    carta1.Show();
                    carta1.Enabled = true;
                }
                else if (posicion == 1)
                {
                    carta2.Image = Image.FromFile(robada.GetRuta());
                    carta2.Show();
                    carta2.Enabled = true;
                }
                else if (posicion == 2)
                {
                    carta3.Image = Image.FromFile(robada.GetRuta());
                    carta3.Show();
                    carta3.Enabled = true;
                }
                else if (posicion == 3)
                {
                    carta4.Image = Image.FromFile(robada.GetRuta());
                    carta4.Show();
                    carta4.Enabled = true;
                }
                else if (posicion == 4)
                {
                    carta5.Image = Image.FromFile(robada.GetRuta());
                    carta5.Show();
                    carta5.Enabled = true;
                }
                else if (posicion == 5)
                {
                    carta6.Image = Image.FromFile(robada.GetRuta());
                    carta6.Show();
                    carta6.Enabled = true;
                }
                else if (posicion == 6)
                {
                    carta7.Image = Image.FromFile(robada.GetRuta());
                    carta7.Show();
                    carta7.Enabled = true;
                }
                else if (posicion == 7)
                {
                    carta8.Image = Image.FromFile(robada.GetRuta());
                    carta8.Show();
                    carta8.Enabled = true;
                }
                else if (posicion == 8)
                {
                    carta9.Image = Image.FromFile(robada.GetRuta());
                    carta9.Show();
                    carta9.Enabled = true;
                }
                else if (posicion == 9)
                {
                    carta10.Image = Image.FromFile(robada.GetRuta());
                    carta10.Show();
                    carta10.Enabled = true;
                }
                else if (posicion == 10)
                {
                    carta11.Image = Image.FromFile(robada.GetRuta());
                    carta11.Show();
                    carta11.Enabled = true;
                }
                else if (posicion == 11)
                {
                    carta12.Image = Image.FromFile(robada.GetRuta());
                    carta12.Show();
                    carta12.Enabled = true;
                }
                else if (posicion == 12)
                {
                    carta13.Image = Image.FromFile(robada.GetRuta());
                    carta13.Show();
                    carta13.Enabled = true;
                }
                else if (posicion == 13)
                {
                    carta14.Image = Image.FromFile(robada.GetRuta());
                    carta14.Show();
                    carta14.Enabled = true;

                }
            }
            catch (FileNotFoundException)
            {
                string mensaje = "0/" + mipropionombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Error 404. Reinicia la aplicación.");
                
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Application.Exit();
               

            }
        }
        private void robar_Click(object sender, EventArgs e)
        {
            try
            {
                if (PuedesRobar == true)
                {
                    string mensaje = "14/" + mipropionombre + "/" + idPartida + "/" + posicion;
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
                

            }
        }
        public void CartaRobada(int robada)
        {
            Carta CartaRobada = new Carta();
            CartaRobada.SetId(robada);
            
                int i;
                i = 0;
                Boolean encontrado = false;
                while (i < 14 && encontrado == false)
                {
                    if (misCartas[i] == null)
                    {
                        misCartas[i] = CartaRobada;
                        ImagenRobada(CartaRobada, i);
                        encontrado = true;
                        PuedesRobar = false;
                        Contador++;
                    }
                    else
                        i++;
                }
        }
        private void PasaTurno_Click(object sender, EventArgs e)
        {
            Funcion13(central.GetId());
        }

        private void revancha_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    int cont = nForm;
                    string invitacion = string.Empty;
                    if (numInvitados == 1)
                    {
                        invitacion = "1," + Norte.Text;
                    }
                    if (numInvitados == 2)
                    {
                        invitacion = "2," + Norte.Text + "," + este.Text;
                    }
                    if (numInvitados == 3)
                    {
                        invitacion = "3," + Norte.Text + "," + este.Text + "," + oeste.Text;
                    }
                    //string invitar = listBox1.SelectedItem.ToString();
                    string mensaje = "9/" + mipropionombre + "/" + invitacion + "/" + miSocket + "/" + cont;
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
                

            }
        }

    }
}
