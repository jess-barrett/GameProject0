using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject0.Screens
{
    public class GameplayScreen : GameScreen
    {
        private Room _room;
        private Player _player;

        public GameplayScreen(Room room)
        {
            _room = room;
        }

        public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            base.LoadContent(content, graphicsDevice);

            _room.LoadContent(content, graphicsDevice);

            var playerSheet = content.Load<Texture2D>("WizardSpriteSheet");
            _player = new Player(playerSheet, new Point(100, 0));
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime, new List<Rectangle>(_room.FloorColliders));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _room.Draw(spriteBatch);
            _player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
