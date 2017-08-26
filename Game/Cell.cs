using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public abstract class Cell : LevelContentBase
    {
        public Cell(string name, Location location, Level level)
        {
            throw new System.NotImplementedException();
        }
        public Cell(string name, Location location,
            Level level, CellContents contents) : this(null, location, level)
        {
            throw new System.NotImplementedException();
        }
        public string Name
        {
            get => default(int);
            set
            {
            }
        }

        public Location Location
        {
            get => default(int);
            set
            {
            }
        }

        public Level Level
        {
            get => default(int);
            set
            {
            }
        }

        public CellConTents CellContents
        {
            get => default(int);
            set
            {
            }
        }

        public virtual bool CanEnter
        {
            get { return CellContents == null; }
        }

        public virtual void RemoveContents()
        {
            throw new System.NotImplementedException();
        }

        public bool TrySetContents(CellContents Contents)
        {
            throw new System.NotImplementedException();
        }

        public bool TryPushContents(Direction direction)
        {
            throw new System.NotImplementedException();
        }

        public bool CanPush(Direction direction)
        {
            throw new System.NotImplementedException();
        }
    }
}