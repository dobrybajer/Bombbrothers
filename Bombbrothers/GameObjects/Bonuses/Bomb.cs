using Bombbrothers.Additional;
using Bombbrothers.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TileType = Bombbrothers.Logic.Enums.TileType;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    /// Klasa Bomb reprezentująca bombą rzucaną przez gracza. Dziedziczy po abstrakcyjnej klasie GObject.
    /// </summary>
    public class Bomb : GObject
    {
        /// <summary>
        /// Lista obiektów typu Rectangle, które są wyświetlane po wybuchu bomby.
        /// </summary>
        private List<Rectangle> Explosions;

        /// <summary>
        /// Zdarzenie, które jest wywoływane po wybuchu bomby.
        /// </summary>
        public event EventHandler BombBoom;

        /// <summary>
        /// Odlicza czas trwania wyświetlania Explosions (1 sek).
        /// </summary>
        private DispatcherTimer Stoper;

        /// <summary>
        /// Odlicza czas od postawienia bomby, do jej ekslozji (3 sek).
        /// </summary>
        private DispatcherTimer BoomTimer;

        /// <summary>
        /// Zasięg rażenia danej bomby.
        /// </summary>
        private int FiringRange;

        private bool _isSet;
        /// <summary>
        /// Właściwość aktywująca metodę OnPropertyChanged. Informuje o tym czy dana bomba jest w trakcie wybuchania.
        /// </summary>
        public bool IsSet
        {
            get
            {
                return _isSet;
            }
            set
            {
                _isSet = value;
                OnPropertyChanged("IsSet");
            }
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Bomb()
        {
            IsSet = false;

            Fill = CreateImageBrush(GameConst.Bomb);

            BoomTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            BoomTimer.Tick += BoomTimer_Tick;

            Stoper = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1),
            };
            Stoper.Tick += ExplosionTimer_Tick;

            Explosions = new List<Rectangle>();
        }

        /// <summary>
        /// Metoda odpalająca bombę w danym położenia i o danym zasięgu. Uruchamia czas licznika BoomTimer.
        /// </summary>
        /// <param name="x">Położenie x bomby.</param>
        /// <param name="y">Położenie y bomby.</param>
        /// <param name="range">Zasięg rażenia bomby.</param>
        public void SetBomb(int x, int y, int range)
        {
            IsSet = true;

            X = x * TileSize;
            Y = y * TileSize;

            FiringRange = range;

            GameParameters.ActualCanvas.Children.Add(this);
            Draw();

            BoomTimer.Start();
        }

        /// <summary>
        /// Tworzy i dodaje do kolekcji Children aktualnego canvas'u obiekt typu Rectangle.
        /// </summary>
        /// <returns>Instancja obiektu rectangle ustawionego w konkretnym położeniu.</returns>
        private Rectangle CreateExplosion()
        {
            Rectangle rect = new Rectangle() { Width = TileSize, Height = TileSize, Fill = CreateImageBrush(GameConst.Explosion)};

            Explosions.Add(rect);

            GameParameters.ActualCanvas.Children.Add(rect);

            return rect;
        }

        /// <summary>
        /// Wyświetla obiekt w danym położeniu na aktualnym canvas'ie.
        /// </summary>
        /// <param name="rect">Obiekt do wyświetlenia.</param>
        /// <param name="x">Położenie x obiektu.</param>
        /// <param name="y">Położenie y obiektu.</param>
        private void ShowExplosion(Rectangle rect, double x, double y)
        {
            Canvas.SetLeft(rect, x * TileSize);
            Canvas.SetTop(rect, y * TileSize);
        }

        /// <summary>
        /// Usuwa wszyskie obiekty znajdujące się w liście Explosions z kolekcji Children danego canvas'u, oraz czyści kolekcję Explosions.
        /// </summary>
        private void HideExplosions()
        {
            foreach(var e in Explosions)
                GameParameters.ActualCanvas.Children.Remove(e);

            Explosions.Clear();
        }

        /// <summary>
        /// Metoda uruchamiana po upłynięciu czasu (3 sek) podanego w BoomTimer. Sprawdza kolizję z otoczeniem, usuwa bombę z aktualnego canvas'u oraz uruchamia czas wyświetlania elementów z kolekcji Explosions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoomTimer_Tick(object sender, EventArgs e)
        {
            if (this.BombBoom != null)
                this.BombBoom(this, new EventArgs());

            CheckCollision();
            
            GameParameters.ActualCanvas.Children.Remove(this);
            BoomTimer.Stop();
            Stoper.Start();
        }

        /// <summary>
        /// Metoda uruchamiana po upłynięciu czasu (1 sek) podanego w Stoper. Usuwa obiekty z aktualnego canvas'u oraz ustawia właściwość IsSet na false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            HideExplosions();
            IsSet = false;
            Stoper.Stop();
        }

        /// <summary>
        /// Sprawdza kolizję zasięgu rażenia bomby z elementami nietrwałymi otoczenia.
        /// </summary>
        private void CheckCollision()
        {
            CheckForBombCollision(x =>
            {
                if(x[0] == Bench || x[0] == Gypsum)
                    GameParameters.ActualBoard.ChangeTypeOfTile(x[1], x[2], Background);

                ShowExplosion(CreateExplosion(), x[2], x[1]);

                return -1;
            }, FiringRange);

        }
    }
}
