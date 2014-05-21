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
    /// Klasa reprezentująca typ gracza - nieśmiały. Dziedziczy po abstrakcyjnej klasie Player.
    /// </summary>
    class ShyBro : Player
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public ShyBro() : base(PlayerType.Shy)
        {

        }

        /// <summary>
        /// Bonus gracza dostępny tylko dla postaci zręczny. Gracz staje się niewidoczny dla wrogów przez 5 sek. (nie zaimplementowano).
        /// </summary>
        public void Hide()
        {
            throw new NotImplementedException();
        }
    }
}
