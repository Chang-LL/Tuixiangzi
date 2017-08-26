using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MGame
{
    public class Game :INotifyPropertyChanged
    {
        ISokobanService sokobanService;
        SynchronizationContext context =
            SynchronizationContext.Current;
        private GameState gameState;
        private int levelDirectory;

        public event EventHandler propertyChanged;
        public event EventHandler PropertyChanged;

        public Game()
        {
            
        }

        public Game(ISokobanService sokobanService)
        {
            this.sobanService = sobanService;
        }

        public GameState GameState
        {
            get { return gameState; }

            private  set
            {
                gameState = value;
                OnpropertyChanged("GameState");
            }
        }

        public string LevelCode
        {
            get { throw new Exception(); }
            set
            {
            }
        }

        public int LevelCount
        {
            get => default(int);
            set
            {
            }
        }

        public ISokobanService sobanService
        {
            get => default(MGame.ISokobanService);
            set
            {
            }
        }

        public Level Level
        {
            get ;
            private set;
            
        }

        public void GotoNextLevel()
        {
            throw new System.NotImplementedException();
        }

        public void InBounds()
        {
            throw new System.NotImplementedException();
        }

        public void Level_LevelCompleted(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void LoadLevel(int levelNumber)
        {
            throw new System.NotImplementedException();
        }

        protected void OnpropertyChanged(string peoperty)
        {
            throw new System.NotImplementedException();
        }

        public void RestartLevel()
        {
            throw new System.NotImplementedException();
        }

        public bool TryGotoLevel(string levelCode)
        {
            throw new System.NotImplementedException();
        }

        private void StartLevel()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}
