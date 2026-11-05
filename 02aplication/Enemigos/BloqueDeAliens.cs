using System;
using static System.Net.Mime.MediaTypeNames;
using Space_Invaders._02aplication.Registros;

namespace Space_Invaders._02aplication.Enemigos
{
    class BloqueDeAliens
    {
        private const int PROBABILIDAD_REVIVIR = Globales.PROBABILIDAD_REVIVIR;
        private const int SEPARACION_ENEMIGOS = Globales.SEPARACION_ENEMIGOS;
        private const int CANTIDAD_ENEMIGOS = Globales.CANTIDAD_ENEMIGOS;
        private const int BALAS_ENEMIGO = Globales.BALAS_ENEMIGO;
        private const int ANCHO_PANTALLA = Globales.ANCHO_PANTALLA;
        private const int LIMITE_PARTIDA = Globales.LIMITE_PARTIDA;
        private Random rand = Globales.random;

        private ConsoleColor colorBalas;
        private Enemigo[,] aliens;
        private Disparo[] balas;            // a lista??
        private int direccion;
        private int posicionInicialY;

        public BloqueDeAliens()
        {
            aliens = new Enemigo[3, CANTIDAD_ENEMIGOS];
            balas = new Disparo[BALAS_ENEMIGO];
            colorBalas = ConsoleColor.DarkRed;
            posicionInicialY = 2;
            direccion = 1;

            for (int i = 0; i < balas.Length; i++)
            {
                balas[i] = new Disparo(0, 0, colorBalas);
            }

            for (int i = 0; i < aliens.GetLength(0); i++)
            {
                for (int j = 0; j < aliens.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        aliens[i, j] = new AlienVerde(j * SEPARACION_ENEMIGOS, posicionInicialY, Globales.PUNTOS_ENEMIGO_VERDE);
                    }
                    else if (i == 1)
                    {
                        aliens[i, j] = new AlienRojo(j * SEPARACION_ENEMIGOS, posicionInicialY + 1, Globales.PUNTOS_ENEMIGO_ROJO);
                    }
                    else
                    {
                        aliens[i, j] = new AlienAzul(j * SEPARACION_ENEMIGOS, posicionInicialY + 2, Globales.PUNTOS_ENEMIGO_AZUL);
                    }
                }
            }
        }

        #region Movimiento

        private Enemigo GetEnemigoIzquierdo()
        {
            Enemigo elegido = null;
            int minX = ANCHO_PANTALLA;
            foreach (Enemigo e in aliens)
            {
                bool enPantalla = e.X < minX;
                if (enPantalla && e.Activo)
                {
                    minX = e.X;
                    elegido = e;
                }
            }

            return elegido;
        }
        private Enemigo GetEnemigoDerecho()
        {
            Enemigo elegido = null;
            int maxX = -1;
            foreach (Enemigo e in aliens)
            {
                bool enPantalla = e.X > maxX;
                if (enPantalla && e.Activo)
                {
                    maxX = e.X;
                    elegido = e;
                }
            }

            return elegido;
        }
        
        public void Mover()
        {
            bool direccionPositiva = direccion > 0;
            Enemigo referencia = direccionPositiva ? GetEnemigoDerecho() : GetEnemigoIzquierdo();
            bool enPantalla = referencia.X < ANCHO_PANTALLA - referencia.Imagen.Length && direccionPositiva ||
                              referencia.X > 0 && !direccionPositiva;

            if (enPantalla)
            {
                DesplazarMatrizEnemigos();
            }
            else
            {
                AlternarDireccionYBajar();
            }
        }

        private void DesplazarMatrizEnemigos()
        {
            foreach (Enemigo e in aliens)
            {
                if (e.Activo)
                {
                    e.MoverA(e.X + direccion, e.Y);
                }
            }
        }

        private void AlternarDireccionYBajar()
        {
            direccion = -direccion;
            if (Globales.avanceVerticalDeEnemigos)
            {
                foreach (Enemigo e in aliens)
                {
                    e.MoverA(e.X, e.Y + 1);
                }
            }
        }

        #endregion

        #region Colisiones

        public int ColisionaCon(Disparo d) // MEJORAR y foreach
        {
            int disparoPosX = d.X;
            int disparoPosY = d.Y;
            for (int i = 0; i < aliens.GetLength(0); i++)
            {
                for (int j = 0; j < aliens.GetLength(1); j++)
                {
                    if (aliens[i, j].Activo)
                    {
                        bool colisionaX = disparoPosX >= aliens[i, j].X && disparoPosX < aliens[i, j].X + 2;
                        bool colisionaY = disparoPosY == aliens[i, j].Y;

                        if (colisionaX && colisionaY)
                        {
                            aliens[i, j].Activo = false;
                            RegistroLogs.RegistrarEnLog($"Alien [{i}, {j}] eliminado");
                            return i + 1;
                        }
                    }
                }
            }

            return 0;
        }

        public bool DisparaALaNave(Nave nave)   // MARGEN DE DAÑO ++
        {
            int j = 0;
            bool colision = false;
            while (j < balas.Length && !colision)
            {
                if (balas[j].Activo && nave.ColisionaCon(balas[j]))
                {
                    balas[j].Activo = false;
                    balas[j].MoverA(0, 0);
                    RegistroLogs.RegistrarEnLog("Jugador disparado");
                    colision = true;
                }
                j++;
            }

            if (colision && Globales.borrarDisparosColisionados)
            {
                BorrarDisparos();
            }

            return colision;
        }

