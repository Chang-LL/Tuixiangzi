using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class JumpCommand : CommandBase
    {
        readonly Level level;
        public JumpCommand(Level level, Location destination)
        {
            Destination = destination;
            this.level = level ?? throw new ArgumentNullException("level");
        }

        public Location Destination
        {
            get;
            private set;
        }

      

        public override void Execute()
        {
            Location manLocation = level.Actor.Location;//得到当前角色的位置
            if (manLocation.IsAdjacentLocation(Destination))//如果目标位置是邻近位置
            {/* 因为位置邻近，所以不需要跳转，使用Move即可 */
                Direction direction = manLocation.GetDirection(Destination);
                Move move = new Move(direction);
                level.Actor.DoMove(move);
            }
            else
            {   //如果目标位置不是邻近的位置，执行跳转移动
                Jump jump = new Jump(Destination);
                level.Actor.DoMove(jump);
            }
        }

       

        public override void Redo()
        {
            Execute();
        }

       

        public override void Undo()
        {
            level.Actor.UndoMove();
        }
    }
}