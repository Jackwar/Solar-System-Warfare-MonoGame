using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Movement
{
    class UpDownPattern : IPattern
    {
        private readonly Vector2 moveDown = new Vector2(0, 1);
        private readonly Vector2 moveLeft = new Vector2(-1, 0);
        private readonly Vector2 moveRight = new Vector2(1, 0);
        private readonly int movementEnd;
        private readonly bool moveEndLeft;
        public UpDownPattern(int movementEnd, bool moveEndLeft)
        {
            this.movementEnd = movementEnd;
            this.moveEndLeft = moveEndLeft;
        }
        public Vector2 Direction(Vector2 position)
        {
            if(position.Y < movementEnd)
            {
                return moveDown;
            }
            else
            {
                if(moveEndLeft)
                {
                    return moveLeft;
                }
                else
                {
                    return moveRight;
                }
            }
        }
    }
}
