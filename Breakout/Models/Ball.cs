using Microsoft.Xna.Framework;

namespace Breakout.Models
{
    public class Ball : Sprite
    {

        public Ball(float width, float height, int speed) : base(width, height, speed)
        {
            Position = new Vector2(width / 2, height / 2);
        }

        public void UpdatePosition()
        {
            if (Position.X < 0) DirectionX = 1 * Speed;
            if (Position.X > (Width - Texture.Width)) DirectionX = -1 * Speed;

            if (Position.Y < 0) DirectionY = 1 * Speed;
            if (Position.Y > (Height - Texture.Height)) DirectionY = -1 * Speed;

            Position = new Vector2(Position.X + DirectionX, Position.Y + DirectionY);
        }

        public void ChangeDirection()
        {
            DirectionY *= -1;
        }

        public float BottomOfBall()
        {
            return (Position.Y + Texture.Height) - Speed;
        }
    } 

}
