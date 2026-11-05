namespace Space_Invaders._02aplication.Enemigos
{
    class AlienVerde : Enemigo
    {
        public AlienVerde(int X, int Y, int obtencion) : base(X, Y, obtencion)
        {
            colorSprite = ConsoleColor.DarkGreen;
            spritePrincipal = "][";
            spriteSecundario = "[]";
            Imagen = spritePrincipal;
        }

        public override void Dibujar()
        {
            base.Dibujar();
            Console.ResetColor();
        }
    }
}
