using System;
using System.Drawing;
using System.Runtime.Serialization.Formatters;
using Space_Invaders._02aplication;

namespace Space_Invaders._01view
{
    class HUD
    {
        private const int ANCHO_PANTALLA = Globales.ANCHO_PANTALLA;
        private const int SUELO_PARTIDA = Globales.SUELO_PARTIDA;
        private const int INICIO_HUD = Globales.INICIO_HUD;
        private const int CENTRO_X = ANCHO_PANTALLA / 2;
        private int recordJugador = Globales.recordJugador;

        private ConsoleColor colorInterfaz;

        public HUD()
        {
            colorInterfaz = ConsoleColor.DarkGray;
        }

        // ====================MARCADORES DEL HUD====================
        private void DibujarBordesMarcador()
        {
            Console.ForegroundColor = colorInterfaz;
            Console.Write("**");
        }
        public void ActualizarContadorVidas(Nave nave, bool finDePartida)
        {
            int X = finDePartida ? CENTRO_X + 8 : CENTRO_X + 15;
            int Y = finDePartida ? INICIO_HUD - 10 : INICIO_HUD + 2;
            int particion = Globales.VIDAS_INICIALES / 3;
            int vidasNave = nave.Vidas;
            ConsoleColor colorVidas = vidasNave switch
            {
                0                               => ConsoleColor.DarkGray,
                var v when v <= particion       => ConsoleColor.DarkRed,
                var v when v <= particion * 2   => ConsoleColor.DarkYellow,
                _                               => ConsoleColor.DarkGreen
            };

            Console.SetCursorPosition(X, Y);
            DibujarBordesMarcador();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0}", nave.Imagen);
            Console.ForegroundColor = colorInterfaz;
            Console.Write(" x ");
            Console.ForegroundColor = colorVidas;
            Console.Write(vidasNave);

            DibujarBordesMarcador();
        }
        public void ActualizarMarcadorBotiquines(Nave nave, bool finDePartida)
        {
            int X = finDePartida ? CENTRO_X - 17 : CENTRO_X - 30;
            int Y = finDePartida ? INICIO_HUD - 10 : INICIO_HUD + 2;
            int botiquinesNave = nave.Botiquines;
            bool tieneBotiquin = botiquinesNave > 0;

            Console.SetCursorPosition(X, Y);
            DibujarBordesMarcador();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("+");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("]");
            Console.ForegroundColor = colorInterfaz;
            Console.Write(" x ");
            Console.ForegroundColor = tieneBotiquin ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
            Console.Write(botiquinesNave);

            DibujarBordesMarcador();
        }
        public void ActualizarMarcadorPuntos(Nave nave)
        {
            int X = CENTRO_X - 8;
            int Y = INICIO_HUD + 2;
            int puntosNave = nave.Puntos;
            ConsoleColor colorPuntos = puntosNave switch
            {
                0                               => ConsoleColor.DarkRed,
                var p when p >= recordJugador   => ConsoleColor.DarkGreen,
                _                               => ConsoleColor.Yellow
            };

            Console.SetCursorPosition(X, Y);
            DibujarBordesMarcador();

            Console.Write("Pts x ");
            Console.ForegroundColor = colorPuntos;
            Console.Write(puntosNave);
            Console.ForegroundColor = colorInterfaz;
            Console.Write(" // ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(recordJugador);

            DibujarBordesMarcador();
        }
        public void ActualizarContadorDisparos(Nave nave)
        {
            int X = CENTRO_X;
            int Y = INICIO_HUD;
            int longitudBordes = 4;
            int balasTotales = Globales.BALAS_NAVE;
            int disparosRestantes = nave.ContrarDisparos();
            
            int mitadMarcador = (balasTotales + longitudBordes) / 2;
            int particion = balasTotales / 4;
            int mitadContador = balasTotales / 2;
            ConsoleColor colorDeBala = disparosRestantes switch
            {
                var d when d <= particion           => ConsoleColor.DarkRed,
                var d when d <= mitadContador       => ConsoleColor.DarkYellow,
                _                                   => ConsoleColor.DarkGreen,
            };

            Console.SetCursorPosition(X - mitadMarcador, Y);
            DibujarBordesMarcador();

            for (int i = 1; i <= balasTotales; i++)
            {
                bool balaGuardada = i <= disparosRestantes;

                Console.ForegroundColor = balaGuardada ? colorDeBala : colorInterfaz;
                Console.Write(balaGuardada ? "|" : "-");
            }

            DibujarBordesMarcador();
        }


        // ====================HUD FIN DE PARTIDA====================
        public void DibujarFinDePartida(Nave nave, bool finDePartida, bool partidaGanada, int puntosFinales)
        {
            bool nuevoRecord = puntosFinales > recordJugador;
            int incremento = 0;
            int ancho = 40;
            int alto = 12;

            int centroX = ANCHO_PANTALLA / 2;
            int centroY = SUELO_PARTIDA / 2;
            int inicioX = centroX - (ancho / 2);
            int inicioY = centroY - (alto / 2);

            int posicionMarcadores = inicioY + 10;

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i <= alto; i++)
            {
                Console.SetCursorPosition(inicioX, inicioY + incremento);
                for (int j = 0; j <= ancho; j++)
                {
                    bool esBorde = i == 0 || j == 0 || i == alto || j == ancho;
                    Console.Write(esBorde ? "*" : " ");
                }
                incremento++;
            }

            DibujarGanadaOPerdida(nave, centroX, inicioY, partidaGanada);
            MostrarNumerosFinales(nave, centroX, inicioY, puntosFinales, recordJugador);

            if (partidaGanada)
            {
                Globales.puntosJugador += puntosFinales;
                if (nuevoRecord)
                    MostrarNuevoRecord(nave, centroX, inicioY, puntosFinales);
            }

            ActualizarMarcadorBotiquines(nave, finDePartida);
            ActualizarContadorVidas(nave, finDePartida);

            Console.ResetColor();
        }
        private void DibujarGanadaOPerdida(Nave nave, int posX, int posY, bool partidaGanada)
        {
            ConsoleColor colorMensaje = partidaGanada ? ConsoleColor.Green : ConsoleColor.DarkRed;
            string mensaje = partidaGanada ? ">>¡HAS GANADO!<<" : ">>¡HAS PERDIDO!<<";

            Console.SetCursorPosition(posX - (mensaje.Length / 2), posY + 2);
            Console.ForegroundColor = colorMensaje;
            Console.Write(mensaje);
        }
        private void MostrarNumerosFinales(Nave nave, int posX, int posY, int puntosFinales, int puntuacionMaxima)
        {
            string mensajePuntos = "PUNTUACIÓN: " + puntosFinales;
            string mensajeRecord = "MAYOR RECORD: " + puntuacionMaxima;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(posX - (mensajePuntos.Length / 2), posY + 4);
            Console.Write(mensajePuntos);
            Console.SetCursorPosition(posX - (mensajeRecord.Length / 2), posY + 5);
            Console.Write(mensajeRecord);
        }
        private void MostrarNuevoRecord(Nave nave, int posX, int posY, int puntosFinales)
        {
            string mensajeNuevoRecord = "¡¡NUEVO RECORD!!";

            Globales.recordJugador = puntosFinales;
            Console.SetCursorPosition(posX - (mensajeNuevoRecord.Length / 2), posY + 7);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(mensajeNuevoRecord);
        }
    }
}
