using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class MoveCommand : CommandBase
    {
        public MoveCommand()
        {
            throw new System.NotImplementedException();
        }

        public Location Direction
        {
            get => default(int);
            set
            {
            }
        }

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