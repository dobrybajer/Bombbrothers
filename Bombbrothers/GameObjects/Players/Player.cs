using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Bombbrothers.Additional;
using Bombbrothers.GameObjects.Bonuses;
using Bombbrothers.Interface.Screens;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    ///     Abstrakcyjna klasa reprezentująca gracza. Dziedziczy po abstrakcyjnej klasie GObject.
    /// </summary>
    internal abstract class Player : GObject
    {
        /// <summary>
        ///     Licznik czasu po trafieniu przez wroga.
        /// </summary>
        private readonly DispatcherTimer _dieTimer;

        /// <summary>
        ///     Zasięg rażenia bomby gracza.
        /// </summary>
        private readonly int _firingRange;

        /// <summary>
        ///     Prędkość gracza.
        /// </summary>
        private readonly double _speed;

        /// <summary>
        ///     Obiekt Bomb zawierający bombę dla danego gracza.
        /// </summary>
        private readonly Bomb _bomb;

        /// <summary>
        ///     Lista aktualnie zebranych bonusów.
        /// </summary>
        private List<Bonus> _actualBonuses;

        /// <summary>
        ///     Informacja czy gracz został trafiony przez wroga.
        /// </summary>
        private bool _blinking;

        /// <summary>
        ///     Informacja czy gracz podłożył bombę lub czy podłożona bomba już wybuchła.
        /// </summary>
        private bool _bombNotSetOrExploded;

        /// <summary>
        ///     Wynik zebrany podczas rozgrywki.
        /// </summary>
        private int _scores;

        private int _remainingBombs;
        private int _remainingLifes;

        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        /// <param name="type">Typ gracza.</param>
        protected Player(Enums.PlayerType type)
        {
            ActualColumns.Add(0);
            ActualRows.Add(0);
            X = Y = 0;
            _blinking = false;
            _dieTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            _dieTimer.Tick += DieTimer_Tick;

            switch (type)
            {
                case Enums.PlayerType.Agile:
                    _speed = GameConst.PlayerSpeed;
                    Fill = CreateImageBrush(GameConst.PlayerAgile);
                    break;
                case Enums.PlayerType.Fast:
                    _speed = GameConst.FastSpeed;
                    Fill = CreateImageBrush(GameConst.PlayerFast);
                    break;
                case Enums.PlayerType.Shy:
                    _speed = GameConst.PlayerSpeed;
                    Fill = CreateImageBrush(GameConst.PlayerShy);
                    break;
            }
            switch (Difficulty)
            {
                case Enums.LevelDifficulty.Easy:
                    RemainingLifes = GameConst.EasyLifes;
                    break;
                case Enums.LevelDifficulty.Medium:
                    RemainingLifes = GameConst.MediumLifes;
                    break;
                case Enums.LevelDifficulty.Hard:
                    RemainingLifes = GameConst.HardLifes;
                    break;
            }
            RemainingBombs = GameConst.BombCount;
            _firingRange = GameConst.PlayerFiringRange;
            _scores = 0;
            _actualBonuses = new List<Bonus>();

            StartTimer();

            _bombNotSetOrExploded = true;

            _bomb = new Bomb();
            GameParameters.ActualBomb = _bomb;
            _bomb.BombBoom += Boom;
        }

        /// <summary>
        ///     Ilość pozostałych żyć gracza. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public int RemainingLifes
        {
            get { return _remainingLifes; }
            set
            {
                _remainingLifes = value;
                OnPropertyChanged("RemainingLifes");
            }
        }

        /// <summary>
        ///     Ilość pozostałych bomb gracza. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public int RemainingBombs
        {
            get { return _remainingBombs; }
            set
            {
                _remainingBombs = value;
                OnPropertyChanged("RemainingBombs");
            }
        }

        /// <summary>
        ///     Metoda wywoływana po nadejściu zdarzenia o wybuchu bomby. Sprawdza czy gracz nie był danym momencie w zasięgu
        ///     rażenia bomby oraz zmienia zmienną BombNotSetOrExploded na true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boom(object sender, EventArgs e)
        {
            CheckBombCollision();
            _bombNotSetOrExploded = true;
        }

        /// <summary>
        ///     Sprawdza czy gracz nie jest w zasięgu rażenia bomby. Jeśli tak, zabiera jedno życie.
        /// </summary>
        private void CheckBombCollision()
        {
            CheckForBombCollision(x =>
            {
                if (IsInRange2(x[1], x[2]))
                {
                    Die();
                    return 0;
                }

                return -1;
            }, _firingRange);
        }

        /// <summary>
        ///     Metoda wywoływana po upływie czasu podanego w DieTimer. Kończy czas ochronny dla gracza po utracie życia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DieTimer_Tick(object sender, EventArgs e)
        {
            _blinking = false;
            _dieTimer.Stop();
        }

        /// <summary>
        ///     Pętla gry dla gracza. Porusza graczem, rzuca bombę, sprawdza kolizję z przecinikami, uaktualnia obiekt oraz rysuje
        ///     gracza na aktualnym canvas'ie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ObjectLoop(object sender, EventArgs e)
        {
            base.ObjectLoop(sender, e);
            var moves = GameParameters.ActualBoard.GetValidMoves(ActualRows, ActualColumns);
            Move(moves);
            DropBomb();
            CheckForEnemyCollision();
            Update();
        }

        /// <summary>
        ///     Porusza graczem uwzględniając możliwe kierunki ruchu zawarte w parametrze wejściowym.
        /// </summary>
        /// <param name="moves">Poprawne kierunki ruchu.</param>
        protected virtual void Move(byte moves)
        {
            if (ActualRows.Count != 0)
                ActualRows.Clear();

            if (ActualColumns.Count != 0)
                ActualColumns.Clear();

            if (Keyboard.IsKeyDown(Key.Left) && (moves & Left) == Left)
                X -= _speed;

            else if (Keyboard.IsKeyDown(Key.Right) && (moves & Right) == Right)
                X += _speed;

            else if (Keyboard.IsKeyDown(Key.Up) && (moves & Up) == Up)
                Y -= _speed;

            else if (Keyboard.IsKeyDown(Key.Down) && (moves & Down) == Down)
                Y += _speed;

            ActualRows.Add((int) (Y/TileSize));
            if ((int) ((Y + ObjectSize)/TileSize) != ActualRows.FirstOrDefault())
                ActualRows.Add((int) ((Y + ObjectSize)/TileSize));

            ActualColumns.Add((int) (X/TileSize));
            if ((int) ((X + ObjectSize)/TileSize) != ActualColumns.FirstOrDefault())
                ActualColumns.Add((int) ((X + ObjectSize)/TileSize));
        }

        /// <summary>
        ///     Uaktualnia statyczną klasę GameParameters informacjami o aktualnym graczu.
        /// </summary>
        protected void Update()
        {
            GameParameters.ActualPlayer = this;
        }

        /// <summary>
        ///     Animacja czasu ochronnego po utracie życia przez gracza.
        /// </summary>
        private void BlinkingAnimation()
        {
            var da = new DoubleAnimation
            {
                From = 1,
                To = 0.3,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            da.RepeatBehavior = new RepeatBehavior(4);
            BeginAnimation(OpacityProperty, da);
        }

        /// <summary>
        ///     Sprawdza czy nie nastąpiła kolizja z dowolnym przeciwnikiem.
        /// </summary>
        private void CheckForEnemyCollision()
        {
            foreach (var e in GameParameters.Enemies.Where(e => !e.IsDead && 
                (IsInRange(e.X, e.Y) ||                                                          
                IsInRange(e.X + ObjectSize, e.Y + ObjectSize) ||                                                         
                IsInRange(e.X + ObjectSize, e.Y) ||                                                      
                IsInRange(e.X, e.Y + ObjectSize)) && !_blinking))
            {
                Die();
            }
        }

        /// <summary>
        ///     Stawia na planszy bombę.
        /// </summary>
        protected virtual void DropBomb()
        {
            if (!Keyboard.IsKeyDown(Key.Space) || RemainingBombs <= 0 || !_bombNotSetOrExploded) return;
            _bombNotSetOrExploded = false;
            _bomb.SetBomb(ActualColumns.FirstOrDefault(), ActualRows.FirstOrDefault(), _firingRange);
            RemainingBombs -= 1;
        }

        /// <summary>
        ///     Metoda wywoływana po wykryciu kolizji z przeciwnikiem lub bombą.
        /// </summary>
        private void Die()
        {
            _blinking = true;
            _dieTimer.Start();
            BlinkingAnimation();

            RemainingLifes -= 1;

            if (RemainingLifes == 0)
                GameOver();
        }

        /// <summary>
        ///     Kończy grę informując użytkownika stosownym komunikatem.
        /// </summary>
        private static void GameOver()
        {
            const string msgtext = "Do you want to play again?";
            const string txt = "You lose!";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            var result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    NavigationController.NavigateTo(new NewGame());
                    break;
                case MessageBoxResult.No:
                    NavigationController.NavigateTo(new Menu());
                    break;
            }
        }

        /// <summary>
        ///     Kończy poziom informując użytkownika stosownym komunikatem.
        /// </summary>
        private void LevelCompleted()
        {
            const string msgtext = "Do you want to play next level?";
            const string txt = "Level completed!";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            var result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    NavigationController.NavigateTo(new NewGame());
                    break;
                case MessageBoxResult.No:
                    NavigationController.NavigateTo(new Menu());
                    break;
            }
        }
    }
}