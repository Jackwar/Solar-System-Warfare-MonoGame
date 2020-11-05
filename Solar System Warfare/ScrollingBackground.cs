using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare
{
    class ScrollingBackground
    {
        private const int BottomPosition = 800;
        private const int TopPosition = -1150;
        private readonly Vector2 direction = new Vector2(0, 1);
        private readonly Background[] backgrounds;

        public ScrollingBackground()
        {
            backgrounds = new Background[]
            {
                new Background(new Vector2(-225, 150), new Vector2(300, 150)),
                new Background(new Vector2(-225, -500), new Vector2(300, -500)),
                new Background(new Vector2(-225, TopPosition), new Vector2(300, TopPosition))
            };
        }

        public void Scroll(GameTime gameTime)
        {
            foreach(var background in backgrounds)
            {
                if(background.LeftPosition.Y >= 800 )
                {
                    background.HardMove(TopPosition);
                }
                background.Move(gameTime, direction);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var background in backgrounds)
            {
                spriteBatch.Draw(background.LeftBackground, background.LeftPosition, Color.White);
                spriteBatch.Draw(background.RightBackground, background.RightPosition, Color.White);
            }
        }

        private class Background
        {
            private const float Speed = 100;
            public Texture2D LeftBackground { get => Assets.Background; }
            public Vector2 LeftPosition { get; set; }
            public Texture2D RightBackground { get => Assets.Background; }
            public Vector2 RightPosition { get; set; }
            public Background(Vector2 leftPosition, Vector2 rightPosition)
            {
                LeftPosition = leftPosition;
                RightPosition = rightPosition;
            }

            public void Move(GameTime gameTime, Vector2 direction)
            {
                LeftPosition += (direction * Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                RightPosition += (direction * Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            public void HardMove(int positionY)
            {
                LeftPosition = new Vector2(LeftPosition.X, positionY);
                RightPosition = new Vector2(RightPosition.X, positionY);
            }
        }
    }
}
