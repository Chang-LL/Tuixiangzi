using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MGame
{
    public class Level
    {
        List<GoalCell> goals = new List<GoalCell>();
        Cell[][] cells;
        public Cell this[int rowNumber, int columnNumber]
        {
            get
            {
                return cells[rowNumber][columnNumber];
            }
        }
        public Cell this[Location location]
        {
            get
            {
                if (InBounds(location))//如果该位置在游戏区域内部
                {  //返回这个位置的Cell对象
                    return this[location.RowNumber, location.ColumnNumber];
                }
                return null;
            }
        }
        public int LevelNumber
        {
            get;
            private set;


        }

        protected Game Game
        {
            get ;
            set;
        }

        public Actor Actor
        {
            get ;
            private set;
            
        }

        public int RowCount
        {
            get
            {
                return cells != null ? cells.Length : 0;
            }           
        }

        public int ColumnCount
        {
            get
            {
                return cells != null && cells.Length > 0 ? cells[0].Length : 0;
            }
           
        }



        public void Load(TextReader mapStream)
        {
            if (mapStream == null)//如果流为空则抛出异常
            {
                throw new ArgumentNullException("mapStream");
            }
            //初始化二维行列方块列表
            List<List<Cell>> rows = new List<List<Cell>>();
            string gridRowText;//用于保存关卡行数据的字符串变量
            int rowCount = 0;  //初始行数总
                               //从流中循环读取行数据
            while ((gridRowText = mapStream.ReadLine()) != null
                && gridRowText.Trim() != string.Empty)
            {   //使用BuildCells方法构造Cell行集合并加载到List中
                rows.Add(BuildCells(gridRowText, rowCount++));
            }
            cells = new Cell[rowCount][];//初始化cells数组
            for (int i = 0; i < rowCount; i++)
            {   //使用泛型List的ToArray方法将列表转换为数组
                cells[i] = rows[i].ToArray();
            }
        }

        private List<Cell> BuildCells(string rowText, int rowNumber)
        {
            List<Cell> row = new List<Cell>(rowText.Length);
            int columnNumber = 0;//初始列编号
            foreach (char c in rowText)//循环行字符串的字符
            {   //初始化Location对象
                Location location = new Location(rowNumber, columnNumber++);
                switch (c)
                {
                    case '#': /* 墙体方块. */
                        row.Add(new WallCell(location, this));
                        break;
                    case ' ': /* 空白区域. */
                        row.Add(new FloorCell(location, this));
                        break;
                    case '$': /* 在方格中的箱子 */
                        row.Add(new FloorCell(location, this,
                            new Treasure(location, this)));
                        break;
                    case '*': /* 在目标方块中的箱子. */
                        GoalCell goalCellWithTreasure =
                            new GoalCell(location, this,
                                new Treasure(location, this));
                        goalCellWithTreasure. //关联事件
                            CompletedGoalChanged +=
                            new EventHandler(GoalCell_CompletedGoalChanged);
                        goals.Add(goalCellWithTreasure);//添加目标方块集合
                        row.Add(goalCellWithTreasure);  //添加到行集合
                        break;
                    case '.': /* 目标方块. */
                        GoalCell goalCell = new GoalCell(location, this);
                        goalCell.CompletedGoalChanged +=
                            new EventHandler(GoalCell_CompletedGoalChanged);
                        goals.Add(goalCell);//添加到目标集合
                        row.Add(goalCell);  //添加到行集合
                        break;
                    case '@': /* 在地面方块中的角色. */
                        Actor actor = new Actor(location, this);
                        row.Add(new FloorCell(location, this, actor));
                        Actor = actor;
                        break;
                    case '!': /* 空白方块. */
                        row.Add(new SpaceCell(location, this));
                        break;
                    default://如果没有这几种字符，则抛出异常
                        throw new FormatException("在关卡地图中找到无效的关卡符号: " + c);
                }
            }
            return row;//返回行列表
        }

        private void GoalCell_CompletedGoalChanged(object sender, EventArgs e)
        {
            foreach (GoalCell goal in goals)//循环所有的目标方块
            {
                if (!goal.HasTreasure)//如果目标方块上面不存在箱子
                {
                    return;//立即返回
                }
            }
            OnLevelCompleted(EventArgs.Empty);//否则触发事件
        }

        public Level (Game game,int levelNumber)
        {
            Game = game;
            LevelNumber= levelNumber;
        }
        public bool InBounds(Location location)
        {
            return (location.RowNumber >= 0
                && location.RowNumber < RowCount
                && location.ColumnNumber >= 0
                && location.ColumnNumber < ColumnCount);
        }
        #region LevelCompleted event

        event EventHandler levelCompleted;

        /// <summary>
        /// Occurs when a level has been completed successfully.
        /// </summary>
        public event EventHandler LevelCompleted
        {
            add
            {
                levelCompleted += value;
            }
            remove
            {
                levelCompleted -= value;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:LevelCompleted"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void OnLevelCompleted(EventArgs e)
        {
            if (levelCompleted != null)
            {
                levelCompleted(this, e);
            }
        }
        #endregion
    }
}