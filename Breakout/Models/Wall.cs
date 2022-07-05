using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout.Models
{
    public class Wall
    {
        public List<Brick> Bricks { get; set; } = new List<Brick>();
        
        public int NumberOfBricksInWidth { get; set; } = 10;
        public int NumberOfBricksInHeight { get; set; } = 2;

        public Wall(ContentManager content, float width, float height, int speed, int level)
        {
            var brickIndex = 1;
            for (var y = 0; y < NumberOfBricksInHeight + level; y++)
            {
                float positionY = y * 41;
                for (var x = 0; x < NumberOfBricksInWidth; x++)
                {
                    var brickColour = (Brick.BrickColour)Enum.ToObject(typeof(Brick.BrickColour), brickIndex);
                    float positionX = x * 80;
                    var brick = new Brick(new Vector2(positionX, positionY+40), content.Load<Texture2D>(brickColour.ToString()), width, height, speed);
                    brick.ActualBrickColour = brickColour;
                    Bricks.Add(brick);
                    brickIndex++;
                    if (brickIndex == 5) brickIndex = 1;
                }
            }
        }

    }
}
