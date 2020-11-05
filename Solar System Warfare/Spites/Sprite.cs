using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Spites
{
    abstract public class Sprite
    {
        public abstract Texture2D Texture { get; }
        protected Vector2 position;
        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Direction { get; set; }
        public int PoolIndex { get; set; }
        public float Speed { get; set; }
        public int Durability { get; set; }

        public Sprite(/*Texture2D texture,*/ Vector2 position, float speed, int durability)
        {
            //Texture = texture;
            Position = position;
            Speed = speed;
            Durability = durability;
        }

        public virtual void Move(GameTime gameTime)
        {
            if(Direction.Length() > 1)
            {
                Direction.Normalize();
            }

            Position += (Direction * Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Damage(int damage)
        {
            Durability -= damage;
        }
    }
}
