using System;

namespace Space_Invaders._02aplication
{
    class Botiquin : Sprite, IEntidadAleatoria
    {
        private const int FRECUENCIA_BOTIQUIN = Globales.FRECUENCIA_BOTIQUIN;
        private const int ENFRIAMIENTO_MINIMO = Globales.ENFRIAMIENTO_MINIMO;
        private const int ENFRIAMIENTO_MAXIMO = Globales.ENFRIAMIENTO_MAXIMO;

        private int enfriamiento;
        public bool Activo { get; set; }

        public Botiquin(int X, int Y)
        {
            colorSprite = ConsoleColor.Red;
            Imagen = "[+]";
            Activo = false;
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
            bool puedeAparecer = Globales.habilitarBotiquines;
            bool probarPresencia = enfriamiento <= 0;
            if (probarPresencia && puedeAparecer)
            {
                int decisionEstado = rand.Next(0, FRECUENCIA_BOTIQUIN + 1);
                bool activarBotiquin = decisionEstado == 0;
                bool desactivarBotiquin = decisionEstado == FRECUENCIA_BOTIQUIN && this.Activo == true;
                if (activarBotiquin)
                {
                    Activo = true;
                }
                else if (desactivarBotiquin)
                {
                    DesactivarBotiquin();
                }
            }
            ManejarEnfriamiento();
        }

        public void DesactivarBotiquin()
        {
            int randomPosX = rand.Next(0, Globales.ANCHO_PANTALLA - Imagen.Length);
            MoverA(randomPosX, 0);
            Activo = false;
        }

        public void Mover()
        {
            bool enPantalla = this.Y < Globales.LIMITE_PARTIDA;
            bool presentable = Globales.habilitarBotiquines;
            if (Activo && enPantalla && presentable)
            {
                MoverA(this.X, this.Y + 1);
            }
        }

        public override void Dibujar()
        {
            if (Activo && Globales.habilitarBotiquines)
            {
                base.Dibujar();
                Console.ResetColor();
            }
        }
    }
}
