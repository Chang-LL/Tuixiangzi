using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class FloorCell : Cell
    {
        const string cellName = "Floor";
        public FloorCell(Location location, Level level)
            : base(cellName, location, level)
        {
        }
        public FloorCell(Location location, Level level, CellContents contents)
            : base(cellName, location, level, contents)
        {
        }
    }
}