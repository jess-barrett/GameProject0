using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Room
{
    private Texture2D _background;
    private Texture2D _tileTexture;

    private List<Vector2> _tilePositions = new List<Vector2>();

    public Room(string backgroundAsset, string tileAsset, List<Vector2> tilePositions)
    {
        BackgroundAsset = backgroundAsset;
        TileAsset = tileAsset;
        _tilePositions = tilePositions;
    }

    public string BackgroundAsset { get; }
    public string TileAsset { get; }

    public void LoadContent(ContentManager content)
    {
        _background = content.Load<Texture2D>(BackgroundAsset);
        _tileTexture = content.Load<Texture2D>(TileAsset);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw background
        spriteBatch.Draw(_background, Vector2.Zero, Color.White);

        // Draw tiles
        foreach (var pos in _tilePositions)
        {
            spriteBatch.Draw(_tileTexture, pos, Color.White);
        }
    }
}
