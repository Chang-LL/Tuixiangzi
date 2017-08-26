using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public abstract class CommandBase
    {
        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Redo()
        {
            throw new System.NotImplementedException();
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}