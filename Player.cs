using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameProject0
{
    public class Player
    {
        public Point Position;
        public int Width;
        public int Height;

        private Texture2D _spriteSheet;

        private int _frame;
        private double _frameTimer;
        private double _fps = 6.0;

        private float _speed = 2f;
        private Vector2 _velocity = Vector2.Zero;
        private float _gravity = 0.3f;
        private float _jumpStrength = -7f;
        private bool _onGround = false;

        private float _scale = 3f; // sprite scale
        private const float _groundTolerance = 0.1f; // tolerance for ground detection

        public Player(Texture2D spriteSheet, Point startPos)
        {
            _spriteSheet = spriteSheet;
            Position = startPos;
            Width = 16;
            Height = 16;
        }

        // Bounds now scaled to match drawn sprite
        public Rectangle Bounds => new Rectangle(
            Position.X,
            Position.Y,
            (int)(Width * _scale),
            (int)(Height * _scale)
        );

        public void Update(GameTime gameTime, List<Rectangle> floorColliders)
        {
            var kbState = Keyboard.GetState();

            // Horizontal movement
            _velocity.X = 0;

            if (kbState.IsKeyDown(Keys.D) && !kbState.IsKeyDown(Keys.A))
            {
                _velocity.X = _speed;
            }
            else if (kbState.IsKeyDown(Keys.A) && !kbState.IsKeyDown(Keys.D))
            {
                _velocity.X = -_speed;
            }
            // else _velocity.X stays 0 (idle) if both or neither are pressed

            // Apply horizontal movement
            Position.X += (int)_velocity.X;
            // Handle horizontal collisions here if needed

            // Jump
            if (kbState.IsKeyDown(Keys.Space) && _onGround)
            {
                _velocity.Y = _jumpStrength;
                _onGround = false;
            }

            // Apply gravity
            _velocity.Y += _gravity;

            // Apply vertical movement
            Position.Y += (int)_velocity.Y;

            // Handle vertical collisions
            HandleVerticalCollisions(floorColliders);

            // Animate
            Animate(gameTime, kbState);
        }

        private void HandleVerticalCollisions(List<Rectangle> floorColliders)
        {
            _onGround = false;
            foreach (var rect in floorColliders)
            {
                if (Bounds.Intersects(rect))
                {
                    if (_velocity.Y > _groundTolerance) // falling
                    {
                        Position.Y = rect.Top - (int)(Height * _scale);
                        _velocity.Y = 0;
                        _onGround = true;
                    }
                    else if (_velocity.Y < -_groundTolerance) // jumping up
                    {
                        Position.Y = rect.Bottom;
                        _velocity.Y = 0;
                    }
                    else if (_velocity.Y >= -_groundTolerance && _velocity.Y <= _groundTolerance) // near zero velocity
                    {
                        Position.Y = rect.Top - (int)(Height * _scale);
                        _velocity.Y = 0;
                        _onGround = true;
                    }
                }
            }
        }

        private void Animate(GameTime gameTime, KeyboardState kbState)
        {
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            int startFrame, endFrame;

            if (kbState.IsKeyDown(Keys.D) && !kbState.IsKeyDown(Keys.A))
            {
                startFrame = 2;
                endFrame = 5;
            }
            else if (kbState.IsKeyDown(Keys.A) && !kbState.IsKeyDown(Keys.D))
            {
                startFrame = 6;
                endFrame = 9;
            }
            else
            {
                startFrame = 0;
                endFrame = 1;
            }

            // Reset frame if it's outside the current animation range
            if (_frame < startFrame || _frame > endFrame)
            {
                _frame = startFrame;
                _frameTimer = 0;
            }

            if (_frameTimer >= 1.0 / _fps)
            {
                _frame++;
                if (_frame > endFrame)
                    _frame = startFrame;
                _frameTimer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle source = new Rectangle(_frame * Width, 0, Width, Height);

            spriteBatch.Draw(
                _spriteSheet,
                new Vector2(Position.X, Position.Y),
                source,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,       // scale applied
                SpriteEffects.None,
                0f
            );
        }
    }
}