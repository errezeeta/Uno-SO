using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            centro.Image = pictureBox1.Image;
            tTurno.Text = "0";
            tiempTurno.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            centro.Image = pictureBox2.Image;
            tTurno.Text = "0";
            tiempTurno.Start();
        }
        
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            centro.Image = pictureBox3.Image;
            tTurno.Text = "0";
            tiempTurno.Start();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            centro.Image = pictureBox4.Image;
            tTurno.Text = "0";
            tiempTurno.Start();
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            centro.Image = pictureBox5.Image;
            tTurno.Text = "0";
            tiempTurno.Start();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            centro.Image = pictureBox6.Image;
            tTurno.Text = "0";
            tiempTurno.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            tPartida.Text = "0";
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox5.BackColor = Color.Transparent;
            pictureBox6.BackColor = Color.Transparent;
            pictureBox7.BackColor = Color.Transparent;
            pictureBox10.BackColor = Color.Transparent;
            centro.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label2.Text = "Rival 1";
            label3.BackColor = Color.Transparent;
            label3.Text = "Rival 2";
            label4.BackColor = Color.Transparent;
            label4.Text = "Rival 3";
            pictureBox7.Image = Image.FromFile("Baraja\\" + "user.PNG");
            pictureBox8.Image = Image.FromFile("Baraja\\" + "user.PNG");
            pictureBox9.Image = Image.FromFile("Baraja\\" + "user.PNG");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Baraja Mano = new Baraja();
            Mano.ReparteCartas();
            pictureBox1.Image = Image.FromFile(Mano.DameImagen(0));
            pictureBox2.Image = Image.FromFile(Mano.DameImagen(1));
            pictureBox3.Image = Image.FromFile(Mano.DameImagen(2));
            pictureBox4.Image = Image.FromFile(Mano.DameImagen(3));
            pictureBox5.Image = Image.FromFile(Mano.DameImagen(4));
            pictureBox6.Image = Image.FromFile(Mano.DameImagen(5));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Double tiempoPartida = Convert.ToDouble(tPartida.Text);
            tiempoPartida = tiempoPartida + 1;
            tPartida.Text = tiempoPartida.ToString();
        }

        private void tiempTurno_Tick(object sender, EventArgs e)
        {
            Double tiempoTurno = Convert.ToDouble(tTurno.Text);
            tiempoTurno = tiempoTurno + 1;
            tTurno.Text = tiempoTurno.ToString();
        }

        
       

        

        
    }
}
