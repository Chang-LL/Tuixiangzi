using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        string levelDirectory = @"..\..\..\Levels\";



        public Game()
        {
            
        }

        public Game(ISokobanService sokobanService)
        {
           this.sokobanService = sokobanService;
        }

        public GameState GameState
        {
            get { return gameState; }

            private  set
            {
                gameState = value;
                OnPropertyChanged("GameState");
            }
        }

        public string LevelCode
        {
            get
            {
                if (Level == null)//判断Level对象是否存在
                {
                    return string.Empty;
                }//根据指定的LevelNumber返回LevelCode
                return MGame.LevelCode.GetLevelCode(Level.LevelNumber);
            }
        }
        event PropertyChangedEventHandler propertyChanged;


        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged += value;
            }
            remove
            {
                propertyChanged -= value;
            }
        }
        public int LevelCount
        {
            get;
            private set;
        }

        public Level Level
        {
            get ;
            private set;
            
        }

        public void GotoNextLevel()
        {
            if (Level.LevelNumber < LevelCount)
            {  //如果还有关卡可玩则加载关卡
                LoadLevel(Level.LevelNumber + 1);
            }
        }


        public void Level_LevelCompleted(object sender, EventArgs e)
        {
            if (Level.LevelNumber < LevelCount - 1)
            {   //将游戏的状态置于LevelCompleted
                GameState = GameState.LevelCompleted;
            }
            else//如果游戏的所有关都过了
            {
                /*则整个游戏玩完 */
                GameState = GameState.GameCompleted;
            }
        }

        public void LoadLevel(int levelNumber)
        {
            GameState = GameState.Loading;//设置当前游戏的状态
            if (Level != null) //如果当前Level存在，通过关卡切换而来
            {
                /* 先取消对关卡完成事件的绑定. */
                Level.LevelCompleted -= new EventHandler(Level_LevelCompleted);
            }
            Level = new Level(this, levelNumber);//新建一个Level
            Level.LevelCompleted += new EventHandler(Level_LevelCompleted);
            string levelMap;//用于保存关卡自字符的变量
            if (sokobanService != null)
            {   //使用sokobanService来得到关卡地图字符串
                levelMap = sokobanService.GetMap(levelNumber);
                using (StringReader reader = new StringReader(levelMap))
                {   //调用Load方法加载关卡内容
                    Level.Load(reader);
                }
            }
            else
            {   //如果sokobanService没有传入，则从文件中加载关卡文件
                string fileName = string.Format(@"{0}Level{1:000}.skbn",
                    levelDirectory, levelNumber);
                using (StreamReader reader = File.OpenText(fileName))
                {  //加载关卡内容
                    Level.Load(reader);
                }
            }
            OnPropertyChanged("Level");//触发关卡属性变更通知
            StartLevel();//开始指定关卡的游戏
        }
        void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            propertyChanged?.Invoke(this, e);
        }
        protected void OnPropertyChanged(string property)
        {
            if (context != null)
            {
                context.Send(delegate
                {   //在同步上下文中触发属性变更通知
                    OnPropertyChanged(new PropertyChangedEventArgs(property));
                }, null);
            }
            else
            {   //得到当前的同步上下文对象
                context = SynchronizationContext.Current;
                if (context == null)
                {   //在当前线程上下中
                    OnPropertyChanged(new PropertyChangedEventArgs(property));
                }
                else
                {   //递归在同步上下文中触发变更
                    OnPropertyChanged(property);
                }
            }
        }

        public void RestartLevel()
        {
            throw new System.NotImplementedException();
        }
        public bool InBounds(Location location)
        {   //调用了Level的InBounds方法
            return Level.InBounds(location);
        }
        public bool TryGotoLevel(string levelCode)
        {
            if (levelCode == null)//判断LevelCode是否传入
            {
                throw new ArgumentNullException("levelCode");
            }
            int levelNumber = MGame.LevelCode.  //调用LevelCode的GetLevelNumber
                GetLevelNumber(levelCode.Trim().ToUpper());
            if (levelNumber < 0) //判断是否找到了LevelNumber
            {
                return false;
            }
            LoadLevel(levelNumber);//加载LevelNumber
            return true;
        }

        private void StartLevel()
        {
            GameState = GameState.Running;
        }

        public void Start()
        {
            if (sokobanService != null)
            {   //获取关卡数
                LevelCount = sokobanService.LevelCount;
            }
            else
            {   //得到关卡总数，通过获取关卡文件的个数来得到
                string[] files = Directory.GetFiles(levelDirectory, "*.skbn");
                LevelCount = files.Length;
            }
            LoadLevel(0);//加载关卡
        }
    }
}
