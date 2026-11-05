using System;

namespace Space_Invaders._02aplication
{
    class Sprite
    {
        protected const int ANCHO_PANTALLA = Globales.ANCHO_PANTALLA;
        protected const int LIMITE_PARTIDA = Globales.LIMITE_PARTIDA;
        protected Random rand = Globales.random;

        protected ConsoleColor colorSprite;       
        public string Imagen { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        #region Dibujado y posición

        public void MoverA(int nuevaX, int nuevaY)
        {
            bool xValida = nuevaX >= 0 && nuevaX <= ANCHO_PANTALLA;
            bool yValida = nuevaY >= 0 && nuevaY <= LIMITE_PARTIDA;

            Console.SetCursorPosition(X, Y);
            Console.Write(new string(' ', Imagen.Length));

            X = xValida ? nuevaX : X;
            Y = yValida ? nuevaY : Y;
        }

        public virtual void Dibujar()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = colorSprite;
            Console.Write(Imagen);
            Console.ResetColor();
        }

        #endregion

        public bool ColisionaCon(Sprite entidad)
        {
            int esteAncho = Imagen.Length;
            int anchoEntidad = entidad.Imagen.Length;
            int entidadPosX = entidad.X;
            int entidadPosY = entidad.Y;

            bool ColisionEnX = this.X <= entidadPosX && this.X + esteAncho >= entidadPosX || 
                             entidadPosX <= this.X && entidadPosX + anchoEntidad >= this.X;
            bool ColisionEnY = this.Y == entidadPosY;

            return ColisionEnX && ColisionEnY;
        }
    }
}