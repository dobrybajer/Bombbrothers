using System;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    ///     Klasa reprezentująca bonus 'Adidas'. Dziedziczy po abstrakcyjnej klasie Bonus. (Nie zaimplementowano)
    /// </summary>
    internal class Spirit : Bonus
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Spirit() : base(Enums.BonusType.Spirit)
        {
        }

        /// <summary>
        ///     Metoda wykonująca akcję danego bonusu.
        /// </summary>
        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}