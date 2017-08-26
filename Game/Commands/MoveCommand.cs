using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class MoveCommand : CommandBase
    {
        Level level;
        public MoveCommand(Level level, Direction direction)
        {
            Direction = direction;//获取方向
            this.level = level ?? throw new ArgumentNullException("level");   //获取Level实例
        }

        public Direction Direction
        {
            get;
            private set;
        }

        public override void Execute()
        {
            Move move = new Move(Direction);//实例化Move类
            level.Actor.DoMove(move);//调用DoMove实现单步移动
        }
        /// <summary>
        /// 撤消己经执行的步骤回到原来的位置		
        /// </summary>
        public override void Undo()
        {
            level.Actor.UndoMove();//使用UndoMove
        }
        /// <summary>
        /// 重作己经被撤消的命令
        /// </summary>
        public override void Redo()
        {
            Execute();
        }
    }
}