using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject0.Screens
{
    public class TitleScreen : GameScreen
    {
        private Texture2D _bgTexture;
        private Texture2D _titleTexture;
        private Texture2D _wizardFrame1;
        private Texture2D _wizardFrame2;
        private Bat _bat;

        private double _frameTimer;
        private int _currentFrame;

        private SpriteFont _font;

        private ScreenManager _screenManager;

        public TitleScreen(ScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            base.LoadContent(content, graphicsDevice);

            _bgTexture = content.Load<Texture2D>("TitleScreenBG");
            _titleTexture = content.Load<Texture2D>("TitleScreenTitle");

            _wizardFrame1 = content.Load<Texture2D>("Wizard1");
            _wizardFrame2 = content.Load<Texture2D>("Wizard2");

            var batTexture = content.Load<Texture2D>("32x32-bat-sprite");
            _bat = new Bat(batTexture, new Vector2(300, 300));

            _font = content.Load<SpriteFont>("DefaultFont");
        }

        public override void Update(GameTime gameTime)
        {
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Switch frame every 0.5 seconds (adjust speed as you like)
            if (_frameTimer >= 0.5)
            {
                _currentFrame = (_currentFrame + 1) % 2;
                _frameTimer = 0;
            }

            _bat.Update(gameTime);

            var kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Enter))
            {
                var room1 = new Room(
                    "Room1",
                    "CobblestoneBlock",
                    new List<Vector2>
                    {
                        new Vector2(50, 300),
                        new Vector2(66, 300),
                        new Vector2(82, 300)
                    }
                );

                _screenManager.SetScreen(new GameplayScreen(room1), Content, GraphicsDevice);
            }
            else if (kb.IsKeyDown(Keys.Escape))
            {
                // Exit the game entirely
                Environment.Exit(0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Background
            var viewport = GraphicsDevice.Viewport;
            var destRect = new Rectangle(0, 0, viewport.Width, viewport.Height);
            spriteBatch.Draw(_bgTexture, destRect, Color.White);

            // Title
            var scaledWidth = (int)(_titleTexture.Width);
            var scaledHeight = (int)(_titleTexture.Height);
            var titleX = (viewport.Width - scaledWidth) / 2;
            var titleY = (viewport.Height - scaledHeight) / 4;
            spriteBatch.Draw(_titleTexture, new Vector2(titleX, titleY), Color.White);

            // Wizard animation
            var wizardTexture = (_currentFrame == 0) ? _wizardFrame1 : _wizardFrame2;
            int wizardX = (viewport.Width / 2) - wizardTexture.Width - 200;
            int wizardY = (viewport.Height / 2) - (wizardTexture.Height / 2 - 75);
            spriteBatch.Draw(wizardTexture, new Vector2(wizardX, wizardY), Color.White);

            // Draw Bat
            _bat.Draw(spriteBatch);

            // Draw instructions text
            string instructions = "Press ESC to Quit";

            var textSize = _font.MeasureString(instructions);
            var textX = (viewport.Width - textSize.X) / 2;
            var textY = viewport.Height - textSize.Y - 125;

            spriteBatch.DrawString(_font, instructions, new Vector2(textX, textY), Color.White);

            spriteBatch.End();
        }
    }
}
