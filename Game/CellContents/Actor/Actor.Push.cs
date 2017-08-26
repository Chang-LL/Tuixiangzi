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
        /// Attempt to relocate to the another cell
        /// using the route in the specified <see cref="Push"/> instance.
        /// </summary>
        /// <param name="push">The push.</param>
        /// <returns><code>true</code> if the move succeeded, 
        /// <code>false</code> otherwise.</returns>
        internal bool DoMove(Push push)
        {
            bool result = false;

            if (push.Undo)
            {
                lock (moveLock)
                {
                    for (int i = push.Route.Length - 1; i >= 0; i--)
                    {
                        Move move = push.Route[i];
                        Location moveLocation = Location.GetAdjacentLocation(push.Route[i].Direction.GetOppositeDirection());
                        Cell toCell = Level[moveLocation];
                        Cell fromCell = this.Cell;
                        if (!toCell.TrySetContents(this))
                        {
                            throw new SokobanException("Unable to follow route.");
                        }
                        if (move.PushedContents != null)
                        {
                            if (!fromCell.TrySetContents(move.PushedContents))
                            {
                                throw new SokobanException("Unable to undo set contents.");
                            }
                        }
                    }
                    MoveCount -= push.Route.Length;
                    result = true;
                }
            }
            else
            {
                WaitCallback callback = delegate (object state)
                {
                    #region Anonymous Push method.
                    lock (moveLock)
                    {
                        PushPathFinder pathFinder = new PushPathFinder(Cell, push.Destination);
                        if (pathFinder.TryFindPath())
                        {
                            for (int i = 0; i < pathFinder.Route.Length; i++)
                            {
                                Move move = pathFinder.Route[i];

                                /* Sleep for the stepDelay period. */
                                Thread.Sleep(stepDelay);
                                Location moveLocation = Location.GetAdjacentLocation(move.Direction);
                                Cell toCell = Level[moveLocation];
                                if (!toCell.CanEnter && toCell.CellContents is Treasure)
                                {
                                    move.PushedContents = toCell.CellContents;
                                    if (!toCell.TryPushContents(move.Direction))
                                    {
                                        throw new SokobanException("Unable to push contents.");
                                    }
                                }
                                if (!toCell.TrySetContents(this))
                                {
                                    throw new SokobanException("Unable to follow route.");
                                }
                                MoveCount++;
                            }
                            /* Set the undo item. */
                            Push newMove = new Push(pathFinder.Route) { Undo = true };
                            moves.Push(newMove);
                            result = true;
                        }
                    }
                    #endregion
                };
                ThreadPool.QueueUserWorkItem(callback);
            }
            return result;
        }
    }
}
