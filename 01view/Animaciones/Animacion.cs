using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space_Invaders._02aplication;

namespace Space_Invaders._01view.Animaciones
{
    class Animacion : Sprite
    {
        protected ConsoleColor colorPrincipal;
        protected ConsoleColor colorSecundario;
        public int TiempoAnimacion { get; set; }

        public Animacion()
        {
            this.TiempoAnimacion = 0;
        }

        public void ActivarEn(int X, int Y, int tiempoAnimado)
        {
            MoverA(X, Y);
            TiempoAnimacion = tiempoAnimado;
        }

        public override void Dibujar()
        {
            bool animacionActiva = TiempoAnimacion > 0;
            bool animacionTerminada = TiempoAnimacion == 0;

            if (animacionActiva)
            {
                bool contadorPar = TiempoAnimacion % 2 == 0;

                this.colorSprite = contadorPar ? colorPrincipal : colorSecundario;
                base.Dibujar();
                TiempoAnimacion--;
            }
            else if (animacionTerminada)
            {
                MoverA(0, 0);
                TiempoAnimacion--;
            }
        }
    }
}
