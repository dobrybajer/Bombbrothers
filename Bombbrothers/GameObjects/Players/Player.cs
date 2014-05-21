using System.Collections.Generic;
using System.Linq;
using Bombbrothers.Logic;
using PlayerType = Bombbrothers.Logic.Enums.PlayerType;
using LevelDifficulty = Bombbrothers.Logic.Enums.LevelDifficulty;
using Bombbrothers.GameObjects.Bonuses;
using System.Windows.Input;
using Bombbrothers.Additional;
using System.Windows;
using System;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Bombbrothers.Interface.Screens;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    /// Abstrakcyjna klasa reprezentująca gracza. Dziedziczy po abstrakcyjnej klasie GObject.
    /// </summary>
    abstract class Player : GObject
    {
        private int _remainingLifes;
        /// <summary>
        /// Ilość pozostałych żyć gracza. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public int RemainingLifes
        {
            get
            {
                return _remainingLifes;
            }
            set
            {
                _remainingLifes = value;
                OnPropertyChanged("RemainingLifes");
            }
        }

        private int _remainingBombs;
        /// <summary>
        /// Ilość pozostałych bomb gracza. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public int RemainingBombs
        {
            get
            {
                return _remainingBombs;
            }
            set
            {
                _remainingBombs = value;
                OnPropertyChanged("RemainingBombs");
            }
        }

        /// <summary>
        /// Prędkość gracza.
        /// </summary>
        private double Speed;

        /// <summary>
        /// Zasięg rażenia bomby gracza.
        /// </summary>
        private int FiringRange;

        /// <summary>
        /// Typ enum gracza.
        /// </summary>
        private PlayerType Type;

        /// <summary>
        /// Wynik zebrany podczas rozgrywki.
        /// </summary>
        private int Scores;

        /// <summary>
        /// Lista aktualnie zebranych bonusów.
        /// </summary>
        private List<Bonus> ActualBonuses;

        /// <summary>
        /// Informacja czy gracz został trafiony przez wroga.
        /// </summary>
        private bool Blinking;

        /// <summary>
        /// Licznik czasu po trafieniu przez wroga.
        /// </summary>
        private DispatcherTimer DieTimer;

        /// <summary>
        /// Informacja czy gracz podłożył bombę lub czy podłożona bomba już wybuchła.
        /// </summary>
        private bool BombNotSetOrExploded;

        /// <summary>
        /// Obiekt Bomb zawierający bombę dla danego gracza.
        /// </summary>
        private Bomb bomb;

        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        /// <param name="type">Typ gracza.</param>
        protected Player(PlayerType type) : base()
        {
            ActualColumns.Add(0);
            ActualRows.Add(0);
            X = Y = 0;
            Type = type;
            Blinking = false;
            DieTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            DieTimer.Tick += DieTimer_Tick;

            switch(Type)
            {
                case PlayerType.Agile:
                    Speed = GameConst.PlayerSpeed;
                    Fill = CreateImageBrush(GameConst.PlayerAgile);
                    break;
                case PlayerType.Fast:
                    Speed = GameConst.FastSpeed;
                    Fill = CreateImageBrush(GameConst.PlayerFast);
                    break;
                case PlayerType.Shy:
                    Speed = GameConst.PlayerSpeed;
                    Fill = CreateImageBrush(GameConst.PlayerShy);
                    break;
            }
            switch(Difficulty)
            {
                case LevelDifficulty.Easy:
                    RemainingLifes = GameConst.EasyLifes;
                    break;
                case LevelDifficulty.Medium:
                    RemainingLifes = GameConst.MediumLifes;
                    break;
                case LevelDifficulty.Hard:
                    RemainingLifes = GameConst.HardLifes;
                    break;

            }
            RemainingBombs = GameConst.BombCount;
            FiringRange = GameConst.PlayerFiringRange;
            Scores = 0;
            ActualBonuses = new List<Bonus>();
            
            StartTimer();

            BombNotSetOrExploded = true;

            bomb = new Bomb();
            GameParameters.ActualBomb = bomb;
            bomb.BombBoom += Boom;
        }

        /// <summary>
        /// Metoda wywoływana po nadejściu zdarzenia o wybuchu bomby. Sprawdza czy gracz nie był danym momencie w zasięgu rażenia bomby oraz zmienia zmienną BombNotSetOrExploded na true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boom(object sender, EventArgs e)
        {
            CheckBombCollision();
            BombNotSetOrExploded = true;
        }

        /// <summary>
        /// Sprawdza czy gracz nie jest w zasięgu rażenia bomby. Jeśli tak, zabiera jedno życie.
        /// </summary>
        private void CheckBombCollision()
        {
            CheckForBombCollision(x =>
            {
                if(IsInRange2((double)x[1], (double)x[2]))
                {
                    Die();
                    return 0;
                }   

                return -1;
            }, FiringRange);
        }

        /// <summary>
        /// Metoda wywoływana po upływie czasu podanego w DieTimer. Kończy czas ochronny dla gracza po utracie życia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DieTimer_Tick(object sender, EventArgs e)
        {
            Blinking = false;
            DieTimer.Stop();
        }

        /// <summary>
        /// Pętla gry dla gracza. Porusza graczem, rzuca bombę, sprawdza kolizję z przecinikami, uaktualnia obiekt oraz rysuje gracza na aktualnym canvas'ie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ObjectLoop(object sender, EventArgs e)
        {
            base.ObjectLoop(sender, e);
            byte moves = GameParameters.ActualBoard.GetValidMoves(ActualRows, ActualColumns);
            Move(moves);
            DropBomb();
            CheckForEnemyCollision();
            Update();
        }

        /// <summary>
        /// Porusza graczem uwzględniając możliwe kierunki ruchu zawarte w parametrze wejściowym.
        /// </summary>
        /// <param name="moves">Poprawne kierunki ruchu.</param>
        protected virtual void Move(byte moves)
        {
            if (ActualRows.Count != 0)
                ActualRows.Clear();

            if (ActualColumns.Count != 0)
                ActualColumns.Clear();

            if (Keyboard.IsKeyDown(Key.Left) && (moves & Left) == Left) 
                X -= Speed;
            
            else if (Keyboard.IsKeyDown(Key.Right) && (moves & Right) == Right)
                X += Speed;
            
            else if (Keyboard.IsKeyDown(Key.Up) && (moves & Up) == Up)
                Y -= Speed;
            
            else if (Keyboard.IsKeyDown(Key.Down) && (moves & Down) == Down)
                Y += Speed;
            
            ActualRows.Add((int)(Y / TileSize));
            if ((int)((Y + ObjectSize) / TileSize) != ActualRows.FirstOrDefault())
                ActualRows.Add((int)((Y + ObjectSize) / TileSize));

            ActualColumns.Add((int)(X / TileSize));
            if ((int)((X + ObjectSize) / TileSize) != ActualColumns.FirstOrDefault())
                ActualColumns.Add((int)((X + ObjectSize) / TileSize));
        }

        /// <summary>
        /// Uaktualnia statyczną klasę GameParameters informacjami o aktualnym graczu.
        /// </summary>
        protected void Update()
        {
            GameParameters.ActualPlayer = this;
        }

        /// <summary>
        /// Animacja czasu ochronnego po utracie życia przez gracza.
        /// </summary>
        private void BlinkingAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0.3;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            da.AutoReverse = true;
            da.RepeatBehavior = RepeatBehavior.Forever;
            da.RepeatBehavior = new RepeatBehavior(4);
            this.BeginAnimation(OpacityProperty, da);
        }

        /// <summary>
        /// Sprawdza czy nie nastąpiła kolizja z dowolnym przeciwnikiem.
        /// </summary>
        private void CheckForEnemyCollision()
        {
            foreach(var e in GameParameters.Enemies)
            {
                if (!e.IsDead && (IsInRange(e.X, e.Y) ||
                   IsInRange(e.X + ObjectSize, e.Y + ObjectSize) ||
                   IsInRange(e.X + ObjectSize, e.Y) ||
                   IsInRange(e.X, e.Y + ObjectSize)) && !Blinking)
                {
                    Die();
                }
            }
        }

        /// <summary>
        /// Stawia na planszy bombę.
        /// </summary>
        protected virtual void DropBomb()
        {
            if (Keyboard.IsKeyDown(Key.Space) && RemainingBombs > 0 && BombNotSetOrExploded)
            {
                BombNotSetOrExploded = false;
                bomb.SetBomb(ActualColumns.FirstOrDefault(), ActualRows.FirstOrDefault(), FiringRange);
                RemainingBombs -= 1;
            }
        }

        /// <summary>
        /// Metoda wywoływana po wykryciu kolizji z przeciwnikiem lub bombą.
        /// </summary>
        private void Die()
        {
            Blinking = true;
            DieTimer.Start();
            BlinkingAnimation();   

            RemainingLifes -= 1;

            if (RemainingLifes == 0)
                GameOver();
        }

        /// <summary>
        /// Kończy grę informując użytkownika stosownym komunikatem.
        /// </summary>
        private void GameOver()
        {
            string msgtext = "Do you want to play again?";
            string txt = "You lose!";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
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
        /// Kończy poziom informując użytkownika stosownym komunikatem.
        /// </summary>
        private void LevelCompleted()
        {
            string msgtext = "Do you want to play next level?";
            string txt = "Level completed!";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
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
