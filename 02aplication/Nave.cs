using System.Text;
using Space_Invaders._02aplication.Enemigos;
using Space_Invaders._02aplication.Registros;
using Space_Invaders._01view.Animaciones;

namespace Space_Invaders._02aplication
{
    class Nave : Sprite
    {
        private const int CURACION_BOTIQUIN = Globales.CURACION_BOTIQUIN;
        private const int ENFRIAMIENTO_NAVE = Globales.ENFRIAMIENTO_NAVE;
        private const int FRAMES_ANIMACION = Globales.FRAMES_ANIMACION;
        private const int VIDAS_INICIALES = Globales.VIDAS_INICIALES;
        private const int ANCHO_PANTALLA = Globales.ANCHO_PANTALLA;
        private const int LIMITE_PARTIDA = Globales.LIMITE_PARTIDA;
        private Random rand = Globales.random;
        private string spriteBase = "/\\";
        private byte velocidad = 1;

        public int Vidas { get; set; }
        public int Puntos { get; set; }
        public int Botiquines { get; set; }
        public int MargenDeDanyo { get; set; }

        private int balas;
        private Disparo[] municion;
        private ConsoleColor colorBalas = Globales.COLOR_BALAS_NAVE;

        public Nave(int X, int Y)
        {
            Vidas = Globales.VIDAS_INICIALES;
            Puntos = 0;
            Botiquines = 0;

            balas = Globales.BALAS_NAVE;
            municion = new Disparo[balas];
            MargenDeDanyo = 0;

            for (int i = 0; i < municion.Length; i++)
                municion[i] = new Disparo(X, Y, colorBalas);
            
            this.Imagen = spriteBase;
            this.colorSprite = Globales.COLOR_NAVE;
            this.X = X;
            this.Y = Y;
        }

        #region Disparos

        public int ContrarDisparos()        // Para su contador del HUD, no influye en "disparos"
        {
            int contadorBalas = balas;
            foreach (Disparo bala in municion)
            {
                if (bala.Activo)
                    contadorBalas--;
            }
            return contadorBalas;
        }

        public void Disparar()
        {
            bool balaDeTurno = false;
            foreach (Disparo bala in municion)
            {
                if (!bala.Activo && !balaDeTurno)
                {
                    bala.MoverA(X, Y);
                    bala.Activo = true;
                    balaDeTurno = bala.Activo;
                }
            }
        }

        public void MoverDisparos()
        {
            foreach (Disparo bala in municion)
            {
                if (bala.Activo)
                {
                    bool enPantalla = bala.Y > 0;
                    if (enPantalla)
                        bala.MoverA(bala.X, bala.Y - 1);
                    else
                    {
                        bala.Activo = false;
                        bala.MoverA(X, Y);
                    }
                }
            }
        }

        #endregion

        #region Movimiento

        public void MoverDerecha()
        {
            bool enPantalla = this.X < ANCHO_PANTALLA - this.Imagen.Length;
            if (enPantalla)
                MoverA(this.X + velocidad, Y);
        }

        public void MoverIzquierda()
        {
            bool enPantalla = this.X > 0;
            if (enPantalla)
                MoverA(this.X - velocidad, Y);
        }

        #endregion

        #region Colisiones

        public void DisparaABalaEnemiga(Enemigo enemigos) // TODO
        {
            foreach (Disparo d in municion)
            {
                enemigos.ColisionaCon(d);
            }
        }

        public int DisparaAEnemigos(BloqueDeAliens enemigos) // ********
        {
            int j = 0;
            int tipoDeEnemigo = 0;
            bool colision = false;
            while (j < municion.Length && !colision)
            {
                tipoDeEnemigo = enemigos.ColisionaCon(municion[j]);
                bool esValido = tipoDeEnemigo is 1 || tipoDeEnemigo is 2 || tipoDeEnemigo is 3;
                if (municion[j].Activo && esValido)
                {
                    if (esValido)
                    {
                        municion[j].Activo = false;
                        municion[j].MoverA(0, 0);
                        colision = true;
                    }
                }
                j++;
            }
            return tipoDeEnemigo;
        }

