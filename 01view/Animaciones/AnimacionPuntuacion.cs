using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders._01view.Animaciones
{
    class AnimacionPuntuacion : Animacion
    {
        private int puntosAcumulados;
        protected int direccion;
        protected int tiempoSubida;

        public AnimacionPuntuacion()
        {
            this.colorPrincipal = ConsoleColor.DarkGreen;
            this.colorSecundario = ConsoleColor.White;
            this.Imagen = "+0";
            this.X = 0;
            this.Y = 0;
            tiempoSubida = Globales.FRAMES_ANIMACION / 2;
            direccion = -1;
        }

        public void ActivarEn(int X, int Y, int tiempoAnimado, int puntosObtenidos)
        {
            puntosAcumulados += puntosObtenidos;
            this.Imagen = $"+{puntosAcumulados}";
            base.ActivarEn(X, Y, tiempoAnimado);
        }

        public override void Dibujar()
        {
            bool aLaMitad = this.TiempoAnimacion >= tiempoSubida + (tiempoSubida / 1.2f);
            bool terminada = this.TiempoAnimacion == 0;

            if (aLaMitad)
                MoverA(X, Y + direccion);
            else if (terminada)
                puntosAcumulados = 0;

            base.Dibujar();
        }
    }
}
