using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare
{
    class SpawnTimer
    {
        private double timeElapsed;
        private double spawnTime = 4;

        public bool SpawnEnemy(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if(timeElapsed >= spawnTime)
            {
                timeElapsed = 0;
                if (spawnTime > 1.2)
                {
                    spawnTime -= 0.20;
                }
                return true;
            }
            return false;
        }
    }
}
