using Bombbrothers.Logic;
using System;

using System.Windows.Controls;
using Bombbrothers.GameObjects.Map;
using Bombbrothers.Additional;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Input;
using PlayerType = Bombbrothers.Logic.Enums.PlayerType;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Bombbrothers.GameObjects.Players;
using Bombbrothers.GameObjects.Enemies;
using System.Windows.Shapes;
using System.Xml;
using System.Threading;
using System.Diagnostics;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : UserControl, INavigable
    {
        private MapBoard Board { get; set; }

        private Player player { get; set; }

        private List<Enemy> Enemies { get; set; }
    

        public Game()
        {
            InitializeComponent();
            PrepareGame();
        }


        private void PrepareGame()
        {
            Board = new MapBoard();
            GameParameters.ActualBoard = Board;
            CreateGrid();

            Enemies = new List<Enemy>();
            Enemies.Add(new Human());
            Enemies.Add(new Matphys());
            Enemies.Add(new Human());
            GameParameters.Enemies = Enemies;

            switch (GameParameters.PlayerType)
            {
                case PlayerType.Agile:
                    player = new AgileBro();
                    break;
                case PlayerType.Fast:
                    player = new FastBro();
                    break;
                case PlayerType.Shy:
                    player = new ShyBro();
                    break;
                default:
                    player = new AgileBro();
                    break;
            }
            
            Info.DataContext = player;
            Field.Width = GameConst.Width;
            Field.Height = GameConst.Height;

            Field.Children.Add(player);

            foreach (var e in Enemies)
            {
                e.PostInitialize();
                Field.Children.Add(e);
            }

            SetGameParameters();
        }

        private void SetGameParameters()
        {
            GameParameters.ActualCanvas = Field;
            GameParameters.ID = 0;
            GameParameters.ActualPlayer = player;
            GameParameters.Enemies = Enemies;
        }

        private void CreateGrid()
        {
            Grid grid = new Grid();
            grid.Width = GameConst.Width;
            grid.Height = GameConst.Height;
            grid.ShowGridLines = true;

            int rows = Board.GetMapSize()[0];
            int columns = Board.GetMapSize()[1];

            for (int i = 0; i < rows; ++i)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < columns; ++i)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    Grid.SetRow(Board.GetTile(i,j), i);
                    Grid.SetColumn(Board.GetTile(i, j), j);
                    grid.Children.Add(Board.GetTile(i, j));
                }
            }
        
            Field.Children.Add(grid);
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            this.Focus();
        }

    }
}
