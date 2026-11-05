using Space_Invaders._02aplication;
using Space_Invaders._02aplication.Registros;
using System;

namespace Space_Invaders._01view
{
    class Bienvenida
    {
        private ConsoleColor colorBordes;
        private Jugador jugador;
        private bool enProceso;
        private Dictionary<string, string> opciones;

        public Bienvenida()
        {
            colorBordes = ConsoleColor.Yellow;
            enProceso = true;
            opciones = new Dictionary<string, string>()
            {
                ["Enter"] = "Jugar",
                ["Esc"] = "Salir",
                ["Tab"] = "Opciones y pausa",
                ["T"] = "Tienda (SOON)",
            };
            CargarDatosJugador();
        }

        public void Lanzar()
        {
            Console.Clear();
            MostrarBienvenida();

            do
            {
                ConsoleKeyInfo input = new ConsoleKeyInfo();
                Console.CursorVisible = false;

                if (Console.KeyAvailable)
                {
                    while (Console.KeyAvailable)
                    {
                        input = Console.ReadKey(true);
                    }

                    switch (input.Key)
                    {
                        case ConsoleKey.Tab:
                            AbrirOpciones();
                            break;

                        case ConsoleKey.Enter:
                            IniciarPartida();
                            break;

                        case ConsoleKey.Escape:
                            enProceso = false;
                            break;

                        default:
                            MostrarInvalidez();
                            break;
                    }
                }
            }
            while (enProceso);
        }

        private void MostrarBienvenida()
        {
            Console.ForegroundColor = colorBordes;
            Console.SetCursorPosition(0, 3);

            Console.WriteLine(new string('*', 50));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bienvenido a Console Invaders".ToUpper());
            Console.WriteLine();    // Salto de linea

            Console.ForegroundColor = ConsoleColor.White;
            foreach (string key in opciones.Keys)
            {
                Console.WriteLine($"[{key}]\t >> {opciones[key]}");
            }
            Console.WriteLine();    // Salto de linea

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"JUGADOR:\t {jugador.Nombre}");
            Console.WriteLine($"MAYOR RECORD:\t {jugador.Record}");
            Console.WriteLine($"TOTAL PUNTOS:\t {jugador.Puntos}");
            Console.ForegroundColor = colorBordes;
            Console.WriteLine(new string('*', 50));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Globales.VERSION);
            Console.ResetColor();
        }

        private void CargarDatosJugador()
        {
            Jugador objeto = RegistroJugador.CargarJugador();
            bool esNull = objeto == null;
            jugador = esNull ? new Jugador() : objeto;
            
            if (!esNull)
            {
                Globales.recordJugador = jugador.Record;
                Globales.puntosJugador = jugador.Puntos;
            }
        }

        private void GuardarDatosJugador()
        {
            jugador.Record = Globales.recordJugador;
            jugador.Puntos = Globales.puntosJugador;
            RegistroJugador.RegistrarDatos(jugador);
        }

        #region Selecciones

        private void AbrirOpciones()
        {
            Opciones opciones = new Opciones();
            bool mostrarControlesMovimiento = false;
            opciones.Lanzar(mostrarControlesMovimiento);
            Console.Clear();
            MostrarBienvenida();
        }

        private void IniciarPartida()
        {
            Partida partida = new Partida();
            partida.Lanzar();
            GuardarDatosJugador();
            Console.Clear();
            MostrarBienvenida();
        }

        private void MostrarInvalidez()
        {
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Opción inválida".ToUpper());
            MostrarBienvenida();
        }

        #endregion
    }
}