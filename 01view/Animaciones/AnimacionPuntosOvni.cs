using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders._01view.Animaciones
{
    class AnimacionPuntosOvni : AnimacionPuntuacion
    {
        public AnimacionPuntosOvni()
        {
            this.colorPrincipal = ConsoleColor.Cyan;
            this.colorSecundario = ConsoleColor.Yellow;
            this.Imagen = $"+{Globales.PUNTOS_OVNI}";
            this.X = 0;
            this.Y = 0;
            direccion = 1;
            tiempoSubida = Globales.FRAMES_ANIMACION / 2;
        }
    }
}
