using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject0
{
    public class Bat
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _startPosition;
        private float _speed;
        private bool _facingRight = true;

        private double _frameTimer;
        private int _frameIndex; // 0,1,2

        private int _frameWidth = 32;
        private int _frameHeight = 32;

        private float _travelDistance = 165f; // pixels

        public Bat(Texture2D texture, Vector2 startPosition, float speed = 50f)
        {
            _texture = texture;
            _position = startPosition;
            _startPosition = startPosition;
            _speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move bat
            if (_facingRight)
                _position.X += _speed * delta;
            else
                _position.X -= _speed * delta;

            // Flip direction after traveling distance
            if (Math.Abs(_position.X - _startPosition.X) >= _travelDistance)
            {
                _facingRight = !_facingRight;
                _startPosition = _position;
            }

            // Animate
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_frameTimer >= 0.15)
            {
                _frameIndex = (_frameIndex + 1) % 3;
                _frameTimer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int row = _facingRight ? 1 : 3; // row 2 or 4
            int col = 1 + _frameIndex;      // cols 2,3,4 → indices 1,2,3

            Rectangle source = new Rectangle(
                col * _frameWidth,
                row * _frameHeight,
                _frameWidth,
                _frameHeight
            );

            spriteBatch.Draw(_texture, _position, source, Color.White);
        }
    }
}
