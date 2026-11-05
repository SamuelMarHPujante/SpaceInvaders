using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders._01view.Animaciones
{
    class AnimacionVida : Animacion
    {
        private string vidaUp;
        private string vidaDown;

        public AnimacionVida()
        {
            colorSecundario = ConsoleColor.White;
            this.Imagen = "<3";
            vidaUp = "1-UP";
            vidaDown = "1-DN";
        }

        public void ActivarEn(int X, int Y, int tiempoAnimado, bool vidaCurada)
        {
            this.colorPrincipal = vidaCurada ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
            this.Imagen = vidaCurada ? vidaUp : vidaDown;
            base.ActivarEn(X, Y, tiempoAnimado);
        }
    }
}
