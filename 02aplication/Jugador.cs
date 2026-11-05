using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders._02aplication
{
    class Jugador
    {
        private string nombre;
        private int record;
        private int puntos;

        public string Nombre 
        {
            get { return nombre; }
            set 
            {
                bool esNull = value == null;
                nombre = esNull ? "Unknown" : value; 
            }
        }
        public int Record
        {
            get { return record; }
            set
            {
                bool esNull = value == null;
                record = esNull ? 0 : value;
            }
        }
        public int Puntos
        {
            get { return puntos; }
            set
            {
                bool esNull = value == null;
                puntos = esNull ? 0 : value;
            }
        }

        public Jugador() { }
        public Jugador(string nombre, int record, int puntos)
        {
            Nombre = nombre;
            Record = record;
            Puntos = puntos;
        }
    }
}
