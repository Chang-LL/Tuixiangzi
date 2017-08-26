using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class JumpCommand : CommandBase
    {
        public JumpCommand()
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