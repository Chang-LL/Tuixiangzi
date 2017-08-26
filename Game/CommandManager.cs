using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class CommandManager
    {

        public CommandManager()
        {
            throw new System.NotImplementedException();
        }

        public CommandBase refoStack
        {
            get => default(CommandBase);
            set
            {
            }
        }

        public CommandBase CommandStack
        {
            get => default(CommandBase);
            set
            {
            }
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void Execute(CommandBase command)
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