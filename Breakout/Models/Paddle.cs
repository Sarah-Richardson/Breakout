using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Models
{
    public class Paddle : Sprite
    {
        public Paddle(ContentManager content, float width, float height, int speed) : base(width, height, speed)
        {
            Texture = content.Load<Texture2D>("paddle");
            Position = new Vector2(width / 2, height);
        }

        public void UpdatePosition()
        {
            Position = new Vector2(Position.X + DirectionX, Height - Texture.Height);
            var leftSide = (Position.X <= 0);
            var rightSide = (Position.X >= (Width - Texture.Width));
            if ((leftSide) || (rightSide))
            {
                DirectionX = 0;
                if (leftSide) Position = new Vector2(0, Height - Texture.Height);
                if (rightSide) Position = new Vector2(Width - Texture.Width, Height - Texture.Height);
            }
        }
    }
}
