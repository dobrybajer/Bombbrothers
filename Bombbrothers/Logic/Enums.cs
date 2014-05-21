using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombbrothers.Logic
{
    /// <summary>
    /// Wyliczenia enum dla róznych typów wykorzystywanych w grze.
    /// </summary>
    public struct Enums
    {
        /// <summary>
        /// Poziom trudności: łatwy, średni, trudny
        /// </summary>
        public enum LevelDifficulty
        {
            Easy,
            Medium,
            Hard
        }

        /// <summary>
        /// Typ gracza: nieśmiały, szybki, zręczny
        /// </summary>
        public enum PlayerType
        {
            Shy,
            Fast, 
            Agile 
        }

        /// <summary>
        /// Bonus gracza: niewidzialność, dwie bomby naraz, żaden
        /// </summary>
        public enum PlayerBonus
        {
            Invisibility,
            TwoBombsOneDrop,
            None
        }

        /// <summary>
        /// Typ wroga: human, biochem, matfiz, emo, woźny
        /// </summary>
        public enum EnemyType
        {
            Human,
            Biochem,
            Matphys, 
            Emo,
            Janitor
        }

        /// <summary>
        /// Typ bonusu: guma do żucia, adidasy, książka, biedronka, kalkulator, inkantacja, zapalniczka, spirytus, zdjęcie Barbary
        /// </summary>
        public enum BonusType
        {
            ChewingGum,
            Adidases,
            Book, 
            Ladybird,
            Calc,
            Incantation,
            Lighter, 
            Spirit,
            BarbarasPhoto
        }

        /// <summary>
        /// Typ kafelka: element nośny, płyta gipsowa, ławka, inny - tło
        /// </summary>
        public enum TileType
        {
            Block, 
            Gypsum, 
            Bench,
            Other
        }
    }
    
}
