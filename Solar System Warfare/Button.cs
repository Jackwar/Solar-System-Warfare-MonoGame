using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare
{
    public enum ButtonPressed
    {
        Start,
        Back,
        Credits,
        None
    }

    public class Button
    {
        public Texture2D Texture { get; }
        public Rectangle Position { get; set; }

        public Button(Texture2D texture, Rectangle position)
        {
            Texture = texture;
            Position = position;
        }

        public bool IsClicked(MouseState mouseState)
        {
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                if(Position.Contains(mouseState.Position))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
