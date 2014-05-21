using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerType = Bombbrothers.Logic.Enums.PlayerType;
using LevelDifficulty = Bombbrothers.Logic.Enums.LevelDifficulty;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    /// Klasa reprezentująca typ gracza - szybki. Dziedziczy po abstrakcyjnej klasie Player.
    /// </summary>
    class FastBro : Player
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public FastBro() : base(PlayerType.Fast)
        {
            
        }
    }
}
