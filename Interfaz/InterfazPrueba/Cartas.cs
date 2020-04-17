using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Cartas

    {
        
        //atributos
        int id;
        int numero;
        int color;
        string ruta;

        //constructor
        public Cartas()
        {
        }
        public void SetRuta(int numero, string color)
        {   this.ruta= "Baraja\\"+numero+color+".PNG";
        }
        public string GetRuta()
        {
            return this.ruta;
        }
 
    }
}
