using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class WallCell : Cell
    {
        public override bool CanEnter
        {
            get
            {
                return false;
            }
        }
        public WallCell(Location location, Level level)
           : base("Wall", location, level)
        {
        }
    }
}