        public bool DisparaAOvni(Ovni ovni)
        {
            int j = 0;
            bool hayColision = false;
            while (j < municion.Length && !hayColision)
            {
                if (municion[j].Activo && ovni.ColisionaCon(municion[j]))
                {
                    municion[j].Activo = false;
                    municion[j].MoverA(0, 0);

                    if (ovni.Activo)
                    {
                        ovni.Activo = false;
                        hayColision = true;
                        RegistroLogs.RegistrarEnLog("Ovni disparado");
                    }
                }
                j++;
            }
            return hayColision;
        }

        public bool ColisionaConBotiquin(Botiquin botiquin)
        {
            int botiquinPosX = botiquin.X;
            int botiquinPosY = botiquin.Y;
            string spritebotiquin = botiquin.Imagen;
            bool hayColision = false;

            if (botiquin.Activo && !hayColision)
            {
                bool colisionaX = (botiquinPosX + (spritebotiquin.Length - 1)) >= this.X && botiquinPosX < this.X + 2;
                bool colisionaY = botiquinPosY == this.Y;
                int randomPosX = rand.Next(0, ANCHO_PANTALLA - spritebotiquin.Length);
                if (colisionaX && colisionaY)
                {
                    botiquin.DesactivarBotiquin();
                    hayColision = true;
                    RegistroLogs.RegistrarEnLog("Botiquín obtenido");
                }
            }

            return hayColision;
        }

        public void DisparaABotiquin(Botiquin botiquin)
        {
            if (botiquin.Activo)
            {
                for (int i = 0; i < municion.Length; i++)           // foreach
                {
                    bool hayColision = municion[i].Activo && botiquin.ColisionaCon(municion[i]);
                    if (hayColision)
                    {
                        municion[i].Activo = false;
                        municion[i].MoverA(0, 0);
                        botiquin.DesactivarBotiquin();
                    }
                }
            }
        }

        public void DisparaATorres(TorreDefensiva[] torres)
        {
            foreach (TorreDefensiva t in torres)
            {
                foreach (Disparo d in municion)
                {
                    if (d.Activo && t.ColisionaCon(d))
                    {
                        d.Activo = false;
                        d.MoverA(0, 0);
                    }
                }
            }
        }

        #endregion

        #region Otros métodos

        public void CurarNave(AnimacionVida animacionVida)
        {
            bool faltanVidas = Vidas < VIDAS_INICIALES;
            bool tieneBotiquin = Botiquines > 0;

            if (faltanVidas && tieneBotiquin)
            {
                Botiquines--;
                Vidas = Vidas + CURACION_BOTIQUIN;
                AnimarVida(animacionVida, vidaCurada: true);
            }
        }

        public void DanyarNave(int danyoARecibir, AnimacionVida animacionVida)
        {
            bool vulnerable = MargenDeDanyo <= 0;
            if (vulnerable)
            {
                MoverA(
                    nuevaX: ANCHO_PANTALLA / 2, 
                    nuevaY: LIMITE_PARTIDA);

                MargenDeDanyo = ENFRIAMIENTO_NAVE;
                Vidas = Vidas - danyoARecibir;
                AnimarVida(animacionVida, vidaCurada: false);
            }
        }

        private void AnimarVida(AnimacionVida animacionVida, bool vidaCurada)
        {
            animacionVida.ActivarEn(
                X: X,
                Y: Y - 1,
                tiempoAnimado: FRAMES_ANIMACION,
                vidaCurada: vidaCurada);
        }

        public override void Dibujar()
        {
            bool margenPar = MargenDeDanyo % 2 == 0 || MargenDeDanyo <= 0;
            bool margenTerminado = MargenDeDanyo == 0;
            bool margenActivo = MargenDeDanyo > 0;
            Console.ForegroundColor = this.colorSprite;
            this.Imagen = margenPar ? spriteBase : "  ";
            base.Dibujar();

            if (margenTerminado)
            {
                this.Imagen = spriteBase;
                MargenDeDanyo--;
            }
            else if (margenActivo)
            {
                MargenDeDanyo--;
            }

            foreach (Disparo bala in municion)
                bala.Dibujar();
        }

        #endregion
    }
}