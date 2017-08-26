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
            Name = name; 
            Location = location;
            Level = level;
        }
        public Cell(string name, Location location,
            Level level, CellContents contents) : this(null, location, level)
        {
            CellContents = contents;		
            contents.Cell = this;
        }
        public string Name
        {
            get ;
            protected set;
           
        }

        public Location Location
        {
            get;
            private set;
        }

        public Level Level
        {
            get;
            private set;
        }

        public CellContents CellContents
        {
            get;
            private set;
        }

        public virtual bool CanEnter
        {
            get { return CellContents == null; }
        }

        public virtual void RemoveContents()
        {
            CellContents = null;
            OnPropertyChanged("CellContents");
        }

        public virtual  bool TrySetContents(CellContents contents)
        {
            if (CanEnter)
            {
                contents.Cell.RemoveContents();//先移除方块内容
                CellContents = contents;//指定方块的内容属性				
                contents.Cell = this;//指定内容所在的方块
                OnPropertyChanged("CellContents");//触发属性变更通知
                return true;
            }
            return false;
        }

        public bool TryPushContents(Direction direction)
        {
            if (!CanPush(direction))//判断指定的方向是否可以推动
            {
                return false;
            }
            Cell neighbour = //得到邻近的方格
                Level[Location.GetAdjacentLocation(direction)];
            neighbour.TrySetContents(CellContents);//设置邻近方格的内容
            return true;
        }

        public bool CanPush(Direction direction)
        {
            if (CellContents == null)//如果内容为空
            {
                return false; //不能推一个空的无内容的方块
            }
            Cell neighbour =  //获取邻近的方块
                Level[Location.GetAdjacentLocation(direction)];
            return neighbour != null && neighbour.CanEnter;
        }
    }
}