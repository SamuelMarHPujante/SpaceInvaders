using System.Timers;

namespace Space_Invaders._02aplication.Enemigos
{
    class Ovni : Enemigo, IEntidadAleatoria
    {
        private const int FRECUENCIA_OVNI = Globales.FRECUENCIA_OVNI;
        private const int ANCHO_PANTALLA = Globales.ANCHO_PANTALLA;
        private const int ENFRIAMIENTO_MINIMO = Globales.ENFRIAMIENTO_MINIMO;
        private const int ENFRIAMIENTO_MAXIMO = Globales.ENFRIAMIENTO_MAXIMO;

        private int direccion;
        private int enfriamiento;

        public Ovni(int X, int Y, int obtencion) : base(X, Y, obtencion)
        {
            colorSprite = ConsoleColor.DarkYellow;
            spritePrincipal = "(o|0|o)";
            spriteSecundario = "(0|o|0)";
            Imagen = spritePrincipal;
            this.Activo = false;
            direccion = 1;
            ManejarEnfriamiento();
        }

        public void ManejarEnfriamiento()
        {
            int min = ENFRIAMIENTO_MINIMO;
            int max = ENFRIAMIENTO_MAXIMO;
            enfriamiento = enfriamiento > 0 ? enfriamiento - 1 : Globales.random.Next(min, max);
        }

        public void ComprobarPresencia()
        {
            bool puedeAparecer = Globales.activarAparicionOvni;
            bool probarPresencia = enfriamiento <= 0;
            if (probarPresencia && puedeAparecer)
            {
                int decisionEstado = rand.Next(0, FRECUENCIA_OVNI + 1);
                bool activarOvni = decisionEstado == 0;
                bool desactivarOvni = decisionEstado == FRECUENCIA_OVNI && this.Activo == true;
                if (activarOvni)
                {
                    Activo = true;
                }
                else if (desactivarOvni)
                {
                    Activo = false;
                    // enemigos.IntentarRevivir();
                }
            }
            ManejarEnfriamiento();
        }

        public void Mover()
        {
            bool direccionPositiva = direccion > 0;
            bool enPantalla = this.X < ANCHO_PANTALLA - Imagen.Length && direccionPositiva || this.X > 0 && !direccionPositiva;
            if (Globales.activarAparicionOvni)
            {
                if (enPantalla)
                {
                    MoverA(X + direccion, Y);
                }
                else
                {
                    direccion = -direccion;
                }
            }
        }

        public void ColisionaConBotiquin(Botiquin botiquin)
        {
            int botiquinPosX = botiquin.X;
            int botiquinPosY = botiquin.Y;
            string spriteBotiquin = botiquin.Imagen;
            bool botiquinPresente = botiquin.Activo;

            if (Activo && botiquinPresente)
            {
                bool colisionaX = botiquinPosX + (spriteBotiquin.Length - 1) >= this.X && botiquinPosX < this.X + (Imagen.Length - 1);
                bool colisionaY = botiquinPosY == this.Y;
                int randomPosX = rand.Next(0, ANCHO_PANTALLA - spriteBotiquin.Length);

                if (colisionaX && colisionaY)
                {
                    botiquin.MoverA(randomPosX, 0);
                    botiquinPresente = false;
                }
            }
        }

        public override void Dibujar()
        {
            if (Globales.activarAparicionOvni)
            {
                base.Dibujar();
                Console.ResetColor();
                InvertirCambio();
            }
        }
    }
}
