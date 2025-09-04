using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject0.Screens
{
    public class ScreenManager
    {
        private GameScreen _currentScreen;

        public void SetScreen(GameScreen screen, ContentManager content, GraphicsDevice graphicsDevice)
        {
            _currentScreen = screen;
            _currentScreen.LoadContent(content, graphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            _currentScreen?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen?.Draw(spriteBatch);
        }
    }
}
