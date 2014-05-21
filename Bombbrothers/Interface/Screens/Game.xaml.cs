using Bombbrothers.Logic;
using System;
using System.Windows.Controls;
using Bombbrothers.GameObjects.Map;
using Bombbrothers.Additional;
using System.Windows;
using PlayerType = Bombbrothers.Logic.Enums.PlayerType;
using System.Collections.Generic;
using Bombbrothers.GameObjects.Players;
using Bombbrothers.GameObjects.Enemies;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Logika dla kontrolki Game.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class Game : UserControl, INavigable
    { 
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Game()
        {
            InitializeComponent();
            PrepareGame();
        }

        /// <summary>
        /// Przygotowanie programu. Stworzenie odpowiednich elementów gry takich jak: gracz, wrogowie, plansze etc.
        /// </summary>
        private void PrepareGame()
        {
            MapBoard Board = new MapBoard();
            GameParameters.ActualBoard = Board;
            CreateGrid(Board);

            List<Enemy> Enemies = new List<Enemy>();
            Enemies.Add(new Human());
            Enemies.Add(new Matphys());
            Enemies.Add(new Human());
            GameParameters.Enemies = Enemies;

            Player player;
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

            GameParameters.ActualCanvas = Field;
            GameParameters.ID = 0;
            GameParameters.ActualPlayer = player;
            GameParameters.Enemies = Enemies;
        }

        /// <summary>
        /// Tworzy panel Grid na podstawie kolekcji Tile[,] zawartej w MapBoard. Stworzony panel dodaje do aktualnego canvas.
        /// </summary>
        /// <param name="Board">Obiekt zawierający kolekcję Tile[,].</param>
        private void CreateGrid(MapBoard Board)
        {
            Grid grid = new Grid();
            grid.Width = GameConst.Width;
            grid.Height = GameConst.Height;        

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

        /// <summary>
        /// Wywoływane, gdy kontrolka została załadowana z przekazaniem parametru. 
        /// </summary>
        /// <param name="state">Obiket przekazany kontrolce.</param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
