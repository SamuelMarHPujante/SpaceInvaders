using Space_Invaders._02aplication;
using Space_Invaders._02aplication.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Space_Invaders._03data
{
    static class Serializador
    {
        public static void Transcribir(string fichero, object objeto)
        {
            Console.Clear();
            Console.WriteLine("Guardando...");
            try
            {
                string jsonString = JsonSerializer.Serialize<object>(objeto);
                File.WriteAllText(fichero, jsonString);
                Console.WriteLine("Guardado con éxito");
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        public static Jugador CargarJugador(string fichero)
        {
            Console.Clear();
            Console.WriteLine("Cargando...");
            try
            {
                string jsonString = File.ReadAllText(fichero);
                Jugador objeto = JsonSerializer.Deserialize<Jugador>(jsonString);
                Console.WriteLine("Cargado con éxito");
                return objeto;
            }
            catch (Exception error)
            {
                MostrarError(error);
                return null;
            }
        }

        private static void MostrarError(Exception error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            RegistroLogs.RegistrarEnLog($"ERROR: {error.Message}");
            Console.ResetColor();
        }
    }
}
