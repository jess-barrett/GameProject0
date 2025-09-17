using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameProject0.Screens
{
    public class Room
    {
        private string _backgroundAsset;
        private string _floorTileAsset;
        private Texture2D _background;
        private Texture2D _floorTile;
        private List<Rectangle> _floorRects; // colliders only
        private int _visualRows = 5; // number of rows to draw

        public Room(string backgroundAsset, string floorTileAsset)
        {
            _backgroundAsset = backgroundAsset;
            _floorTileAsset = floorTileAsset;
            _floorRects = new List<Rectangle>();
        }

        public IReadOnlyList<Rectangle> FloorColliders => _floorRects;

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _background = content.Load<Texture2D>(_backgroundAsset);
            _floorTile = content.Load<Texture2D>(_floorTileAsset);

            var viewport = graphicsDevice.Viewport;
            int tilesAcross = viewport.Width / _floorTile.Width;
            _floorRects.Clear();
            int colliderY = viewport.Height - _floorTile.Height * _visualRows; // top row of visual tiles

            for (int i = 0; i < tilesAcross; i++)
            {
                int x = i * _floorTile.Width;
                _floorRects.Add(new Rectangle(x, colliderY, _floorTile.Width, _floorTile.Height));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var viewport = spriteBatch.GraphicsDevice.Viewport;
            var destRect = new Rectangle(0, 0, viewport.Width, viewport.Height);

            // Draw background
            spriteBatch.Draw(_background, destRect, Color.White);

            int tilesAcross = viewport.Width / _floorTile.Width;
            int startY = viewport.Height - _floorTile.Height * _visualRows; // top of the first visual row

            // Draw all visual rows
            for (int row = 0; row < _visualRows; row++)
            {
                int y = startY + row * _floorTile.Height;
                for (int col = 0; col < tilesAcross; col++)
                {
                    int x = col * _floorTile.Width;
                    spriteBatch.Draw(_floorTile, new Rectangle(x, y, _floorTile.Width, _floorTile.Height), Color.White);
                }
            }
        }
    }
}