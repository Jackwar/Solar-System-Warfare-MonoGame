using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare
{
    class ScoreUpdater
    {
        private readonly Vector2 position = new Vector2(530, 16);
        public int Score { get; set; } = 0;

        public void AddScore()
        {
            Score += 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Assets.Font, Score.ToString("D5"), position, Color.Yellow);
        }
    }
}
