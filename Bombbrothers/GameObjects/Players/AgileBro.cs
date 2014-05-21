using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bombbrothers.Logic;
using PlayerType = Bombbrothers.Logic.Enums.PlayerType;
using LevelDifficulty = Bombbrothers.Logic.Enums.LevelDifficulty;
using System.Windows.Controls;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    /// Klasa reprezentująca typ gracza - zręczny. Dziedziczy po abstrakcyjnej klasie Player.
    /// </summary>
    class AgileBro : Player
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public AgileBro() : base(PlayerType.Agile)
        {

        }
    }
}
