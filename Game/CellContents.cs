using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class CellContents : LevelContentBase
    {
        public CellContents(string name, Location location, Level level)
        {
            Name = name; //用于区分子类的名称
            Location = location;//当前内容所在的位置
            Level = level;//当前Level对象实例
        }

        public Cell Cell
        {
            get
            {   //返回当前位置的Cell对象
                return Level[Location];
            }
            set
            {   //设置当前方块内容的位置
                Location = value.Location;
                OnPropertyChanged("Cell");
                OnPropertyChanged("Location");
            }
        }

        public Level Level
        {
            get;
            private set;
        }

        public Location Location
        {
             get;
            private set;
        }

        public string Name
        {
             get ;
            protected set;
        }
    }
}