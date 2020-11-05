using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Spites
{
    public class EnemyProjectile : Sprite
    {
        public override Texture2D Texture { get => Assets.EnemyProjectile; }
        public EnemyProjectile(/*Texture2D texture,*/ Vector2 position, float speed, int durability) 
            : base(/*texture,*/ position, speed, durability) { }
    }
}
