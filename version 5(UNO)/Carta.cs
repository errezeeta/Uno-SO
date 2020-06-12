using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Form1
{
    class Carta
    {   //Atributos
        int numero;
        char color;
        int id;
        string ruta;
        //Constructor
        public Carta()
        {
        }
        public void SetId(int id)
        {
            this.id = id;
            if (id < 10)
                //Asignacion del color
            {
                this.color = 'b';
            }
            else if (id > 9 && id < 20)
            {
                this.color = 'g';
            }
            else if (id > 19 && id < 30)
            {
                this.color = 'r';
            }
            else if (id < 29)
            {
                this.color = 'y';
            }
                //Asignacion del numero
            if ( id==0 ||id==10 ||id==20 ||id==30)
            {
                this.numero = 0;
            }
            else if(id == 1 || id == 11 || id == 21 || id == 31)
            {
                this.numero = 1;
            }
            else if (id == 2 || id == 12 || id == 22 || id == 32)
            {
                this.numero = 2;
            }
            else if (id == 3 || id == 13 || id == 23 || id == 33)
            {
                this.numero = 3;
            }
            else if (id == 4 || id == 14 || id == 24 || id == 34)
            {
                this.numero = 4;
            }
            else if (id == 5 || id == 15 || id == 25 || id == 35)
            {
                this.numero = 5;
            }
            else if (id == 6 || id == 16 || id == 26 || id == 36)
            {
                this.numero = 6;
            }
            else if (id == 7 || id == 17 || id == 27 || id == 37)
            {
                this.numero = 7;
            }
            else if (id == 8 || id == 18 || id == 28 || id == 38)
            {
                this.numero = 8;
            }
            else if (id == 9 || id == 19 || id == 29 || id == 39)
            {
                this.numero = 9;
            }
                //Asignación de ruta
           this.ruta = "Baraja\\" + id + ".PNG";
        }
        public int GetId()
        {
            return this.id;
        }

        public string GetRuta()
        {
            return this.ruta;
        }

        public char GetColor()
        {
            return this.color;
        }

        public int GetNumero()
        {
            return this.numero;
        }

    }
}
