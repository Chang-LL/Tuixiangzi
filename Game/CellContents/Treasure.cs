using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class Treasure : CellContents
    {
        public Treasure(Location location, 
            Level level):base("Treasure",location,level)
        {
            
        }
    }
}