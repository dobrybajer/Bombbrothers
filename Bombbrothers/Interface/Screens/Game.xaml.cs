using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Bombbrothers.Additional;
using Bombbrothers.GameObjects.Enemies;
using Bombbrothers.GameObjects.Map;
using Bombbrothers.GameObjects.Players;
using Bombbrothers.Logic;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki Game.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class Game : INavigable
    {
        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public Game()
        {
            InitializeComponent();
            PrepareGame();
        }

        /// <summary>
        ///     Implementacja interfejsu INavigable. Wywoływane, gdy kontrolka została załadowana z przekazaniem parametru.
        /// </summary>
        /// <param name="state">Obiekt przekazany kontrolce.</param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Przygotowanie programu. Stworzenie odpowiednich elementów gry takich jak: gracz, wrogowie, plansze etc.
        /// </summary>
        private void PrepareGame()
        {
            var board = new MapBoard();
            GameParameters.ActualBoard = board;
            CreateGrid(board);

            var enemies = new List<Enemy> {new Human(), new Matphys(), new Human()};
            GameParameters.Enemies = enemies;

            Player player;
            switch (GameParameters.PlayerType)
            {
                case Enums.PlayerType.Agile:
                    player = new AgileBro();
                    break;
                case Enums.PlayerType.Fast:
                    player = new FastBro();
                    break;
                case Enums.PlayerType.Shy:
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

            foreach (Enemy e in enemies)
            {
                e.PostInitialize();
                Field.Children.Add(e);
            }

            GameParameters.ActualCanvas = Field;
            GameParameters.Id = 0;
            GameParameters.ActualPlayer = player;
            GameParameters.Enemies = enemies;
        }

        /// <summary>
        ///     Tworzy panel Grid na podstawie kolekcji Tile[,] zawartej w MapBoard. Stworzony panel dodaje do aktualnego canvas.
        /// </summary>
        /// <param name="board">Obiekt zawierający kolekcję Tile[,].</param>
        private void CreateGrid(MapBoard board)
        {
            var grid = new Grid {Width = GameConst.Width, Height = GameConst.Height};

            int rows = board.GetMapSize()[0];
            int columns = board.GetMapSize()[1];

            for (int i = 0; i < rows; ++i)
            {
                grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(1, GridUnitType.Star)});
            }

            for (int i = 0; i < columns; ++i)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
            }

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    Grid.SetRow(board.GetTile(i, j), i);
                    Grid.SetColumn(board.GetTile(i, j), j);
                    grid.Children.Add(board.GetTile(i, j));
                }
            }

            Field.Children.Add(grid);
        }
    }
}