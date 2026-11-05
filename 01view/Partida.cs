using System;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Space_Invaders._02aplication;
using Space_Invaders._02aplication.Enemigos;
using Space_Invaders._02aplication.Registros;
using Space_Invaders._01view.Animaciones;

namespace Space_Invaders._01view
{
    class Partida
    {
        private const int PUNTOS_ENEMIGO_VERDE = Globales.PUNTOS_ENEMIGO_VERDE;
        private const int PUNTOS_ENEMIGO_ROJO = Globales.PUNTOS_ENEMIGO_ROJO;
        private const int PUNTOS_ENEMIGO_AZUL = Globales.PUNTOS_ENEMIGO_AZUL;
        private const int CURACION_BOTIQUIN = Globales.CURACION_BOTIQUIN;
        private const int FRAMES_ANIMACION = Globales.FRAMES_ANIMACION;
        private const int CANTIDAD_TORRES = Globales.CANTIDAD_TORRES;
        private const int VIDAS_INICIALES = Globales.VIDAS_INICIALES;
        private const int ANCHO_PANTALLA = Globales.ANCHO_PANTALLA;
        private const int LIMITE_PARTIDA = Globales.LIMITE_PARTIDA;
        private const int SUELO_PARTIDA = Globales.SUELO_PARTIDA;
        private const int ANCHO_TORRE = Globales.ANCHO_TORRE;
        private const int PUNTOS_OVNI = Globales.PUNTOS_OVNI;
        private const int DANYO_BALAS = Globales.DANYO_BALAS;

        private int botiquinesNave;
        private int puntosNave;
        private int vidasNave;
        private int navePosX;
        private int navePosY;
        private int ovniPosX;
        private int ovniPosY;

        private bool hayEnemigosVivos;
        private bool llegadaATierra;
        private bool finDePartida;
        private bool vidaCurada;

        private TorreDefensiva[] torres;
        private BloqueDeAliens aliens;
        private Botiquin botiquin;
        private Nave nave;
        private Ovni ovni;

        private AnimacionBotiquin animacionBotiquin;
        private AnimacionPuntuacion animacionPuntos;
        private AnimacionPuntosOvni animacionOvni;
        private AnimacionVida animacionVida;
        private HUD HUD;

        public Partida()
        {
            nave = new Nave(ANCHO_PANTALLA / 2, LIMITE_PARTIDA);
            torres = new TorreDefensiva[CANTIDAD_TORRES];
            ovni = new Ovni(ANCHO_PANTALLA / 2, 0, Globales.PUNTOS_OVNI);
            botiquin = new Botiquin(0, 0);
            aliens = new BloqueDeAliens();

            animacionBotiquin = new AnimacionBotiquin();
            animacionPuntos = new AnimacionPuntuacion();
            animacionOvni = new AnimacionPuntosOvni();
            animacionVida = new AnimacionVida();
            HUD = new HUD();
        }
                        
        public void Lanzar()
        {
            int alturaTorres = SUELO_PARTIDA - (SUELO_PARTIDA / 3);
            int segmentosTorres = ANCHO_PANTALLA / CANTIDAD_TORRES;
            int mitadSegmentos = segmentosTorres / 2;
            int comienzoTorre = ANCHO_TORRE / 2;
            ConsoleKeyInfo input = new ConsoleKeyInfo();

            ActualizarDatosDeObjetos();
            RegistroLogs.RegistrarEnLog("Partida iniciada");
            Console.Clear();

            for (int i = 0; i < torres.Length; i++)
            {
                torres[i] = new TorreDefensiva(mitadSegmentos - comienzoTorre, alturaTorres);
                mitadSegmentos += segmentosTorres;
            }

            do
            {
                DibujarSprites();

                #region Inputs

                Thread.Sleep(Globales.PAUSA_BUCLE);
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
                        
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            nave.MoverDerecha();
                            break;
                        
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            nave.MoverIzquierda();
                            break;
                        
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            nave.Disparar();
                            break;
                        
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            nave.CurarNave(animacionVida);
                            break;
                    }
                }

                #endregion

                MoverSprites();
                ComprobarColisiones();
                ActualizarHUD();
                ActualizarDatosDeObjetos();
            } 
            while (input.Key != ConsoleKey.Escape && !finDePartida);

