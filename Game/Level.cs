using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MGame
{
    public class Level
    {
        private List<GoalCell> goals;
        Cell[][] cells;
        //private Cell[][] cells
        //{
        //    get => default(Cell);
        //    set
        //    {
        //    }
        //}
        public Cell this[int rowNumber, int columnNumber]
        {
            get
            {
                return cells[rowNumber][columnNumber];
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

        public Cell Cells
        {
            get => default(Cell);
            set
            {
            }
        }

        public void load(TextReader mapStream)
        {
            throw new System.NotImplementedException();
        }

        private List<Cell> BuikdCells(string rowText, int rowNumber)
        {
            throw new System.NotImplementedException();
        }

        private void GoalCell_CompletedGoalChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public Level (Game game,int levelNumber)
        {
            Game = game;
            LevelNumber= levelNumber;
        }
    }
}