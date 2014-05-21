using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bombbrothers.Logic;
using Bombbrothers.Additional;
using LevelDifficulty = Bombbrothers.Logic.Enums.LevelDifficulty;
using System.Windows;
using Bombbrothers.GameObjects.Players;
using System.Windows.Threading;
using System.Timers;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    /// Abstrakcyjna klasa reprezentująca wroga. Dziedziczy po klasie GObject.
    /// </summary>
    public abstract class Enemy : GObject
    {
        /// <summary>
        /// Ilość żyć pozostałych wrogowi.
        /// </summary>
        protected int RemainingLifes;

        /// <summary>
        /// Szybkość wroga.
        /// </summary>
        protected double Speed;

        /// <summary>
        /// Typ wroga.
        /// </summary>
        protected Enums.EnemyType Type;

        /// <summary>
        /// Kierunek ruchu wroga (początkowo - prawo). Używane do poruszania się wroga.
        /// </summary>
        private byte Direction = Right;

        /// <summary>
        /// Numer identyfikujący wroga.
        /// </summary>
        protected int ID;

        /// <summary>
        /// pole informujące o tym czy wróg żyje czy nie.
        /// </summary>
        public bool IsDead;

        /// <summary>
        /// Zdarzenie wykonywane po zniszczeniu wroga.
        /// </summary>
        public event EventHandler EnemyDown;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="type">Typ enum wroga.</param>
        protected Enemy(Enums.EnemyType type) : base()
        {
            ID = GameParameters.ID++;
            IsDead = false;
            Type = type;

            switch(type)
            {
                case Enums.EnemyType.Biochem:
                    Speed = GameConst.EnemySpeed;
                    RemainingLifes = GameConst.BiochemLifes;
                    Fill = CreateImageBrush(GameConst.EnemyBiochem);
                    break;
                case Enums.EnemyType.Emo:
                    Speed = GameConst.EnemySpeed;
                    RemainingLifes = GameConst.EmoLifes;
                    Fill = CreateImageBrush(GameConst.EnemyEmo);
                    break;
                case Enums.EnemyType.Human:
                    Speed = GameConst.EnemySpeed;
                    RemainingLifes = GameConst.HumanLifes;
                    Fill = CreateImageBrush(GameConst.EnemyHuman);
                    break;
                case Enums.EnemyType.Matphys:
                    Speed = GameConst.EnemySpeed;
                    RemainingLifes = GameConst.MatphysLifes;
                    Fill = CreateImageBrush(GameConst.EnemyMatphys);
                    break;
                case Enums.EnemyType.Janitor:
                    Speed = GameConst.EnemySpeed;
                    RemainingLifes = GameConst.JanitorLifes;
                    Fill = CreateImageBrush(GameConst.EnemyJanitor);
                    break;
                default:
                    throw new NotImplementedException();
            }
            SetRandomPosition();
            StartTimer();
        }

        /// <summary>
        /// Metoda wywoływana po utworzeniu obiektu typu Bomb, który jest tworzony podczas tworzenia obiektu typu Player. Obsługuje zdarzenie wybuchu bomby.
        /// </summary>
        public void PostInitialize()
        {
            GameParameters.ActualBomb.BombBoom += new EventHandler(Boom);
        }

        /// <summary>
        /// Pętla gry dla wroga. Wykonuje ruch, uaktualnia status oraz rysuje w każdej klatce.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ObjectLoop(object sender, EventArgs e)
        {
            base.ObjectLoop(sender, e);         
            byte moves = GameParameters.ActualBoard.GetValidMoves(ActualRows, ActualColumns);
            Move(moves);
            Update();
          
        }

        /// <summary>
        /// Ustawia wroga w losowej (dostępnej) pozycji na planszy.
        /// </summary>
        private void SetRandomPosition()
        {
            int i = 0;

            i = StaticRandom.rand.Next() % GameParameters.EmptyTiles.Count;

            int row = GameParameters.EmptyTiles[i].Item1;
            int column = GameParameters.EmptyTiles[i].Item2;

            X = TileSize * column;
            Y = TileSize * row;

            ActualRows.Add(row);
            ActualColumns.Add(column);
            Draw();
        }

        /// <summary>
        /// Wykonuje ruch wroga biorąc pod uwagę dostępne aktualnie ruchy.
        /// </summary>
        /// <param name="moves">Dostępne aktualnie kierunki ruchów.</param>
        protected virtual void Move(byte moves)
        {
            if (ActualRows.Count != 0)
                ActualRows.Clear();

            if (ActualColumns.Count != 0)
                ActualColumns.Clear();

            if ((moves & Direction) == Direction)
                switch (Direction)
                {
                    case Right:
                        if ((moves & Right) == Right)
                        {
                            X += 1;
                        }
                        break;
                    case Left:
                        if ((moves & Left) == Left)
                        {
                            X -= 1;
                        }
                        break;
                    case Up:
                        if ((moves & Up) == Up)
                        {
                            Y -= 1;
                        }
                        break;
                    case Down:
                        if ((moves & Down) == Down)
                        {
                            Y += 1;
                        }
                        break;
                    default:
                        break;
                }
            else
            {
                int direction = StaticRandom.rand.Next() % 4;
                switch (direction)
                {
                    case 0:
                        Direction = Right;
                        break;
                    case 1:
                        Direction = Left;
                        break;
                    case 2:
                        Direction = Up;
                        break;
                    case 3:
                        Direction = Down;
                        break;
                    default:
                        break;
                }
            }

            ActualRows.Add((int)(Y / TileSize));
            if ((int)((Y + ObjectSize) / TileSize) != ActualRows.FirstOrDefault())
                ActualRows.Add((int)((Y + ObjectSize) / TileSize));

            ActualColumns.Add((int)(X / TileSize));
            if ((int)((X + ObjectSize) / TileSize) != ActualColumns.FirstOrDefault())
                ActualColumns.Add((int)((X + ObjectSize) / TileSize));
        }

        /// <summary>
        /// Sprawdza czy wróg nie był w zasięgu rażenia bomby. Jeśli tak - zmmniejsza licznik żyć wroga.
        /// </summary>
        private void CheckBombCollision()
        {
            CheckForBombCollision(x =>
            {
                if (IsInRange2((double)x[1], (double)x[2]))
                {
                    Die();
                    return 0;
                }

                return -1;
            }, GameConst.PlayerFiringRange);
        }

        /// <summary>
        /// Metoda wywoływana po otrzymaniu zdarzenia wybuchu bomby.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boom(object sender, EventArgs e)
        {
            CheckBombCollision();
        }

        /// <summary>
        /// Uaktualnia statyczną klasę GameParameters nowymi informacjami o wrogu.
        /// </summary>
        protected void Update()
        {
            int ind = GameParameters.Enemies.FindIndex(x => x.ID == ID);
            GameParameters.Enemies[ind] = this;
        }

        /// <summary>
        /// Metoda wywoływana po kontakcie wroga z bombą.
        /// </summary>
        public void Die()
        {
            RemainingLifes -= 1;
            if (RemainingLifes == 0)
            {
                if (this.EnemyDown != null)
                    this.EnemyDown(this, new EventArgs());

                StopTimer();
                IsDead = true;
                GameParameters.ActualCanvas.Children.Remove(this);
            }
        }
    }

    /// <summary>
    /// Statyczna klasa reprezentująca generator liczb losowych do ustawiania obiektów typu Enemy w losowych pozycjach.
    /// </summary>
    static class StaticRandom
    {
        static public Random rand = new Random();
    }
}
