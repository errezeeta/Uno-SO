using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Baraja
    {
        //Atributos
        Cartas[] baraja= new Cartas[8];
        public void ReparteCartas()
        {
            int i = 0;
            string color = "Hello";
            Random rnd = new Random();
            while (i < 8)
            {
                this.baraja[i] = new Cartas();
                int numero = rnd.Next(0, 13);
                int clr = rnd.Next(1, 6);
                if (clr == 1)
                { color = "b"; }
                if (clr == 2)
                { color = "r"; }
                if (clr == 3)
                { color = "y"; }
                if (clr == 4)
                { color = "g"; }
                if (clr == 5)
                { color = "n";
                    if (numero < 6)
                        numero = 13;
                    else
                        numero = 14;
                }
                this.baraja[i].SetRuta(numero, color);
                i++;
            }
        }
        public string DameImagen(int numero)
    {
        return this.baraja[numero].GetRuta();
    }


    }
}
