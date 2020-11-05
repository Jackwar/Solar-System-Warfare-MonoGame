using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Movement
{
    public interface IPattern
    {
        public Vector2 Direction(Vector2 position);
    }
}
