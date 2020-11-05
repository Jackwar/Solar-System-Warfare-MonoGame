using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solar_System_Warfare.Spites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare
{
    class Lives
    {
        /*private readonly Vector2 firstHeartPosition = new Vector2(16, 16);
        private readonly Vector2 secondHeartPosition = new Vector2(32, 16);
        private readonly Vector2 thirdHeartPosition = new Vector2(48, 16);*/
        private readonly Vector2[] positions;
        private readonly Texture2D[] textures;
        //private readonly Earth earth;
        
        public Lives(/*Earth earth*/)
        {
            //this.earth = earth;
            positions = new Vector2[]
            {
                new Vector2(16, 16),
                new Vector2(32, 16),
                new Vector2(48, 16),
            };
            textures = new Texture2D[]
            {
                Assets.FullHeart,
                Assets.FullHeart,
                Assets.FullHeart,
            };
        }

        public void RemoveLife(int lives)
        {
            textures[lives] = Assets.EmptyHeart;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(textures[i], positions[i], Color.White);
            }
        }
    }
}
