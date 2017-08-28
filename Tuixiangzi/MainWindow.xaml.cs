using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MGame;

namespace Tuixiangzi
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Game Game
        {
            get
            {
              return   (Game)TryFindResource("sokobanGame");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Game.PropertyChanged += game_PropertyChanged;
            try
            {
                Game.Start();
            }
            catch (Exception ex)
            {

                MessageBox.Show("加载游戏异常。" + ex.Message);
            }                      
        }

        private void game_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "GameState":
                    UpdateGameDisplay();
                    break;
            }
        }

        private void UpdateGameDisplay()
        {
            switch (Game.GameState)
            {
                case GameState.Loading:
                    //FeedbackControll.M
                    break;
                case GameState.Running:
                    break;
                case GameState.LevelCompleted:
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
                case GameState.GameCompleted:
                    break;
                default:
                    break;
            }
        }
    }
}
