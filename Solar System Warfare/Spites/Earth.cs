using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Spites
{
    public class Earth : Sprite
    {
        private const double fireRate = 1;
        private double coolDown;
        public override Texture2D Texture { get => Assets.Earth; }

        public Earth(Vector2 position, float speed, int durability): 
            base(position, speed, durability){}

        public void Reset()
        {
            Position = new Vector2(300, 600);
            Durability = 3;
        }

        public void PlayerDirection(KeyboardState kState, Vector2 leftStick)
        {
            var direction = new Vector2();
            if (leftStick == Vector2.Zero)
            {                
                if(kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
                {
                    direction.Y -= 1;
                }
                if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
                {
                    direction.Y += 1;
                }
                if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
                {
                    direction.X -= 1;
                }
                if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
                {
                    direction.X += 1;
                }
            }
            else
            {
                direction = leftStick;
            }

            Direction = direction;
        }

        public void MoveEarth(GameTime gameTime)
        {
            Move(gameTime);

            if (Position.X < 0)
            {
                position.X = 0;
            }
            else if (Position.X > 600 - Texture.Width)
            {
                position.X = 600 - Texture.Width;
            }

            if (Position.Y < 0)
            {
                position.Y = 0;
            }
            else if (Position.Y > 800 - Texture.Height)
            {
                position.Y = 800 - Texture.Height;
            }
        }

        public bool Firing(GameTime gameTime, KeyboardState kState)
        {
            if(coolDown <= 0)
            {
                if (kState.IsKeyDown(Keys.Space))
                {
                    coolDown = fireRate;
                    return true;
                }
            }
            else
            {
                coolDown -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            return false;
        }
    }
}
