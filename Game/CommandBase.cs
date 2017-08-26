using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public abstract class CommandBase
    {
        public abstract void Execute();

        public abstract void Redo();

        public abstract void Undo();
    }
}