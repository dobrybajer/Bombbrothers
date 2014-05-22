using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using Bombbrothers.Additional;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    ///     Klasa Bomb reprezentująca bombą rzucaną przez gracza. Dziedziczy po abstrakcyjnej klasie GObject.
    /// </summary>
    public class Bomb : GObject
    {
        /// <summary>
        ///     Odlicza czas od postawienia bomby, do jej ekslozji (3 sek).
        /// </summary>
        private readonly DispatcherTimer _boomTimer;

        /// <summary>
        ///     Lista obiektów typu Rectangle, które są wyświetlane po wybuchu bomby.
        /// </summary>
        private readonly List<Rectangle> _explosions;

        /// <summary>
        ///     Odlicza czas trwania wyświetlania Explosions (1 sek).
        /// </summary>
        private readonly DispatcherTimer _stoper;

        /// <summary>
        ///     Zasięg rażenia danej bomby.
        /// </summary>
        private int _firingRange;

        private bool _isSet;

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public Bomb()
        {
            IsSet = false;

            Fill = CreateImageBrush(GameConst.Bomb);

            _boomTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            _boomTimer.Tick += BoomTimer_Tick;

            _stoper = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };
            _stoper.Tick += ExplosionTimer_Tick;

            _explosions = new List<Rectangle>();
        }

        /// <summary>
        ///     Właściwość aktywująca metodę OnPropertyChanged. Informuje o tym czy dana bomba jest w trakcie wybuchania.
        /// </summary>
        public bool IsSet
        {
            get { return _isSet; }
            set
            {
                _isSet = value;
                OnPropertyChanged("IsSet");
            }
        }

        /// <summary>
        ///     Zdarzenie, które jest wywoływane po wybuchu bomby.
        /// </summary>
        public event EventHandler BombBoom;

        /// <summary>
        ///     Metoda odpalająca bombę w danym położenia i o danym zasięgu. Uruchamia czas licznika BoomTimer.
        /// </summary>
        /// <param name="x">Położenie x bomby.</param>
        /// <param name="y">Położenie y bomby.</param>
        /// <param name="range">Zasięg rażenia bomby.</param>
        public void SetBomb(int x, int y, int range)
        {
            IsSet = true;

            X = x*TileSize;
            Y = y*TileSize;

            _firingRange = range;

            GameParameters.ActualCanvas.Children.Add(this);
            Draw();

            _boomTimer.Start();
        }

        /// <summary>
        ///     Tworzy i dodaje do kolekcji Children aktualnego canvas'u obiekt typu Rectangle.
        /// </summary>
        /// <returns>Instancja obiektu rectangle ustawionego w konkretnym położeniu.</returns>
        private Rectangle CreateExplosion()
        {
            var rect = new Rectangle {Width = TileSize, Height = TileSize, Fill = CreateImageBrush(GameConst.Explosion)};

            _explosions.Add(rect);

            GameParameters.ActualCanvas.Children.Add(rect);

            return rect;
        }

        /// <summary>
        ///     Wyświetla obiekt w danym położeniu na aktualnym canvas'ie.
        /// </summary>
        /// <param name="rect">Obiekt do wyświetlenia.</param>
        /// <param name="x">Położenie x obiektu.</param>
        /// <param name="y">Położenie y obiektu.</param>
        private static void ShowExplosion(UIElement rect, double x, double y)
        {
            Canvas.SetLeft(rect, x*TileSize);
            Canvas.SetTop(rect, y*TileSize);
        }

        /// <summary>
        ///     Usuwa wszyskie obiekty znajdujące się w liście Explosions z kolekcji Children danego canvas'u, oraz czyści kolekcję
        ///     Explosions.
        /// </summary>
        private void HideExplosions()
        {
            foreach (var e in _explosions)
                GameParameters.ActualCanvas.Children.Remove(e);

            _explosions.Clear();
        }

        /// <summary>
        ///     Metoda uruchamiana po upłynięciu czasu (3 sek) podanego w BoomTimer. Sprawdza kolizję z otoczeniem, usuwa bombę z
        ///     aktualnego canvas'u oraz uruchamia czas wyświetlania elementów z kolekcji Explosions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoomTimer_Tick(object sender, EventArgs e)
        {
            if (BombBoom != null)
                BombBoom(this, new EventArgs());

            CheckCollision();

            GameParameters.ActualCanvas.Children.Remove(this);
            _boomTimer.Stop();
            _stoper.Start();
        }

        /// <summary>
        ///     Metoda uruchamiana po upłynięciu czasu (1 sek) podanego w Stoper. Usuwa obiekty z aktualnego canvas'u oraz ustawia
        ///     właściwość IsSet na false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            HideExplosions();
            IsSet = false;
            _stoper.Stop();
        }

        /// <summary>
        ///     Sprawdza kolizję zasięgu rażenia bomby z elementami nietrwałymi otoczenia.
        /// </summary>
        private void CheckCollision()
        {
            CheckForBombCollision(x =>
            {
                if (x[0] == Bench || x[0] == Gypsum)
                    GameParameters.ActualBoard.ChangeTypeOfTile(x[1], x[2], Background);

                ShowExplosion(CreateExplosion(), x[2], x[1]);

                return -1;
            }, _firingRange);
        }
    }
}