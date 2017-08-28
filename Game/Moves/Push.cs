using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class Push : MoveBase
    {
        public Push(Location destination)
        {
            Desrination = destination;
        }
       
        public Location Desrination
        {
            get;
            private set;
        }

        public Move[] Route
        {
            get;
            private set;
        }
        public Push(Move[] route)
        {
            Route = route;
        }
    }
}