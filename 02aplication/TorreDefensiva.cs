using System.Text;

namespace Space_Invaders._02aplication
{
    class TorreDefensiva : Sprite
    {
        private const int ANCHO_TORRE = Globales.ANCHO_TORRE;
        private const char IMAGEN_LADRILLO = '*';

        public StringBuilder[] ladrillos = {
            new StringBuilder(new String(IMAGEN_LADRILLO, ANCHO_TORRE)),
            new StringBuilder(new String(IMAGEN_LADRILLO, ANCHO_TORRE)),
            new StringBuilder(new String(IMAGEN_LADRILLO, ANCHO_TORRE))
        };

        public TorreDefensiva(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            colorSprite = Globales.COLOR_TORRES;
        }

        public bool ColisionaCon(Disparo bala)
        {
            int disparoPosX = bala.X;
            int disparoPosY = bala.Y;
            bool colisionaX = disparoPosX >= this.X && disparoPosX < this.X + ladrillos[0].Length;
            bool colisionaY = disparoPosY >= this.Y && disparoPosY < this.Y + ladrillos.Length;

            if (colisionaX && colisionaY)
            {
                bool hayLadrillo = ladrillos[disparoPosY - this.Y][disparoPosX - this.X] == IMAGEN_LADRILLO;
                if (hayLadrillo)
                {
                    ladrillos[disparoPosY - this.Y][disparoPosX - this.X] = ' ';
                    return true;
                }
            }
            return false;
        }

        public override void Dibujar()
        {
            int j = 0;
            Console.ForegroundColor = colorSprite;

            for (int i = 0; i < ladrillos.Length; i++)
            {
                bool hayLadrillo = ladrillos[i][j] == IMAGEN_LADRILLO;
                bool dentroDeAnchura = j <= ANCHO_TORRE;

                Console.SetCursorPosition(X, Y + i);
                if (hayLadrillo)        Console.WriteLine(ladrillos[i]);
                if (dentroDeAnchura)    j++;
            }

            Console.ResetColor();
        }
    }
}
