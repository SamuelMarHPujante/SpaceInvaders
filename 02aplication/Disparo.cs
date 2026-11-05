namespace Space_Invaders._02aplication
{
    class Disparo : Sprite
    {
        public bool Activo { get; set; }

        public Disparo(int X, int Y, ConsoleColor colorBalas)
        {
            Activo = false;
            this.Imagen = "|";
            this.X = X;
            this.Y = Y;
            colorSprite = colorBalas;
        }

        public override void Dibujar()
        {
            if (Activo)
            {
                base.Dibujar();
                Console.ResetColor();
            }
        }
    }
}
