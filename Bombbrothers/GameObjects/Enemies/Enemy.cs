using System;
using System.Linq;
using Bombbrothers.Additional;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    ///     Abstrakcyjna klasa reprezentująca wroga. Dziedziczy po klasie GObject.
    /// </summary>
    public abstract class Enemy : GObject
    {
        /// <summary>
        ///     Kierunek ruchu wroga (początkowo - prawo). Używane do poruszania się wroga.
        /// </summary>
        private byte _direction = Right;

        /// <summary>
        ///     Numer identyfikujący wroga.
        /// </summary>
        protected int Id;

        /// <summary>
        ///     pole informujące o tym czy wróg żyje czy nie.
        /// </summary>
        public bool IsDead;

        /// <summary>
        ///     Ilość żyć pozostałych wrogowi.
        /// </summary>
        protected int RemainingLifes;

        /// <summary>
        ///     Szybkość wroga.
        /// </summary>
        protected double Speed;

        /// <summary>
        ///     Typ wroga.
        /// </summary>
        protected Enums.EnemyType Type;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="type">Typ enum wroga.</param>
        protected Enemy(Enums.EnemyType type)
        {
            Id = GameParameters.Id++;
            IsDead = false;
            Type = type;

            switch (type)
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
        ///     Zdarzenie wykonywane po zniszczeniu wroga.
        /// </summary>
        public event EventHandler EnemyDown;

        /// <summary>
        ///     Metoda wywoływana po utworzeniu obiektu typu Bomb, który jest tworzony podczas tworzenia obiektu typu Player.
        ///     Obsługuje zdarzenie wybuchu bomby.
        /// </summary>
        public void PostInitialize()
        {
            GameParameters.ActualBomb.BombBoom += Boom;
        }

        /// <summary>
        ///     Pętla gry dla wroga. Wykonuje ruch, uaktualnia status oraz rysuje w każdej klatce.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ObjectLoop(object sender, EventArgs e)
        {
            base.ObjectLoop(sender, e);
            var moves = GameParameters.ActualBoard.GetValidMoves(ActualRows, ActualColumns);
            Move(moves);
            Update();
        }

        /// <summary>
        ///     Ustawia wroga w losowej (dostępnej) pozycji na planszy.
        /// </summary>
        private void SetRandomPosition()
        {
            var i = StaticRandom.Rand.Next()%GameParameters.EmptyTiles.Count;

            var row = GameParameters.EmptyTiles[i].Item1;
            var column = GameParameters.EmptyTiles[i].Item2;

            X = TileSize*column;
            Y = TileSize*row;

            ActualRows.Add(row);
            ActualColumns.Add(column);
            Draw();
        }

        /// <summary>
        ///     Wykonuje ruch wroga biorąc pod uwagę dostępne aktualnie ruchy.
        /// </summary>
        /// <param name="moves">Dostępne aktualnie kierunki ruchów.</param>
        protected virtual void Move(byte moves)
        {
            if (ActualRows.Count != 0)
                ActualRows.Clear();

            if (ActualColumns.Count != 0)
                ActualColumns.Clear();

            if ((moves & _direction) == _direction)
                switch (_direction)
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
                }
            else
            {
                var direction = StaticRandom.Rand.Next()%4;
                switch (direction)
                {
                    case 0:
                        _direction = Right;
                        break;
                    case 1:
                        _direction = Left;
                        break;
                    case 2:
                        _direction = Up;
                        break;
                    case 3:
                        _direction = Down;
                        break;
                }
            }

            ActualRows.Add((int) (Y/TileSize));
            if ((int) ((Y + ObjectSize)/TileSize) != ActualRows.FirstOrDefault())
                ActualRows.Add((int) ((Y + ObjectSize)/TileSize));

            ActualColumns.Add((int) (X/TileSize));
            if ((int) ((X + ObjectSize)/TileSize) != ActualColumns.FirstOrDefault())
                ActualColumns.Add((int) ((X + ObjectSize)/TileSize));
        }

        /// <summary>
        ///     Sprawdza czy wróg nie był w zasięgu rażenia bomby. Jeśli tak - zmmniejsza licznik żyć wroga.
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
            }, GameConst.PlayerFiringRange);
        }

        /// <summary>
        ///     Metoda wywoływana po otrzymaniu zdarzenia wybuchu bomby.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boom(object sender, EventArgs e)
        {
            CheckBombCollision();
        }

        /// <summary>
        ///     Uaktualnia statyczną klasę GameParameters nowymi informacjami o wrogu.
        /// </summary>
        protected void Update()
        {
            var ind = GameParameters.Enemies.FindIndex(x => x.Id == Id);
            GameParameters.Enemies[ind] = this;
        }

        /// <summary>
        ///     Metoda wywoływana po kontakcie wroga z bombą.
        /// </summary>
        public void Die()
        {
            RemainingLifes -= 1;
            if (RemainingLifes != 0) return;
            if (EnemyDown != null)
                EnemyDown(this, new EventArgs());

            StopTimer();
            IsDead = true;
            GameParameters.ActualCanvas.Children.Remove(this);
        }
    }

    /// <summary>
    ///     Statyczna klasa reprezentująca generator liczb losowych do ustawiania obiektów typu Enemy w losowych pozycjach.
    /// </summary>
    internal static class StaticRandom
    {
        public static Random Rand = new Random();
    }
}