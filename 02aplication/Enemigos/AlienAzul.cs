namespace Space_Invaders._02aplication.Enemigos
{
    class AlienAzul : Enemigo
    {
        public AlienAzul(int X, int Y, int obtencion) : base(X, Y, obtencion)
        {
            colorSprite = ConsoleColor.DarkBlue;
            spritePrincipal = ")(";
            spriteSecundario = "()";
            Imagen = spritePrincipal;
        }

        public override void Dibujar()
        {
            base.Dibujar();
            Console.ResetColor();
        }
    }
}
