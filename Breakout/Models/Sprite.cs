using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Models
{
    public abstract class Sprite
    {
        public Texture2D Texture { get; set; }
        public Color Colour { get; set; } = Color.White;
        public Vector2 Position { get; set; } = new Vector2(0, 0);

        public float Width { get; set; }
        public float Height { get; set; }

        public int Speed { get; set; }

        public float DirectionY { get; set; }
        public float DirectionX { get; set; }

        protected Sprite(float width, float height, int speed)
        {
            Width = width;
            Height = height;
            Speed = speed;
            DirectionX = 1 * Speed;
            DirectionY = 1 * Speed;
        }

        public Rectangle Bounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public bool HasCollided(Rectangle rectangle)
        {
            return Bounds().Intersects(rectangle);
        }

    }
}
