using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class PushCommand : CommandBase
    {
        readonly Level level;

        public PushCommand(Level level, Location destination)
        {
            Destination = destination;
            this.level = level ?? throw new ArgumentNullException("level");
        }
        public Location Destination
        {
            get;
            private set;
        }

        public override void Execute()
        {
            Push push = new Push(Destination);
            level.Actor.DoMove(push);
        }
        public override void Undo()
        {
            level.Actor.UndoMove();
        }
        public override void Redo()
        {
            Execute();
        }
    }
}