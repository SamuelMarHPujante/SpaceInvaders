using Space_Invaders._02aplication.Registros;

namespace Space_Invaders._01view
{
    class Opciones
    {
        private bool romperBucle;

        public void Lanzar(bool mostrarControlesMovimiento)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 0);

            Console.Write(new string('=', 21));
            Console.Write("OPCIONES");
            Console.WriteLine(new string('=', 21));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pulse la tecla indicada para alternar su opción asignada.");
            Console.Write("Pulse cualquier otra tecla para volver atrás.");

            MostrarPuntuaciones();

            if (mostrarControlesMovimiento)
                MostrarControles();

            do
            {
                ConsoleKeyInfo tecla = new ConsoleKeyInfo();
                romperBucle = false;

                if (Console.KeyAvailable)
                {
                    while (Console.KeyAvailable)
                        tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.Q:
                            AlternarOpcion(ref Globales.avanceVerticalDeEnemigos, nameof(Globales.avanceVerticalDeEnemigos));
                            break;
                        case ConsoleKey.W:
                            AlternarOpcion(ref Globales.activarDisparosEnemigos, nameof(Globales.activarDisparosEnemigos));
                            break;
                        case ConsoleKey.E:
                            AlternarOpcion(ref Globales.borrarDisparosColisionados, nameof(Globales.borrarDisparosColisionados));
                            break;
                        case ConsoleKey.R:
                            AlternarOpcion(ref Globales.activarAparicionOvni, nameof(Globales.activarAparicionOvni));
                            break;
                        case ConsoleKey.T: // TO-DO
                            continue;
                        case ConsoleKey.Y:
                            AlternarOpcion(ref Globales.ovniReviveALosEnemigos, nameof(Globales.ovniReviveALosEnemigos));
                            break;
                        case ConsoleKey.A:
                            AlternarOpcion(ref Globales.invencibilidadDeLaNave, nameof(Globales.invencibilidadDeLaNave));
                            break;
                        case ConsoleKey.S:
                            AlternarOpcion(ref Globales.fuegoAmigoNave, nameof(Globales.fuegoAmigoNave));
                            break;
                        case ConsoleKey.D:
                            AlternarOpcion(ref Globales.habilitarBotiquines, nameof(Globales.habilitarBotiquines));
                            break;
                        default:
                            romperBucle = true;
                            break;
                    }
                }
                MostrarMenuOpciones();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(new string('=', 50));
                Console.ResetColor();
            }
            while (!romperBucle);
        }


        // ====================OPCIONES====================
        private void AlternarOpcion(ref bool condicion, string nombreCondicion)
        {
            condicion = !condicion;
            RegistroLogs.RegistrarEnLog($"{nombreCondicion} a {condicion.ToString().ToUpper()}");
        }
        private void MostrarMenuOpciones()
        {
            int posX = 40;
            int posY = 4;
            int incremento = 0;

            MostrarOpcion(posY + incremento, "[Q] - Avance de enemigos");
            MostrarOnOff(posX, posY + incremento, Globales.avanceVerticalDeEnemigos);
            incremento++;

            MostrarOpcion(posY + incremento, "[W] - Disparos de enemigos");
            MostrarOnOff(posX, posY + incremento, Globales.activarDisparosEnemigos);
            incremento++;

            MostrarOpcion(posY + incremento, "[E] - Reinicio de balas enemigas");
            MostrarOnOff(posX, posY + incremento, Globales.borrarDisparosColisionados);
            incremento++;

            MostrarOpcion(posY + incremento, "[R] - Apariciones del ovni");
            MostrarOnOff(posX, posY + incremento, Globales.activarAparicionOvni);
            incremento++;

            MostrarOpcion(posY + incremento, "[T] - Disparos por el ovni");
            //MostrarOnOff(posX, posY + incremento, Configuracion.activarDisparosOvni);
            MostrarBloqueado(posX, posY + incremento);
            incremento++;

            MostrarOpcion(posY + incremento, "[Y] - El ovni revive a enemigos");
            MostrarOnOff(posX, posY + incremento, Globales.ovniReviveALosEnemigos);
            incremento++;

            MostrarOpcion(posY + incremento, "[A] - Invencibilidad de la nave");
            MostrarOnOff(posX, posY + incremento, Globales.invencibilidadDeLaNave);
            incremento++;

            MostrarOpcion(posY + incremento, "[S] - Fuego amigo de la nave");
            MostrarOnOff(posX, posY + incremento, Globales.fuegoAmigoNave);
            incremento++;

            MostrarOpcion(posY + incremento, "[D] - Botiquines aleatorios");
            MostrarOnOff(posX, posY + incremento, Globales.habilitarBotiquines);
            incremento++;

            Console.SetCursorPosition(0, posY + incremento);
            Console.ResetColor();
        }
        private void MostrarOpcion(int posY, string opcion)
        {
            Console.SetCursorPosition(0, posY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(opcion);
        }
        private void MostrarOnOff(int posX, int posY, bool condicion)
        {
            string textoON = "ON ";
            string textoOFF = "OFF";

            string estadoActivacion = condicion ? textoON : textoOFF;
            Console.ForegroundColor = condicion ? ConsoleColor.Green : ConsoleColor.DarkRed;

            Console.SetCursorPosition(posX, posY);
            Console.Write(estadoActivacion);

            Console.ResetColor();
        }
        private void MostrarBloqueado(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("NO IMPLEMENTADO");
        }


        // ====================OTROS MENÚS====================
        private void MostrarPuntuaciones()
        {
            int posX = 25;
            int posY = 14;
            int incremento = 0;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(0, posY);
            Console.Write(new string('=', 22));
            Console.Write("PUNTOS");
            Console.WriteLine(new string('=', 22));
            incremento++;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("(o|0|o)");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("+" + Globales.PUNTOS_OVNI);
            incremento++;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("][");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("+" + Globales.PUNTOS_ENEMIGO_VERDE);
            incremento++;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("XX");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("+" + Globales.PUNTOS_ENEMIGO_ROJO);
            incremento++;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write(")(");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("+" + Globales.PUNTOS_ENEMIGO_AZUL);
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("Total enemigos");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Globales.PUNTOS_TOTALES_ENEMIGOS);
            incremento++;

            Console.SetCursorPosition(0, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }
        private void MostrarControles()
        {
            int posX = 25;
            int posY = 21;
            int incremento = 0;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, posY);
            Console.Write(new string('=', 21));
            Console.Write("CONTROLES");
            Console.WriteLine(new string('=', 20));
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Tab]");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Pausar y configurar la partida");
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("[Backspace]");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Salir del juego");
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("[Flecha arr.]/[W]");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Disparar al enemigo");
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("[Flecha izq.]/[A]");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Mover a la izquierda");
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("[Flecha abj.]/[S]");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Usar botiquín guardado");
            incremento++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, posY + incremento);
            Console.Write("[Flecha der.]/[D]");
            Console.SetCursorPosition(posX, posY + incremento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Mover a la derecha");
            incremento++;

            Console.SetCursorPosition(0, posY + incremento);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }
    }
}
