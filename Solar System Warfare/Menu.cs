using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare
{
    public class Menu
    {
        private const double ButtonTimerCooldown = 0.2;
        private double currentButtonTime;
        private bool buttonOnCooldown = false;
        public Button StartButton { get; set; }
        public Button BackButton { get; set; }
        public Button CreditsButton { get; set; }
        public bool CreditsOpen { get; set; } = false;

        public Menu()
        {
            StartButton = new Button(Assets.StartButton, new Rectangle(201, 600, 197, 62));
            BackButton = new Button(Assets.BackButton, new Rectangle(201, 600, 197, 62));
            CreditsButton = new Button(Assets.CreditsButton, new Rectangle(201, 500, 197, 62));
        }

        public ButtonPressed Update(MouseState mouseState, GameTime gameTime)
        {
            if (!buttonOnCooldown)
            {
                if (StartButton.IsClicked(mouseState))
                {
                    if (!CreditsOpen)
                    {
                        buttonOnCooldown = true;
                        return ButtonPressed.Start;
                    }
                }
                if (BackButton.IsClicked(mouseState))
                {
                    if (CreditsOpen)
                    {
                        buttonOnCooldown = true;
                        CreditsOpen = false;
                        return ButtonPressed.Back;
                    }
                }
                if (CreditsButton.IsClicked(mouseState))
                {
                    if (!CreditsOpen)
                    {
                        buttonOnCooldown = true;
                        CreditsOpen = true;
                        return ButtonPressed.Credits;
                    }
                }
            }
            else
            {
                currentButtonTime += gameTime.ElapsedGameTime.TotalSeconds;
                if(currentButtonTime >= ButtonTimerCooldown)
                {
                    currentButtonTime = 0;
                    buttonOnCooldown = false;
                }
            }
            return ButtonPressed.None;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!CreditsOpen)
            {
                spriteBatch.Draw(StartButton.Texture, StartButton.Position, Color.White);
                spriteBatch.Draw(CreditsButton.Texture, CreditsButton.Position, Color.White);
            }
            else
            {
                spriteBatch.Draw(BackButton.Texture, StartButton.Position, Color.White);
            }
        }
    }
}
