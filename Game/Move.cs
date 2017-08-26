using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class Move : MoveBase
    {
        public Move(Direction direction)
        {
            throw new System.NotImplementedException();
        }

        public Direction Direction
        {
            get => default(int);
            set
            {
            }
        }

        public CellContents PushedContents
        {
            get => default(int);
            set
            {
            }
        }
    }
}