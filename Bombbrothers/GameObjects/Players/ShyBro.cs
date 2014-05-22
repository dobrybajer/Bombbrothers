using System;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    ///     Klasa reprezentująca typ gracza - nieśmiały. Dziedziczy po abstrakcyjnej klasie Player.
    /// </summary>
    internal class ShyBro : Player
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public ShyBro() : base(Enums.PlayerType.Shy)
        {
        }

        /// <summary>
        ///     Bonus gracza dostępny tylko dla postaci zręczny. Gracz staje się niewidoczny dla wrogów przez 5 sek. (nie
        ///     zaimplementowano).
        /// </summary>
        public void Hide()
        {
            throw new NotImplementedException();
        }
    }
}