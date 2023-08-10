using System.Drawing;
using System.IO;
using System.Media;

namespace ArenaDeBatalha.GameLogic
{
    public abstract class GameObject
    {
        #region Game Object Properties
        public Bitmap Sprite { get; set; }
        public bool Active { get; set; }
        public int Speed { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get => this.Sprite.Width; }
        public int Height { get => this.Sprite.Height; }
        // Limites da tela
        public Size Bounds { get; set; }

        // Localização do retangulo aonde está o objeto
        public Rectangle Rectangle { get; set; }

        // Som do jogo
        public Stream Sound { get; set; }

        // Representa a minha tela
        public Graphics Screen { get; set; }

        // Reprodutor do som 
        private SoundPlayer soundPlayer { get; set; }
        #endregion

        #region Game Object Methods

        public abstract Bitmap GetSprite();

        public GameObject(Size bounds, Graphics screen)
        {
            this.Bounds = bounds;
            this.Screen = screen;
            this.Active = true;
            this.soundPlayer = new SoundPlayer();
            this.Sprite = this.GetSprite();
            this.Rectangle = new Rectangle(this.Left, this.Top, this.Width, this.Height);
        }

        // É necessário atualizar a tela e o objeto o tempo inteiro
        public virtual void UpdateObject()
        {
            this.Rectangle = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            this.Screen.DrawImage(this.Sprite, this.Rectangle);
        }

        // Para o objeto se mover para a esquerda
        public virtual void MoveLeft()
        {
            if (this.Left > 0) this.Left -= this.Speed;
        }


        // Para o objeto se mover para a direita
        public virtual void MoveRight()
        {
            if (this.Left < this.Bounds.Width - this.Width) this.Left += this.Speed;
        }

        // Para o objeto se mover para baixo
        public virtual void MoveDown()
        {
            this.Top += this.Speed;
        }

        // Para o objeto se mover para Cima
        public virtual void MoveUp()
        {
            this.Top -= this.Speed;
        }

        // Para saber se o objeto está saindo da tela
        public bool IsOutOfBounds()
        {
            return
                (this.Top > this.Bounds.Height + this.Height) ||
                (this.Top < -this.Height) ||
                (this.Left > this.Bounds.Width + this.Width) ||
                (this.Left < -this.Width);
        }

        // Tocar a música desejada em um determinado momento
        public void PlaySound()
        {
            soundPlayer.Stream = this.Sound;
            soundPlayer.Play();
        }

        // Para saber se o objeto está colidindo
        public bool IsCollidingWith(GameObject gameObject)
        {
            if (this.Rectangle.IntersectsWith(gameObject.Rectangle))
            {
                this.PlaySound();
                return true;
            }
            else return false;
        }

        // Destruir o objeto
        public void Destroy()
        {
            this.Active = false;
        }
        #endregion
    }

}

