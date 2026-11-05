using System;
using static System.Collections.Specialized.BitVector32;
using Space_Invaders._01view;
using Space_Invaders._02aplication.Registros;

namespace Space_Invaders
{
    class Juego()
    {
        static void Main()
        {
            Bienvenida saludo = new Bienvenida();
            RegistroLogs.BorrarLog();

            Console.WindowWidth = Globales.ANCHO_PANTALLA;
            Console.WindowHeight = Globales.ALTO_PANTALLA;
            
            saludo.Lanzar();
            RegistroLogs.RegistrarEnLog("Juego finalizado");
        }
    }
}