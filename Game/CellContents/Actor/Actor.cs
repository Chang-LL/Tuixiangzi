using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public partial class Actor : CellContents
    {
        readonly Stack<MoveBase> moves = new Stack<MoveBase>();
        int moveCount;
        /* 将被DoMove方法使用的锁定对象 */
        readonly object moveLock = new object();
        public int stepDelay = 100;//默认延迟时长
                                   /// <summary>
                                   /// 获取或设置为了跳跃而需要的单步延迟时常
                                   /// </summary>
                                   /// <value>
                                   /// 该值以毫秒为单位，在一个跳跃期间，单步被异步的执行，
                                   /// 线程将睡眠指定的时间</value>
        public int StepDelay
        {
            get
            {
                return stepDelay;
            }
            set
            {
                stepDelay = value;
                OnPropertyChanged("StepDelay");//触发变更通知
            }
        }
        /// <summary>
        /// 得到移动次数
        /// </summary>
        public int MoveCount
        {
            get
            {
                return moveCount;//返回移动次数
            }
            private set
            {
                moveCount = value;
                if (moveCount < 0)
                {   /* 万一总数小于0,出于安全起见等于0 */
                    moveCount = 0;
                }//触发属性变更通知
                OnPropertyChanged("MoveCount");
            }
        }

        /// <summary>
        /// Actor类的构造函数
        /// </summary>
        public Actor(Location location, Level level)
            : base("Actor", location, level)
        {
        }

        /// <summary>
        /// 撤消最后一次的移动，移动可能是单步或一系列的移动
        /// 一系列的移动将被进行跳转
        /// </summary>
        /// <returns></returns>
        public bool UndoMove()
        {
            if (moves.Count < 1)//如果撤消堆栈没有记录
            {
                return false;//退出移动
            }
            MoveBase moveBase = moves.Pop();//从堆栈中弹出撤消命令
            Move move = moveBase as Move;//如果操作为单步移动
            if (move != null)
            {   //执行撤消移动
                return DoMove(move);
            }
            Jump jump = moveBase as Jump;//如果操作为多步跳转
            if (jump != null)
            {   //执行多步跳转
                return DoMove(jump);
            }
            Push push = moveBase as Push;//如果操作为推送
            if (push != null)
            {   //执行推送
                return DoMove(push);
            }
            return false;
        }
    }
}