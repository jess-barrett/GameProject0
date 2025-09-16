using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0.Screens
{
    public class GameplayScreen : GameScreen
    {
        private Room _room;
        private Texture2D _playerSheet;
        private Vector2 _playerPos = new Vector2(100, 100);

        private int _frame;
        private double _frameTimer;
        private double _fps = 6.0;

        public GameplayScreen(Room room)
        {
            _room = room;
        }

        public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            base.LoadContent(content, graphicsDevice);

            _room.LoadContent(content);
            _playerSheet = content.Load<Texture2D>("PlayerSpriteSheet");
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Right))
            {
                _playerPos.X += 2;
                Animate(2, 4, gameTime);
            }
            else if (kstate.IsKeyDown(Keys.Left))
            {
                _playerPos.X -= 2;
                Animate(5, 7, gameTime);
            }
            else
            {
                Animate(0, 1, gameTime);
            }
        }

        private void Animate(int startFrame, int endFrame, GameTime gameTime)
        {
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_frameTimer >= 1.0 / _fps)
            {
                _frame++;
                if (_frame > endFrame) _frame = startFrame;
                _frameTimer = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _room.Draw(spriteBatch);

            // Draw player
            int frameWidth = 16;
            int frameHeight = 16;
            Rectangle source = new Rectangle(_frame * frameWidth, 0, frameWidth, frameHeight);

            spriteBatch.Draw(
                _playerSheet,
                _playerPos,
                source,
                Color.White,
                0f,
                Vector2.Zero,
                3f,
                SpriteEffects.None,
                0f
            );

            spriteBatch.End();
        }
    }
}
