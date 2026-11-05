using System;

namespace Space_Invaders
{
    class Globales
    {
        public static Random random = new Random();
        public static string VERSION = "V-1.12";

        // ==========DATOS DE JUGADOR==========
        //public static string nombreDelJugador = "no-name";
        public static int recordJugador = 0;
        public static int puntosJugador = 0;


        // ==========FUNCIONALIDADES DESACTIVABLES==========
        public static bool activarDisparosEnemigos = true;
        public static bool activarDisparosOvni = false;             // TODO
        public static bool activarAparicionOvni = true;
        public static bool borrarDisparosColisionados = false;
        public static bool fuegoAmigoNave = false;
        public static bool habilitarBotiquines = true;
        public static bool invencibilidadDeLaNave = false;
        public static bool avanceVerticalDeEnemigos = true;
        public static bool ovniReviveALosEnemigos = false;          // FIX
        

        // ==========DIMENSIONES DE LA PANTALLA==========
        public const int ANCHO_PANTALLA = 100;                      // Ancho pedido por defecto: 80
        public const int ALTO_PANTALLA = 30;                        // Alto pedido por defecto: 25



        // ==========POSICIONES DEL SUELO Y HUD==========
        public const int ALTURA_HUD = 5;
        public const int SUELO_PARTIDA = ALTO_PANTALLA - ALTURA_HUD;
        public const int INICIO_HUD = SUELO_PARTIDA + 1;                // Donde se inicia el HUD
        public const int LIMITE_PARTIDA = SUELO_PARTIDA - 1;            // Donde se mueve la nave


        // ==========COLORES DE OBJETOS==========
        public const ConsoleColor COLOR_TORRES = ConsoleColor.Cyan;
        public const ConsoleColor COLOR_BALAS_NAVE = ConsoleColor.Yellow;
        public const ConsoleColor COLOR_NAVE = ConsoleColor.White;


        // ==========AJUSTES DE OBJETOS==========
        public const int ANCHO_TORRE = 6;
        public const int CANTIDAD_TORRES = 4;
        public const int CURACION_BOTIQUIN = 1;
        public const int CANTIDAD_ENEMIGOS = 10;
        public const int SEPARACION_ENEMIGOS = 3;


        // ==========ATRIBUTOS PARA LA PARTIDA==========
        public const int ENFRIAMIENTO_MINIMO = 5;
        public const int ENFRIAMIENTO_MAXIMO = 20;
        public const int ENFRIAMIENTO_NAVE = 30;
        public const int PAUSA_BUCLE = 45;
        public const int FRECUENCIA_OVNI = 10;
        public const int FRECUENCIA_BOTIQUIN = 10;
        public const int PROBABILIDAD_REVIVIR = 15;
        public const int FRAMES_ANIMACION = 20;
        public const int DANYO_BALAS = 1;


        // ==========DISPAROS EN PANTALLA==========
        public const int BALAS_NAVE = 6;
        public const int BALAS_ENEMIGO = 6;


        // ==========VIDAS Y PUNTOS==========
        public const int VIDAS_INICIALES = 3;
        public const int PUNTOS_ENEMIGO_VERDE = 30;
        public const int PUNTOS_ENEMIGO_ROJO = 20;
        public const int PUNTOS_ENEMIGO_AZUL = 10;
        public const int PUNTOS_OVNI = 100;
        public const int PUNTOS_TOTALES_ENEMIGOS = (PUNTOS_ENEMIGO_VERDE * CANTIDAD_ENEMIGOS) + (PUNTOS_ENEMIGO_ROJO * CANTIDAD_ENEMIGOS) + (PUNTOS_ENEMIGO_AZUL * CANTIDAD_ENEMIGOS);
    }
}
