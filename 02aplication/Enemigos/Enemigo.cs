using System.Diagnostics.CodeAnalysis;

namespace Space_Invaders._02aplication.Enemigos
{
    class Enemigo : Sprite
    {
        protected string spritePrincipal, spriteSecundario;
        protected bool usarSpritePrincipal;
        public bool Activo { get; set; }
        public int Obtencion { get; set; }

        public Enemigo()
        {
            usarSpritePrincipal = false;
            Activo = true;
        }
        public Enemigo(int X, int Y, int obtencion)
        {
            usarSpritePrincipal = false;
            Activo = true;
            this.X = X;
            this.Y = Y;
            Obtencion = obtencion;
        }

        public override void Dibujar()
        {
            if (Activo)
            {
                base.Dibujar();
            }
        }

        public void InvertirCambio()
        {
            usarSpritePrincipal = !usarSpritePrincipal;
            Imagen = usarSpritePrincipal ? spriteSecundario : spritePrincipal;
        }
    }
}