        public void ColisionaConBotiquin(Botiquin botiquin)
        {
            int botiquinPosX = botiquin.X;
            int botiquinPosY = botiquin.Y;
            string spriteBotiquin = botiquin.Imagen;

            int filas = aliens.GetLength(0);
            int columnas = aliens.GetLength(1);

            for (int i = 0; i < filas; i++)             // a foreach
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (aliens[i, j].Activo && botiquin.Activo)
                    {
                        bool colisionaX = (botiquinPosX + (spriteBotiquin.Length - 1)) >= aliens[i, j].X && botiquinPosX < aliens[i, j].X + 2;
                        bool colisionaY = botiquinPosY == aliens[i, j].Y;
                        if (colisionaX && colisionaY)
                        {
                            int randomPosX = rand.Next(0, ANCHO_PANTALLA - spriteBotiquin.Length);
                            botiquin.MoverA(randomPosX, 0);
                            botiquin.Activo = false;
                        }
                    }
                }
            }
        }

        public bool DisparaABotiquin(Botiquin botiquin)
        {
            int j = 0;
            bool colision = false;
            while (j < balas.Length && !colision)
            {
                if (balas[j].Activo && botiquin.ColisionaCon(balas[j]))
                {
                    int randomPosX = rand.Next(0, ANCHO_PANTALLA - botiquin.Imagen.Length);
                    balas[j].Activo = false;
                    balas[j].MoverA(0, 0);
                    botiquin.MoverA(randomPosX, 0);
                    colision = true;
                }
                j++;
            }

            return colision;
        }

        public void DisparaATorres(TorreDefensiva[] torres)
        {
            foreach (TorreDefensiva t in torres)
            {
                foreach (Disparo d in balas)
                {
                    if (d.Activo && t.ColisionaCon(d))
                    {
                        d.Activo = false;
                        d.MoverA(0, 0);
                    }
                }
            }
        }

        public void ColisionaConTorres(TorreDefensiva[] torres)    // TODO
        {
            foreach (TorreDefensiva t in torres)
            {
                foreach (Enemigo e in aliens)
                {
                    if (e.Activo && t.ColisionaCon(e))
                    {
                        e.Activo = false;
                        e.MoverA(0, 0);
                    }
                }
            }
        }

        #endregion

        #region Disparos

        public void IntentarDisparo()       // MEJORAR - Mucho nesting
        {
            int j;
            int dispararBalas = rand.Next(0, BALAS_ENEMIGO + 1);
            bool disparosActivos = Globales.activarDisparosEnemigos;

            for (int k = 0; k < balas.Length; k++)
            {
                if (disparosActivos && dispararBalas > 0 && !balas[k].Activo)
                {
                    for (int i = 0; i < aliens.GetLength(0); i++)
                    {
                        j = 0;

                        while (j < aliens.GetLength(1) && !balas[k].Activo)
                        {
                            if (aliens[i, j].Activo)
                            {
                                int enemigoAleatorio = rand.Next(0, 10);
                                if (enemigoAleatorio >= 9)
                                {
                                    balas[k].MoverA(aliens[i, j].X, aliens[i, j].Y + 1);
                                    balas[k].Activo = true;
                                    dispararBalas--;
                                }
                            }
                            j++;
                        }
                    }
                }
            }
        }

        public void MoverDisparo()
        {
            for (int i = 0; i < balas.Length; i++)
            {
                if (balas[i].Activo)
                {
                    bool enPantalla = balas[i].Y < LIMITE_PARTIDA;

                    if (enPantalla)
                    {
                        balas[i].MoverA(balas[i].X, balas[i].Y + 1);
                    }
                    else
                    {
                        balas[i].Activo = false;
                        balas[i].MoverA(0, 0);
                    }
                }
            }
        }

        private void BorrarDisparos()
        {
            foreach (Disparo d in balas)
            {
                d.Activo = false;
                d.MoverA(0, 0);
            }
        }

        #endregion

        #region Otros métodos

        public void Dibujar()
        {
            foreach (Enemigo e in aliens)
            {
                e.Dibujar();
                e.InvertirCambio();
            }

            foreach (Disparo d in balas)
            {
                Console.ForegroundColor = colorBalas;            // atributo color balas
                d.Dibujar();
            }
        }

        public bool HayEnemigosVivos()
        {
            foreach (Enemigo e in aliens)
            {
                if (e.Activo)
                {
                    return true;
                }
            }

            return false;
        }

        public void IntentarRevivir() // TO-FIX
        {
            foreach (Enemigo e in aliens)
            {
                if (!e.Activo && Globales.ovniReviveALosEnemigos)
                {
                    int revividoAleatorio = rand.Next(0, PROBABILIDAD_REVIVIR);
                    bool activacionValida = revividoAleatorio == 0 || revividoAleatorio == PROBABILIDAD_REVIVIR - 1;
                    e.Activo = activacionValida;
                }
            }
        }

        public bool ComprobarLlegadaATierra()
        {
            bool naveAlcanzada = false;
            foreach (Enemigo e in aliens)
            {
                if (e.Activo && !naveAlcanzada)
                {
                    bool enSuelo = e.Y == LIMITE_PARTIDA;
                    naveAlcanzada = enSuelo;
                }
            }

            return naveAlcanzada;
        }

        #endregion
    }
}
