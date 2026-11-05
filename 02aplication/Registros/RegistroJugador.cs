using Space_Invaders._02aplication;
using Space_Invaders._02aplication.Registros;
using Space_Invaders._03data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Space_Invaders._02aplication.Registros
{
    class RegistroJugador
    {
        private static string datosJugador = "..\\..\\..\\03data\\jugador.json";

        public static Jugador? CargarJugador()
        {
            Jugador? jugador;
            bool hayFichero = File.Exists(datosJugador);
            jugador = hayFichero ? (Jugador)Serializador.CargarJugador(datosJugador) : null;

            return jugador;
        }

        public static void RegistrarDatos(Jugador jugador)
        {
            Serializador.Transcribir(datosJugador, jugador);
        }
    }
}
