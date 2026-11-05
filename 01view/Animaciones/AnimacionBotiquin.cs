using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders._01view.Animaciones
{
    class AnimacionBotiquin : Animacion
    {
        public AnimacionBotiquin()
        {
            this.colorPrincipal = ConsoleColor.DarkGreen;
            this.colorSecundario = ConsoleColor.White;
            this.Imagen = "+[+]+";
            this.X = 0;
            this.Y = 0;
        }
    }
}
