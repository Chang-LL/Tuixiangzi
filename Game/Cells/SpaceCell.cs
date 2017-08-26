using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class SpaceCell : Cell
    {
        public SpaceCell(Location location, Level level)
               : base("Space", location, level)
        {
        }
    }
}