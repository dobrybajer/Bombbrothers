using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombbrothers.Additional
{
    /// <summary>
    /// Klasa reprezantująca stałe wartości używane w programie. Wszystkie wartości pochodzą z dokumentacji wstępnej.
    /// </summary>
    public static class GameConst
    {
        #region Map Const
        public const int MapRows = 13;
        public const int MapColumns = 17;

        public const int BenchCount = 12;
        public const int GypsumCount = 40;

        public const int BenchMoveTime = 3;

        public const int TileSize = 50;
        public const int ObjectSize = 45;

        public const int Width = 850;
        public const int Height = 650;

        public const int TypeBackground = 0;
        public const int TypeBlock = 1;
        public const int TypeBench = 2;
        public const int TypeGypsum = 3;
        #endregion

        #region Player Const
        public const int ShyHideCount = 2;
        public const int ShyHideTime = 5;

        public const int BombCount = 3;

        public const double PlayerSpeed = 1;

        public const double FastSpeed = 1.3 * PlayerSpeed;
        public const double FastSpeedNextLevel = 0.05;

        public const int PlayerFiringRange = 3;

        public const int BlinkingTimeAfterHit = 3;
        #endregion

        #region Enemy Const
        public const double EnemySpeed = 0.8 * PlayerSpeed;

        public const int HumanLifes = 1;
        public const int BiochemLifes = 2;
        public const int MatphysLifes = 3;
        public const int EmoLifes = 1;
        public const int JanitorLifes = 10;

        public const int EnemyFromPlayerDistance = 5;

        public const int EmoCount = 1;

        public const double JanitorShot = 3 * PlayerSpeed;

        public const int Level1EnemyCount = 10;
        public const int Level2EnemyCount = 13;
        public const int Level3EnemyCount = 16;
        public const int Level4EnemyCount = 20;
        public const int Level5EnemyCount = 25;
        #endregion

        #region Bonus Const
        public const int BombTime = 3;

        #endregion

        #region Level Const
        public const double X = 1;

        public const double EasyTime = 2 * X;
        public const int EasyLifes = 10;

        public const double MediumTime = 1.5 * X;
        public const int MediumLifes = 5;

        public const double HardTime = X;
        public const int HardLifes = 3;
        #endregion

        #region Other Const
        public const byte Right = 2;
        public const byte Down = 4;
        public const byte Left = 8;
        public const byte Up = 16;
        #endregion

        #region Bitmap Const
        public const string PlayerAgile = "../../Resources/Bitmaps/player_agile.png";
        public const string PlayerFast = "../../Resources/Bitmaps/player_fast.jpg";
        public const string PlayerShy = "../../Resources/Bitmaps/player_shy.jpg";

        public const string MapBackground = @"/Bombbrothers;component/Resources/Bitmaps/background.jpg";
        public const string MapBlock = @"/Bombbrothers;component/Resources/Bitmaps/block.jpg";
        public const string MapGypsum = @"/Bombbrothers;component/Resources/Bitmaps/gypsum.jpg";
        public const string MapBench = @"/Bombbrothers;component/Resources/Bitmaps/bench.png";

        public const string EnemyBiochem = "../../Resources/Bitmaps/enemy_biochem.jpg";
        public const string EnemyEmo = "../../Resources/Bitmaps/enemy_emo.jpg";
        public const string EnemyHuman = "../../Resources/Bitmaps/enemy_human.jpg";
        public const string EnemyJanitor = "../../Resources/Bitmaps/enemy_janitor.jpg";
        public const string EnemyMatphys = "../../Resources/Bitmaps/enemy_matfiz.jpg";

        public const string Bomb = "../../Resources/Bitmaps/bomb.png";
        public const string Explosion = "../../Resources/Bitmaps/MegaExplosion.png";
        #endregion
    }
}
