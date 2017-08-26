using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class GoalCell : Cell
    {
        const string cellName = "Goal";

        public bool HasTreasure
        {
            get
            {   //判断方块内容是否为一个Treasure，即箱子
                return CellContents is Treasure;
            }
        }
        public GoalCell(Location location, Level level)
                : base(cellName, location, level)
        {
        }
        public GoalCell(Location location, Level level, CellContents contents)
            : base(cellName, location, level, contents)
        {
        }
        public override bool TrySetContents(CellContents contents)
        {
            if (base.TrySetContents(contents))//使用基类的方法设置内容
            {
                if (contents is Treasure)   //如果内容是箱子时
                {   //触发完成事件，判断是否所有的目标方块上面己经装满了箱子
                    OnCompletedGoalChanged(EventArgs.Empty);
                }
                return true;
            }
            return false;
        }
        public override void RemoveContents()
        {
            /* 首先检查是否内容不为空，或者内容是否为箱子 */
            if (CellContents != null && CellContents is Treasure)
            {   //如果是的话，触发完成事件
                OnCompletedGoalChanged(EventArgs.Empty);
            }
            base.RemoveContents();//调用基类的移除方法
        }
        #region CompletedGoalChanged event
        event EventHandler completedGoalChanged;

        /// <summary>
        /// Occurs when a <see cref="Treasure"/> instance
        /// is either removed from or placed in this goal.
        /// </summary>
        public event EventHandler CompletedGoalChanged
        {
            add
            {
                completedGoalChanged += value;
            }
            remove
            {
                completedGoalChanged -= value;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:CompletedGoalChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void OnCompletedGoalChanged(EventArgs e)
        {
            if (completedGoalChanged != null)
            {
                completedGoalChanged(this, e);
            }
        }
        #endregion

    }
}