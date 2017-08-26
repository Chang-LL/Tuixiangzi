using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MGame
{
    public partial class Actor
    {
        /// <summary>
        /// 使用特定的Jump对象进行跳转移动
        /// </summary>
        /// <param name="jump">指定如何移动的Jump对象</param>
        internal bool DoMove(Jump jump)
        {
            bool result = false;//初始化结果值
            if (jump.Undo)  //如果为撤消跳转
            {
                lock (moveLock)//使用线程同步对象锁定线程
                {   //遍历移动路径
                    for (int i = jump.Route.Length - 1; i >= 0; i--)
                    {   //获得要移动的邻近的相反的方向
                        Location moveLocation = Location.GetAdjacentLocation
                            (jump.Route[i].Direction.GetOppositeDirection());
                        //得到目标Cell对象实例
                        Cell toCell = Level[moveLocation];
                        if (!toCell.TrySetContents(this))//尝试设置方块内容
                        {   //如果放置失败，抛出异常
                            throw new SokobanException("不能到达指定的路径.");
                        }
                    }
                    MoveCount -= jump.Route.Length;//减少移动步骤
                    result = true;
                }
            }
            else
            {  //使用ThreadPool接收的WaitCallback创建线程执行代码
                WaitCallback callback = delegate
                {
                    #region 匿名Jump方法.
                    lock (moveLock)
                    {   //使用SearchPathFinder查找最短移动路径
                        SearchPathFinder searchPathFinder =
                            new SearchPathFinder(Cell, jump.Destination);
                        if (searchPathFinder.TryFindPath())//查找最短目标路径
                        {   //遍历结果路径
                            for (int i = 0; i < searchPathFinder.Route.Length; i++)
                            {
                                Move move = searchPathFinder.Route[i];//得到路径中的Move对象
                                                                      /* 休眠线程指定的时长*/
                                Thread.Sleep(stepDelay);
                                Location moveLocation = //获取移动的邻近位置
                                    Location.GetAdjacentLocation(move.Direction);
                                Cell toCell = Level[moveLocation];//得到目标Cell
                                if (!toCell.TrySetContents(this))//设置目标Cell内容
                                {  //如果无法到达，则抛出异常
                                    throw new SokobanException("不能到达指定的路径.");
                                }
                                MoveCount++;//增加移动步长
                            }
                            /* 设置用于撤消跳转的Jump对象*/
                            Jump newMove = new Jump(searchPathFinder.Route)
                            { Undo = true };
                            moves.Push(newMove);//将对象压入堆栈
                            result = true;
                        }
                    }
                    #endregion
                };//将callback放入线程池，在后台执行跳转操作
                ThreadPool.QueueUserWorkItem(callback);
            }
            return result;//返回执行结果
        }
    }
}
