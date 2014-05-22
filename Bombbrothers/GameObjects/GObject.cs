using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bombbrothers.Additional;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects
{
    /// <summary>
    ///     Abstrakcyjna klasa bazowa GObject dla wszystkich obiektów w grze. Dziedziczy po abstrakcyjnej klasie Shape oraz
    ///     implementuje interfejs INotifyPropertyChanged.
    /// </summary>
    public abstract class GObject : Shape, INotifyPropertyChanged
    {
        /// <summary>
        ///     Wartość int reprezentująca typ 'ławka'.
        /// </summary>
        protected const int Bench = GameConst.TypeBench;

        /// <summary>
        ///     Wartość int reprezentująca typ 'płyta gipsowa'.
        /// </summary>
        protected const int Gypsum = GameConst.TypeGypsum;

        /// <summary>
        ///     Wartość int reprezentująca typ 'tło'.
        /// </summary>
        protected const int Background = GameConst.TypeBackground;

        /// <summary>
        ///     Wartość int reprezentująca typ 'element nośny'.
        /// </summary>
        protected const int Block = GameConst.TypeBlock;

        /// <summary>
        ///     Wartość int reprezentująca rozmiar obiektu Tile.
        /// </summary>
        protected const int TileSize = GameConst.TileSize;

        /// <summary>
        ///     Wartość int reprezentująca rozmiar obiektu gracza lub wroga.
        /// </summary>
        protected const int ObjectSize = GameConst.ObjectSize;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: prawo.
        /// </summary>
        protected const byte Right = GameConst.Right;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: dół.
        /// </summary>
        protected const byte Down = GameConst.Down;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: lewo.
        /// </summary>
        protected const byte Left = GameConst.Left;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: góra.
        /// </summary>
        protected const byte Up = GameConst.Up;

        /// <summary>
        ///     Położenie obiektu w kolumnie/ach aktualnej planszy.
        /// </summary>
        protected List<int> ActualColumns;

        /// <summary>
        ///     Położenie obiektu w wierszu/ach aktualnej planszy.
        /// </summary>
        protected List<int> ActualRows;

        /// <summary>
        ///     Poziom trudności gry wybrany przez użytkownika.
        /// </summary>
        protected Enums.LevelDifficulty Difficulty;

        private double _posX;
        private double _posY;

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        protected GObject()
        {
            ActualColumns = new List<int>();
            ActualRows = new List<int>();
            Width = GameConst.ObjectSize;
            Height = GameConst.ObjectSize;
            Difficulty = GameParameters.Difficulty;
        }

        /// <summary>
        ///     Położenie X obiektu w aktualnym canvas'ie. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public double X
        {
            get { return _posX; }
            set
            {
                _posX = value;
                OnPropertyChanged("X");
            }
        }

        /// <summary>
        ///     Położenie Y obiektu w aktualnym canvas'ie. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public double Y
        {
            get { return _posY; }
            set
            {
                _posY = value;
                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        ///     Implementacja klasy bazowej Shape.
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                var geometry = new StreamGeometry {FillRule = FillRule.EvenOdd};

                using (var context = geometry.Open())
                {
                    var pt1 = new Point(0, 0);
                    var pt2 = new Point(0, Height);
                    var pt3 = new Point(Width, 0);
                    var pt4 = new Point(Width, Height);

                    context.BeginFigure(pt1, true, false);
                    context.LineTo(pt2, true, true);
                    context.LineTo(pt3, true, true);
                    context.BeginFigure(pt4, true, false);
                    context.LineTo(pt2, true, true);
                    context.LineTo(pt3, true, true);
                }
                geometry.Freeze();

                return geometry;
            }
        }

        /// <summary>
        /// Uchwyt do zdarzenia PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Uruchamia pętlę gry dla obiektu.
        /// </summary>
        protected void StartTimer()
        {
            CompositionTarget.Rendering += ObjectLoop;
        }

        /// <summary>
        ///     Zatrzymuje pętlę gry dla obiektu.
        /// </summary>
        protected void StopTimer()
        {
            CompositionTarget.Rendering -= ObjectLoop;
        }

        /// <summary>
        ///     Metoda używana przez pętlę gry obiektu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ObjectLoop(object sender, EventArgs e)
        {
            Draw();
        }

        /// <summary>
        ///     Tworzy ImageBrush z podanego źródła typu string.
        /// </summary>
        /// <param name="source">Źródło bitmapy.</param>
        /// <returns></returns>
        protected ImageBrush CreateImageBrush(string source)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(source, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            return new ImageBrush(bi);
        }

        /// <summary>
        ///     Sprawdza czy dane współrzędne są położone w środku prostokąta danego obiektu. Wersja do kolizji gracza z wrogiem.
        /// </summary>
        /// <param name="x">Współrzędna x.</param>
        /// <param name="y">Współrzędna y.</param>
        /// <returns></returns>
        protected bool IsInRange(double x, double y)
        {
            return x >= X && (x <= X + ObjectSize) && (y <= Y + ObjectSize) && y >= Y;
        }

        /// <summary>
        ///     Sprawdza czy dane współrzędne są położone w środku prostokąta danego obiektu. Wersja do kolizji z bombą.
        /// </summary>
        /// <param name="x">Współrzędna x.</param>
        /// <param name="y">Współrzędna y.</param>
        /// <returns></returns>
        protected bool IsInRange2(double x, double y)
        {
            var xn = x*TileSize;
            var yn = y*TileSize;
            return X >= xn && (X <= xn + TileSize) && (Y <= yn + TileSize) && Y >= yn;
        }

        /// <summary>
        ///     Wyświetla obiekt w aktualnym canvas'ie.
        /// </summary>
        public virtual void Draw()
        {
            Canvas.SetLeft(this, X);
            Canvas.SetTop(this, Y);
        }

        /// <summary>
        ///     Implementacja interfejsu INotifyPropertyChanged
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        ///     Sprawdza kolizję obiektu wywołującego tą metodą z bombą.
        /// </summary>
        /// <param name="func">Funkcja podana przez obiekt wywołujący.</param>
        /// <param name="firingRange">Zasięg rażenia bomby.</param>
        protected void CheckForBombCollision(Func<List<int>, int> func, int firingRange)
        {
            var x = (int) GameParameters.ActualBomb.X/TileSize;
            var y = (int) GameParameters.ActualBomb.Y/TileSize;
            var count = 0;
            var type = 0;

            while (count <= firingRange && type != Block)
            {
                if (func(new List<int> {type, y, x - count}) == 0)
                    return;

                count++;

                if (x - count < 0)
                    break;

                type = GameParameters.ActualBoard.GetTile(y, x - count).Type;
            }

            count = 0;
            type = 0;
            while (count <= firingRange && type != Block)
            {
                if (func(new List<int> {type, y, x + count}) == 0)
                    return;

                count++;

                if (x + count == GameConst.MapColumns)
                    break;

                type = GameParameters.ActualBoard.GetTile(y, x + count).Type;
            }

            count = 0;
            type = 0;
            while (count <= firingRange && type != Block)
            {
                if (func(new List<int> {type, y - count, x}) == 0)
                    return;

                count++;

                if (y - count < 0)
                    break;

                type = GameParameters.ActualBoard.GetTile(y - count, x).Type;
            }

            count = 0;
            type = 0;
            while (count <= firingRange && type != Block)
            {
                if (func(new List<int> {type, y + count, x}) == 0)
                    return;

                count++;

                if (y + count > GameConst.MapRows)
                    break;

                type = GameParameters.ActualBoard.GetTile(y + count, x).Type;
            }
        }
    }
}