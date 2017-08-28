using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class Move : MoveBase
    {
        public Move(Direction direction)
        {
            Direction = direction;
        }

        public Direction Direction
        {
            get;
            private set;
        }

        public CellContents PushedContents
        {
            get;
            set;
        }
    }
}