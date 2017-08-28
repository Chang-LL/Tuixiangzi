using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame.PathFinders
{
    class PushPathFinder
    {
        readonly Location destination;
        readonly Cell startCell;
        readonly Level level;
        readonly List<Move> moves = new List<Move>();
        bool treasurePushed;
        Move[] route;

        /// <summary>
        /// Gets the route, or set of steps that map out
        /// the relocation. May be null.
        /// </summary>
        /// <value>The route that an <see cref="Actor"/>
        /// should take to move to destination. May be null.</value>
        public Move[] Route
        {
            get
            {
                return route;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPathFinder"/> class.
        /// </summary>
        /// <param name="start">The start cell. Presumably where
        /// the <see cref="Actor"/> is located.</param>
        /// <param name="destination">Where we would like to get.</param>
        public PushPathFinder(Cell start, Location destination)
        {
            this.destination = destination;
            startCell = start;
            level = start.Level;
        }

        /// <summary>
        /// Tries to find a route to the destination, where the route 
        /// may contain one treasure to be pushed.
        /// </summary>
        /// <returns><code>true</code> if a route was found, 
        /// <code>false</code> otherwise.</returns>
        public bool TryFindPath()
        {
            Location current = startCell.Location;
            if (current.ColumnNumber != destination.ColumnNumber && current.RowNumber != destination.RowNumber)
            {
                return false;
            }
            moves.Clear();

            bool horizontal = current.RowNumber == destination.RowNumber;

            /* We loop through cells in the path. 
			 If a cell in the path contains a treasure, it will be pushed.
			 If there are more than one treasure in the path
			 the push will fail, i.e. return false. */
            if (horizontal)
            {
                int rowNumber = current.RowNumber;

                if (current.ColumnNumber < destination.ColumnNumber)
                {
                    for (int i = current.ColumnNumber + 1; i <= destination.ColumnNumber; i++)
                    {
                        Cell cell = level[rowNumber, i];
                        if (!Enter(cell, Direction.Right))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = current.ColumnNumber - 1; i >= destination.ColumnNumber; i--)
                    {
                        Cell cell = level[rowNumber, i];
                        if (!Enter(cell, Direction.Left))
                        {
                            return false;
                        }
                    }
                }
            }
            else /* Vertical path. */
            {
                int columnNumber = current.ColumnNumber;

                if (current.RowNumber < destination.RowNumber)
                {
                    for (int i = current.RowNumber + 1; i <= destination.RowNumber; i++)
                    {
                        Cell cell = level[i, columnNumber];
                        if (!Enter(cell, Direction.Down))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = current.RowNumber - 1; i >= destination.RowNumber; i--)
                    {
                        Cell cell = level[i, columnNumber];
                        if (!Enter(cell, Direction.Up))
                        {
                            return false;
                        }
                    }
                }
            }

            route = moves.ToArray();
            moves.Clear();
            return true;
        }

        /// <summary>
        /// Tests whether we can enter the specified cell
        /// from the specified direction.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="fromDirection">From direction.</param>
        /// <returns></returns>
        bool Enter(Cell cell, Direction fromDirection)
        {
            bool result = false;
            if (cell.CanEnter)
            {
                result = true;
            }
            else if (!treasurePushed && cell.CellContents is Treasure)
            {
                treasurePushed = true;
                result = true;
            }

            /* If we were successful and this isn't the last cell where 
			 we have previously pushed a treasure, then we add it to moves. 
			 The reason we check for the last cell and treasurePushed
			 is because we want the treasure to be located at the destination
			 at the end of the move. */
            if (result && !(cell.Location == destination && treasurePushed))
            {
                Move move = new Move(fromDirection);
                moves.Add(move);
            }

            return result;
        }
    }
}

