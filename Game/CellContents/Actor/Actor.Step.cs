using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    public partial class Actor
    {
        /// <summary>
        /// 向指定的方向进行移动
        /// </summary>
        internal bool DoMove(Move move)
        {
            lock (moveLock)//使用线程同步对象锁定当前移动
            {   //执行实际的移动操作
                return DoMoveAux(move);
            }
        }
        internal bool DoMoveAux(Move move)//单步移动操作辅助方法
        {
            bool result = false;//初始化返回值
            Location moveLocation = Location. //得到指定方向的邻近的一个位置
                GetAdjacentLocation(move.Direction);
            if (Level.InBounds(moveLocation)) //判断位置是否在游戏区域内部
            {
                Cell toCell = Level[moveLocation];//得到要移动位置的Cell对象
                Cell fromCell = Level[Location];  //得到来源位置的Cell对象
                CellContents toCellContents = toCell.CellContents;//获取移动位置的内容
                if (toCell.CanEnter) //如果目标Cell允许设置内容
                {   //如果不是撤消操作而是一个标准移动
                    if (!move.Undo)
                    {   //设置目标Cell的方块内容
                        result = toCell.TrySetContents(this);
                        if (result)
                        {
                            Move newMove =  //为撤消操作指定相反的方向以备撤消
                                new Move(move.Direction.GetOppositeDirection())
                                { Undo = true };
                            moves.Push(newMove);//向撤消堆栈中插入Move
                            MoveCount++;        //移动次数增加
                        }
                    }
                    //如果是撤消移动操作的话
                    else if (move.PushedContents != null) //如果角色推动的内容存在
                    {   //设置目标方块的推动内容
                        toCell.TrySetContents(this);
                        //然后向源方块设置推动的内容，因为单步移动操作
                        result = fromCell.TrySetContents(move.PushedContents);
                        if (result)
                        {   //移动次数减少
                            MoveCount--;
                        }
                    }
                    else
                    {   /* 如果没有推动内容，直接设置目标方块内容 */
                        result = toCell.TrySetContents(this);
                        if (result)
                        {   //移动次数减少
                            MoveCount--;
                        }
                    }
                }//如果目标方块有内容，则先将目标方块内容推动到邻近方块
                else if (toCell.TryPushContents(move.Direction))
                {   //如果不为撤消操作
                    if (!move.Undo)
                    {
                        Move newMove =  //实例化Move，并指定推动的内容以便撤消
                            new Move(move.Direction.GetOppositeDirection())
                            { Undo = true, PushedContents = toCellContents };
                        moves.Push(newMove);//压入撤消堆栈
                    }
                    //设置目标方块的内容
                    result = toCell.TrySetContents(this);
                    if (result)
                    {   //移动次数增加
                        MoveCount++;
                    }
                }
            }
            return result;//返回最终操作结果
        }
    }
}
