using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solar_System_Warfare.Movement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Spites
{
    public class Enemy: Sprite
    {
        public override Texture2D Texture { get => Assets.Enemy; }
        private const double fireRate = 1.5;
        private double lastFire = 1;
        private IPattern pattern;
        public Enemy(/*Texture2D texture,*/ Vector2 position, float speed, int durability, IPattern pattern) :
            base(/*texture,*/ position, speed, durability)
        {
            this.pattern = pattern;
        }

        public bool Firing(GameTime gameTime)
        {
            lastFire += gameTime.ElapsedGameTime.TotalSeconds;
            if(lastFire >= fireRate)
            {
                lastFire = 0;
                return true;
            }
            return false;
        }

        public override void Move(GameTime gameTime)
        {
            Direction = pattern.Direction(Position);
            base.Move(gameTime);
        }
    }
}
