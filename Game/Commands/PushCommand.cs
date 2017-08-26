using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class PushCommand : CommandBase
    {
        private int level;

        public PushCommand()
        {
            throw new System.NotImplementedException();
        }

        public Location Destination
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