            if (finDePartida)
            {
                MostrarFinDePartida();
            }
            else
            {
                RegistroLogs.RegistrarEnLog("Partida detenida");
            }
        }


        // ====================MÉTODOS DE LA PARTIDA====================
        private void ActualizarDatosDeObjetos()
        {
            vidasNave = nave.Vidas;
            llegadaATierra = aliens.ComprobarLlegadaATierra();
            hayEnemigosVivos = aliens.HayEnemigosVivos();
            finDePartida = vidasNave == 0 || llegadaATierra || !hayEnemigosVivos;

            botiquinesNave = nave.Botiquines;
            puntosNave = nave.Puntos;
            navePosX = nave.X;
            navePosY = nave.Y;
            ovniPosX = ovni.X;
            ovniPosY = ovni.Y;
        }

        private void DibujarSprites()
        {
            for (int i = 0; i < torres.Length; i++)
            {
                torres[i].Dibujar();
            }

            nave.Dibujar();
            aliens.Dibujar();
            ovni.ComprobarPresencia();
            ovni.Dibujar();
            botiquin.ComprobarPresencia();
            botiquin.Dibujar();

            aliens.IntentarDisparo();

            animacionBotiquin.Dibujar();
            animacionVida.Dibujar();
            animacionOvni.Dibujar();
            animacionPuntos.Dibujar();
        }

        private void AbrirOpciones()
        {
            Opciones opciones = new Opciones();
            bool mostrarControlesMovimiento = true;
            opciones.Lanzar(mostrarControlesMovimiento);
            Console.Clear();
        }

        private void MoverSprites()
        {
            aliens.Mover();
            nave.MoverDisparos();
            aliens.MoverDisparo();
            ovni.Mover();
            botiquin.Mover();
        }

        private void ComprobarEnemigosDisparados()
        {
            int enemigoDisparado = nave.DisparaAEnemigos(aliens);
            int puntosAObtener;

            switch (enemigoDisparado)
            {
                case 1:
                    puntosAObtener = PUNTOS_ENEMIGO_VERDE;
                    break;

                case 2:
                    puntosAObtener = PUNTOS_ENEMIGO_ROJO;
                    break;

                case 3:
                    puntosAObtener = PUNTOS_ENEMIGO_AZUL;
                    break;

                default:
                    puntosAObtener = 0;
                    break;
            }

            if (puntosAObtener > 0)
            {
                AgregarPuntos(puntosAObtener);
            }
        }

        private void ComprobarColisiones()
        {
            ComprobarEnemigosDisparados();

            if (aliens.DisparaALaNave(nave) && !Globales.invencibilidadDeLaNave)       // COLISIONES CON NAVE
            {
                nave.DanyarNave(DANYO_BALAS, animacionVida);
            }

            if (nave.ColisionaConBotiquin(botiquin))                             // COLISIONES CON BOTIQUINES
            {
                bool faltanVidas = vidasNave < VIDAS_INICIALES;

                if (faltanVidas)
                {
                    nave.Vidas = vidasNave + CURACION_BOTIQUIN;
                    vidaCurada = true;
                    animacionVida.ActivarEn(navePosX, navePosY - 1, FRAMES_ANIMACION, vidaCurada);
                }
                else
                {
                    nave.Botiquines = botiquinesNave + 1;
                    animacionBotiquin.ActivarEn(navePosX, navePosY - 1, FRAMES_ANIMACION);
                }
            }

            if (!Globales.fuegoAmigoNave)                      // COLISIONES DESACTIVABLES
            {
                nave.DisparaATorres(torres);
                nave.DisparaABotiquin(botiquin);
            }

            if (nave.DisparaAOvni(ovni))                        // COLISIONES CON OVNI
            {
                animacionOvni.ActivarEn(ovniPosX, ovniPosY + 1, FRAMES_ANIMACION);
                AgregarPuntos(PUNTOS_OVNI);
            }

            aliens.DisparaATorres(torres);
            aliens.ColisionaConBotiquin(botiquin);

            if (aliens.DisparaABotiquin(botiquin))
            {
                botiquin.Activo = false;
            }

            if (Globales.activarAparicionOvni)
            {
                ovni.ColisionaConBotiquin(botiquin);
            }
        }


        // ====================HUD Y ANIMACIONES====================
        private void ActualizarHUD()
        {
            //int centroX = ANCHO_PANTALLA / 2;

            Console.SetCursorPosition(0, SUELO_PARTIDA);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(new string('=', ANCHO_PANTALLA));

            HUD.ActualizarContadorVidas(nave, finDePartida);
            HUD.ActualizarMarcadorBotiquines(nave, finDePartida);
            HUD.ActualizarMarcadorPuntos(nave);
            HUD.ActualizarContadorDisparos(nave);
            Console.ResetColor();
        }
        private void AgregarPuntos(int puntosObtenidos)
        {
            nave.Puntos = puntosNave + puntosObtenidos;
            animacionPuntos.ActivarEn(navePosX, navePosY - 1, FRAMES_ANIMACION, puntosObtenidos);
        }
        private void MostrarFinDePartida()
        {
            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            bool partidaGanada = !hayEnemigosVivos;

            HUD.DibujarFinDePartida(nave, finDePartida, partidaGanada, puntosNave);
            do
            {
                if (Console.KeyAvailable)
                {
                    while (Console.KeyAvailable)
                    {
                        tecla = Console.ReadKey(true);
                    }

                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        Partida partida = new Partida();
                        partida.Lanzar();
                    }
                    else if (tecla.Key == ConsoleKey.Tab)
                    {
                        AbrirOpciones();
                        HUD.DibujarFinDePartida(nave, finDePartida, partidaGanada, puntosNave);
                    }
                }
            }
            while (tecla.Key != ConsoleKey.Backspace);
            RegistroLogs.RegistrarEnLog(partidaGanada ? "Partida ganada" : "Partida perdida");
        }
    }
}