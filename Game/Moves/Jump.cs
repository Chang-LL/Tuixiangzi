using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class Jump : MoveBase
    {
        public Location Destination
        {
            get;
            private set;
        }
        public Move[] Route
        {
            get;
            private set;
        }
        public Jump(Location destination)
        {
            this.Destination = destination;
        }
        public Jump(Move[] route)
        {
            Route = route;
        }
    }
}