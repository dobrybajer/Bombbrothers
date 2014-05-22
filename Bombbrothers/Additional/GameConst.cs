namespace Bombbrothers.Additional
{
    /// <summary>
    ///     Klasa reprezantująca stałe wartości używane w programie. Wszystkie wartości pochodzą z dokumentacji wstępnej.
    /// </summary>
    public static class GameConst
    {
        #region Map Const

        /// <summary>
        ///     Liczba wierszy w planszy.
        /// </summary>
        public const int MapRows = 13;

        /// <summary>
        ///     Liczba kolumn w planszy.
        /// </summary>
        public const int MapColumns = 17;

        /// <summary>
        ///     Liczba ławek na planszy.
        /// </summary>
        public const int BenchCount = 12;

        /// <summary>
        ///     Liczba płyt gipsowych na planszy.
        /// </summary>
        public const int GypsumCount = 40;

        /// <summary>
        ///     Czas przechodzenia przez ławkę (w sekundach);
        /// </summary>
        public const int BenchMoveTime = 3;

        /// <summary>
        ///     Rozmiar jednego Tile (w pikselach).
        /// </summary>
        public const int TileSize = 50;

        /// <summary>
        ///     Rozmiar jednego obiektu (gracz, wróg, bonus) (w pikselach).
        /// </summary>
        public const int ObjectSize = 45;

        /// <summary>
        ///     Szerokość planszy (w pikselach).
        /// </summary>
        public const int Width = 850;

        /// <summary>
        ///     Wysokość planszy (w pikselach).
        /// </summary>
        public const int Height = 650;

        /// <summary>
        ///     Rodzaj elementu planszy - tło.
        /// </summary>
        public const int TypeBackground = 0;

        /// <summary>
        ///     Rodzaj elementu planszy - element nośny.
        /// </summary>
        public const int TypeBlock = 1;

        /// <summary>
        ///     Rodzaj elementu planszy - ławka.
        /// </summary>
        public const int TypeBench = 2;

        /// <summary>
        ///     Rodzaj elementu planszy - płyta gipsowa.
        /// </summary>
        public const int TypeGypsum = 3;

        #endregion

        #region Player Const

        /// <summary>
        ///     Liczba bonusów postaci na poziom dla gracza nieśmiały.
        /// </summary>
        public const int ShyHideCount = 2;

        /// <summary>
        ///     Czas działania bonusu dla gracza nieśmiały (w sekundach).
        /// </summary>
        public const int ShyHideTime = 5;

        /// <summary>
        ///     Liczba bomb dla danego gracza na jeden poziom.
        /// </summary>
        public const int BombCount = 3;

        /// <summary>
        ///     Prędkość podstawowa postaci (wyrażona w pikselach na jednostkę czasu pętli gry).
        /// </summary>
        public const double PlayerSpeed = 1;

        /// <summary>
        ///     Prędkość postaci szybki.
        /// </summary>
        public const double FastSpeed = 1.3*PlayerSpeed;

        /// <summary>
        ///     Wartość dodawana do szybkości dla postaci szybki wraz z przebyciem każdego poziomu.
        /// </summary>
        public const double FastSpeedNextLevel = 0.05;

        /// <summary>
        ///     Zasięg rażenia bomby rzucanej przez gracza (w jednostkach, gdzie 1 jednostka = 1 wiersz = 1 kolumna).
        /// </summary>
        public const int PlayerFiringRange = 3;

        /// <summary>
        ///     Czas migania gracza po otrzymaniu ciosu (w sekundach).
        /// </summary>
        public const int BlinkingTimeAfterHit = 3;

        #endregion

        #region Enemy Const

        /// <summary>
        ///     Prędkość wroga (wyrażona w pikselach ne jednostkę czasu).
        /// </summary>
        public const double EnemySpeed = 0.8*PlayerSpeed;

        /// <summary>
        ///     Liczba żyć dla wroga human.
        /// </summary>
        public const int HumanLifes = 1;

        /// <summary>
        ///     Liczba żyć dla wroga biochem.
        /// </summary>
        public const int BiochemLifes = 2;

        /// <summary>
        ///     Liczba żyć dla wroga matfiz.
        /// </summary>
        public const int MatphysLifes = 3;

        /// <summary>
        ///     Liczba żyć dla wroga emo.
        /// </summary>
        public const int EmoLifes = 1;

        /// <summary>
        ///     Liczba żyć dla wroga woźny.
        /// </summary>
        public const int JanitorLifes = 10;

        /// <summary>
        ///     Odległość minimalna w jakiej mogą pojawić się wrogowie na planszy w stosunku do gracza. (wyrażona w jednostkach)
        /// </summary>
        public const int EnemyFromPlayerDistance = 5;

        /// <summary>
        ///     Liczba wrogów typu emo przypadająca na poziom.
        /// </summary>
        public const int EmoCount = 1;

        /// <summary>
        ///     Prędkość wroga typu woźny (wyrażona w pikselach na jednostkę czasu).
        /// </summary>
        public const double JanitorShot = 3*PlayerSpeed;

        /// <summary>
        ///     Liczba wrogów przypadająca na poziom 1.
        /// </summary>
        public const int Level1EnemyCount = 10;

        /// <summary>
        ///     Liczba wrogów przypadająca na poziom 2.
        /// </summary>
        public const int Level2EnemyCount = 13;

        /// <summary>
        ///     Liczba wrogów przypadająca na poziom 3.
        /// </summary>
        public const int Level3EnemyCount = 16;

        /// <summary>
        ///     Liczba wrogów przypadająca na poziom 4.
        /// </summary>
        public const int Level4EnemyCount = 20;

        /// <summary>
        ///     Liczba wrogów przypadająca na poziom 5.
        /// </summary>
        public const int Level5EnemyCount = 25;

        #endregion

        #region Bonus Const

        /// <summary>
        ///     Czas po jakim postawiona bomba wybucha (w sekundach).
        /// </summary>
        public const int BombTime = 3;

        #endregion

        #region Level Const

        /// <summary>
        ///     Współczynnik podstawowy do skalowania danych w zależności od poziomu trudności.
        /// </summary>
        public const double X = 1;

        /// <summary>
        ///     Czas rozgrywki jednego poziomu na poziomie łatwym.
        /// </summary>
        public const double EasyTime = 2*X;

        /// <summary>
        ///     Liczba żyć gracza na poziomie łatwym.
        /// </summary>
        public const int EasyLifes = 10;

        /// <summary>
        ///     Czas rozgrywki jednego poziomu na poziomie średnim.
        /// </summary>
        public const double MediumTime = 1.5*X;

        /// <summary>
        ///     Liczba żyć gracza na poziomie średnim.
        /// </summary>
        public const int MediumLifes = 5;

        /// <summary>
        ///     Czas rozgrywki jednego poziomu na poziomie trudnym.
        /// </summary>
        public const double HardTime = X;

        /// <summary>
        ///     Liczba żyć gracza na poziomie trudnym.
        /// </summary>
        public const int HardLifes = 3;

        #endregion

        #region Other Const

        /// <summary>
        ///     Wartość reprezentująca kierunek ruchu w prawo.
        /// </summary>
        public const byte Right = 2;

        /// <summary>
        ///     Wartość reprezentująca kierunek ruchu w dół.
        /// </summary>
        public const byte Down = 4;

        /// <summary>
        ///     Wartość reprezentująca kierunek ruchu w lewo.
        /// </summary>
        public const byte Left = 8;

        /// <summary>
        ///     Wartość reprezentująca kierunek ruchu w górę.
        /// </summary>
        public const byte Up = 16;

        #endregion

        #region Bitmap Const

        /// <summary>
        ///     Bitmapa odpowiadająca postaci zręczny.
        /// </summary>
        public const string PlayerAgile = "../../Resources/Bitmaps/player_agile.png";

        /// <summary>
        ///     Bitmapa odpowiadająca postaci szybki.
        /// </summary>
        public const string PlayerFast = "../../Resources/Bitmaps/player_fast.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca postaci nieśmiały.
        /// </summary>
        public const string PlayerShy = "../../Resources/Bitmaps/player_shy.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca elementowi otoczenia - tło.
        /// </summary>
        public const string MapBackground = @"/Bombbrothers;component/Resources/Bitmaps/background.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca elementowi otoczenia - element nośny.
        /// </summary>
        public const string MapBlock = @"/Bombbrothers;component/Resources/Bitmaps/block.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca elementowi otoczenia - płyta gipsowa.
        /// </summary>
        public const string MapGypsum = @"/Bombbrothers;component/Resources/Bitmaps/gypsum.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca elementowi otoczenia - ławka.
        /// </summary>
        public const string MapBench = @"/Bombbrothers;component/Resources/Bitmaps/bench.png";

        /// <summary>
        ///     Bitmapa odpowiadająca wrogowi biochem.
        /// </summary>
        public const string EnemyBiochem = "../../Resources/Bitmaps/enemy_biochem.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca wrogowi emo.
        /// </summary>
        public const string EnemyEmo = "../../Resources/Bitmaps/enemy_emo.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca wrogowi human.
        /// </summary>
        public const string EnemyHuman = "../../Resources/Bitmaps/enemy_human.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca wrogowi woźny.
        /// </summary>
        public const string EnemyJanitor = "../../Resources/Bitmaps/enemy_janitor.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca wrogowi matfiz.
        /// </summary>
        public const string EnemyMatphys = "../../Resources/Bitmaps/enemy_matfiz.jpg";

        /// <summary>
        ///     Bitmapa odpowiadająca bombie.
        /// </summary>
        public const string Bomb = "../../Resources/Bitmaps/bomb.png";

        /// <summary>
        ///     Bitmapa odpowiadająca wybuchowi bomby przypadająca na jedną jednostkę.
        /// </summary>
        public const string Explosion = "../../Resources/Bitmaps/MegaExplosion.png";

        #endregion
    }
}