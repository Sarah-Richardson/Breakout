using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Models
{
    public class Brick : Sprite
    {
        public enum BrickColour
        {
            greenBrick = 1,
            orangeBrick = 2,
            redBrick = 3,
            yellowBrick = 4
        }

        public BrickColour ActualBrickColour { get; set; }

        public Brick(Vector2 position, Texture2D texture, float width, float height, int speed) : base(width, height, speed)
        {
            Position = position;
            Texture = texture;
        }
        
    }
}
