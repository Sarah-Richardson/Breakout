using Breakout.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BreakOutMain : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Ball _ball;
        private Wall _wall;
        private Paddle _paddle;

        private float _width;
        private float _height;
        private int _initialSpeed = 3;
        private Player _player;
        private SpriteFont _font;

        private bool _cheatMode;

        public BreakOutMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _width = _graphics.GraphicsDevice.Viewport.Width;
            _height = _graphics.GraphicsDevice.Viewport.Height;

            _player = new Player();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ball = new Ball(_width, _height, _initialSpeed)
            {
                Texture = Content.Load<Texture2D>("ball"),
            };

            _wall = new Wall(Content, _width, _height, _initialSpeed, _player.Level);
            _paddle = new Paddle(Content, _width, _height, _initialSpeed);
            
            _font = Content.Load<SpriteFont>("score");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            _ball.Texture.Dispose();
            _paddle.Texture.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!_cheatMode)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    _paddle.DirectionX = -1 * _paddle.Speed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    _paddle.DirectionX = 1 * _paddle.Speed;
                }

            }
            else
            {
                _paddle.Position = new Vector2((_ball.Position.X + (_ball.Texture.Width/2)) - (_paddle.Texture.Width / 2), _height - _paddle.Texture.Height);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                _cheatMode = !_cheatMode;
            }

            _ball.UpdatePosition();

            var hasCollidedWithBrick = false;
            var brickPosition = 0;

            foreach (var brick in _wall.Bricks)
            {
                if (_ball.HasCollided(brick.Bounds()))
                {
                    hasCollidedWithBrick = true;
                    break;
                }
                brickPosition++;
            }

            if (hasCollidedWithBrick)
            {
                _player.Score += (int)_wall.Bricks[brickPosition].ActualBrickColour;
                _wall.Bricks.RemoveAt(brickPosition);
                _ball.ChangeDirection();

                if (_wall.Bricks.Count == 0)
                {
                    _player.Level++;
                    _wall = new Wall(Content, _width, _height, _initialSpeed, _player.Level);
                    _ball.Position = new Vector2(_width / 2, _height / 2 + (10 *_player.Level));
                    _paddle.Speed++;
                    _ball.Speed++;
                }
            }

            if (_ball.HasCollided(_paddle.Bounds()))
            {
                _ball.ChangeDirection();
            }

            if (_ball.BottomOfBall() > _paddle.Position.Y)
            {
                Exit();
            }
            
            _paddle.UpdatePosition();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_ball.Texture, _ball.Position, _ball.Colour);

            foreach (var brick in _wall.Bricks)
            {
                _spriteBatch.Draw(brick.Texture, brick.Position, brick.Colour);
            }

            _spriteBatch.Draw(_paddle.Texture, _paddle.Position, _paddle.Colour);

            _spriteBatch.DrawString(_font, "Score : " + _player.Score, new Vector2(10, 10), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
