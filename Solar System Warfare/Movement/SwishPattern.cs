using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_System_Warfare.Movement
{
    class SwishPattern: IPattern
    {
        private readonly Vector2 MoveLeft = new Vector2(-1f, 0.4f);
        private readonly Vector2 MoveRight = new Vector2(1f, 0.4f);
        private readonly int movementEnd;
        private bool movingLeft = true;

        public SwishPattern(int movementEnd)
        {
            this.movementEnd = movementEnd;
        }

        public Vector2 Direction(Vector2 position)
        {
            if (movingLeft)
            {
                if (position.Y > movementEnd || position.X > 25)
                {
                    return MoveLeft;
                }
                else
                {
                    movingLeft = false;
                    return MoveRight;
                }
            }
            else
            {
                if (position.Y > movementEnd || position.X < 575)
                {
                    return MoveRight;
                }
                else
                {
                    movingLeft = true;
                    return MoveLeft;
                }
            }

        }
    }
}
