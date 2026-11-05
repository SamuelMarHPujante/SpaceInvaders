namespace Space_Invaders._02aplication.Enemigos
{
    class AlienRojo : Enemigo
    {
        public AlienRojo(int X, int Y, int obtencion) : base(X, Y, obtencion)
        {
            colorSprite = ConsoleColor.DarkRed;
            spritePrincipal = "XX";
            spriteSecundario = "xx";
            Imagen = spritePrincipal;
        }

        public override void Dibujar()
        {
            base.Dibujar();
            Console.ResetColor();
        }
    }
}
