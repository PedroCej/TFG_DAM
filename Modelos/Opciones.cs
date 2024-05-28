using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTFG.Modelos
{
    public class Opciones
    {
        public string Tema { get; set; }
        public string Idioma { get; set; }
        public string TamLetra { get; set; }

        public Opciones() {
            Tema = "default";
            Idioma = "default";
            TamLetra = "default";
        }

        public Opciones(string tema, string idioma, string tamLetra)
        {
            Tema = tema;
            Idioma = idioma;
            TamLetra = tamLetra;
        }


    }
}
